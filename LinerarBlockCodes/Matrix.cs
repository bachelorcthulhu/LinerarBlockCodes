using System.Linq;

namespace LinerarBlockCodes
{
    public class Matrix
    {
        protected int Rows { get; set; }
        protected int Columns { get; set; }
        public GaloisField[,] Data { get; set; }
        public Matrix(int _size) 
        {
            Rows= _size;
            Columns= _size;
            Data = new GaloisField[Rows, Columns];
        }
        public Matrix(int _rows,int _columns) 
        { 
            Rows = _rows;
            Columns = _columns;
            Data = new GaloisField[Rows, Columns];
        }

        public int GetRows()
        {
            return Rows;
        }

        public int GetColumns()
        {
            return Columns;
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write("{0} ", Data[i, j].Value);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public class IdentityMatrix : Matrix
    {
        public List<string> DualSyndromes { get; set; }

        public IdentityMatrix(int _size, bool _isDualSyndromesListNeeded) : base(_size)
        {
            Rows = _size;
            Columns = _size;
            Data = new GaloisField[Rows, Columns];
            DualSyndromes = new List<string>();

            for (int i = 0; i < _size; i++)
            {
                Data[i, i].Value = GF.ElementOne;
            }

            if (_isDualSyndromesListNeeded)
            {
                DualSyndromes.Add(SupportMethods.MakeStringFromVector(SupportMethods.GetColumn(Data, _size - 1)));
                for (int i = _size - 2; i >= 0; i--)
                {
                    DualSyndromes.Insert(0,
                        SupportMethods.MakeDualSyndrome(Data, i, i+1));
                    DualSyndromes.Insert(0, 
                        SupportMethods.MakeStringFromVector(SupportMethods.GetColumn(Data, i)));
                }
            }
        }
    }


    public class ParityCheckMatrix : Matrix
    {
        public int IdentityPartLength { get; private set; }
        public int ParityPartLength { get; private set; }
        public List<string> AllSyndromes { get; set; }
        public List<string> MainSyndromes { get; set; }
        public List<string> DualSyndromes { get; set; }

        public ParityCheckMatrix(int _rows,int _columns, bool _isDualSyndromesListNeeded)
            : base(_rows, _columns) 
        {
            IdentityPartLength = Rows;
            ParityPartLength = Columns - IdentityPartLength;
            AllSyndromes = new List<string>();
            MainSyndromes = new List<string>();
            DualSyndromes= new List<string>();

            int number = 0; // Стартовое число, с которого начинает заполняться матрица, т.к. 1 и 2 являются степенями двойки
            string binaryNumberSyndrom;           

            if (_isDualSyndromesListNeeded)
            {
                IdentityMatrix identityMatrix = new IdentityMatrix(Rows, true);
                AllSyndromes = identityMatrix.DualSyndromes;

                for (int i = 0; i < IdentityPartLength; i++)
                {
                    Data[i, ParityPartLength + i].Value = identityMatrix.Data[i, i].Value;
                }

                List<string> SyndromesCandidates = new List<string>();
                GaloisField[] tempVariable = new GaloisField[Rows];

                for (int i = 0; i < ParityPartLength; i++)
                {    
                    while (true)
                    {
                        number++;
                        binaryNumberSyndrom = SupportMethods.ConvertToBinary(number, Rows);        

                        if (SyndromesCandidates.Count != 0)
                        {
                            foreach (var candidate in SyndromesCandidates)
                            {
                                if (!AllSyndromes.Contains(candidate))
                                {
                                    tempVariable = SupportMethods.MakeVectorFromString(candidate);
                                    if (!AllSyndromes.Contains(SupportMethods.MakeDualSyndrome(
                                        SupportMethods.MakeVectorFromString(AllSyndromes.First()),
                                        tempVariable)))
                                    {
                                        SyndromesCandidates.Remove(candidate);
                                        break;
                                    }
                                }
                                else
                                {
                                    SyndromesCandidates.Remove(candidate);
                                }
                            }
                        }

                        if (!AllSyndromes.Contains(binaryNumberSyndrom))
                        {
                            tempVariable = SupportMethods.MakeVectorFromString(binaryNumberSyndrom);

                            if (!AllSyndromes.Contains(SupportMethods.MakeDualSyndrome(
                                        SupportMethods.MakeVectorFromString(AllSyndromes.First()),
                                        tempVariable)))
                            {
                                break;
                            }
                            else
                            {
                                SyndromesCandidates.Add(binaryNumberSyndrom);
                            }

                        }
                    }

                    AllSyndromes.Insert(0, SupportMethods.MakeDualSyndrome(
                                        SupportMethods.MakeVectorFromString(AllSyndromes.First()),
                                        tempVariable));

                    AllSyndromes.Insert(0, binaryNumberSyndrom);

                    for (int j = 0; j < Rows; j++)
                    {
                        if (binaryNumberSyndrom[j] == '0')
                            Data[j,i].Value = GF.ElementZero;
                        else
                            Data[j,i].Value = GF.ElementOne;
                    }
            }
                for (int i = 0; i < AllSyndromes.Count; i++)
                {
                    if (i % 2 == 0)
                        MainSyndromes.Add(AllSyndromes[i]);
                    else
                        DualSyndromes.Add(AllSyndromes[i]);        
                }
            }
        }

        public void PrintAllSyndromes()
        {

            for (int i = 0; i < MainSyndromes.Count; i += 1)
            {
                    Console.WriteLine("Синдром ошибки в {0} символе: {1}", i + 1, MainSyndromes[i]);               
            }

            for (int i = 0; i < DualSyndromes.Count; i += 1)
            {
                Console.WriteLine("Синдром ошибки в {0} и {1} символе: {2}", i + 1, i + 2, DualSyndromes[i]);
            }
            Console.WriteLine();
        }

    }

    public class GeneratorMatrix : Matrix
    {
        public int IdentityPartLength { get; private set; }
        public int ParityPartLength { get; private set; }
        public int MessageLength { get; private set; }

        public GeneratorMatrix(int _rows, int _columns) : base(_rows, _columns) 
        {
            IdentityPartLength = Rows;
            MessageLength = Columns;
            ParityPartLength = MessageLength - IdentityPartLength;
        }

        public void FillMatrix(Matrix _parityCheckMatrix)
        {
            int hMatrixRow = 0; // for count columns in parity check matrix

            IdentityMatrix identityMatrix = new IdentityMatrix(Rows, true);

            for (int i = 0; i < IdentityPartLength; i++)
            {
                Data[i, i].Value = identityMatrix.Data[i, i].Value;
            }

            for (int i = IdentityPartLength; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Data[j,i].Value = _parityCheckMatrix.Data[hMatrixRow, j].Value;
                }
                hMatrixRow++;
            }
        }
    }
}

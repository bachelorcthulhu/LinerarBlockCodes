namespace LinerarBlockCodes
{
    public class Matrix
    {
        protected int Rows { get; set; }
        protected int Columns { get; set; }
        protected GaloisField[,] Data { get; set; }
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
        }
    }

    public class IdentityMatrix : Matrix
    {
        public IdentityMatrix(int _size) : base(_size)
        {
            Rows = _size;
            Columns = _size;
            Data = new GaloisField[Rows, Columns];

            for (int i = 0; i < _size; i++)
            {
                Data[i, i].Value = GF.ElementOne;
            }
        }
    }


    public class ParityCheckMatrix : Matrix
    {
        public int ParityPartLength { get; private set; }
        public int IdentityPartLength { get; private set; }
        public int SyndromesLength { get; private set; }
        public List<string> AllSyndroms { get; set; }
        public ParityCheckMatrix(int _rows,int _columns) : base(_rows, _columns) 
        {
            ParityPartLength = _rows;
            IdentityPartLength = _columns - _rows; //need to make check that columns more than rows
            SyndromesLength = Rows;
            AllSyndroms = new List<string>();
        }

        public void MakeTwoSyndromMatrix()
        {
            List<string> canditatesToSyndroms = new List<string>();

            int number = 3; // Стартовое число, с которого начинает заполняться матрица, т.к. 1 и 2 являются степенями двойки
            int powerOfTwo = 0; // Степень для двойки; степень 0 => 2^0 => 1

            string binaryNumber = SupportMethods.ConvertToBinary((int)Math.Pow(2, powerOfTwo), SyndromesLength);

            AllSyndroms.Add(binaryNumber);

            for (int j = 0; j < Rows; j++)
            {
                Data[j, Columns - 1].Value = (byte)binaryNumber[j];
            }

            for (int i = Columns - 2; i >= IdentityPartLength; i--)
            {

                //Получаем двоичную запись числа
                binaryNumber = SupportMethods.ConvertToBinary((int)Math.Pow(2, powerOfTwo), Rows);

                //Заполняем колонку двоичным числом
                for (int j = 0; j < Rows; j++)
                {
                    Data[j, i].Value = (byte)binaryNumber[j];

                }
                AllSyndroms.Insert(0, SupportMethods.MakeDualSyndrom(SupportMethods.MakeGFArray(binaryNumber),
                    SupportMethods.MakeGFArray(AllSyndroms.First())));

                //Увеличиваем степень двойки
                powerOfTwo++;
            }

        }
    }

    public class GeneratorMatrix : Matrix
    {
        public GeneratorMatrix(int _rows, int _columns) : base(_rows, _columns) 
        { 

        }
    }
}

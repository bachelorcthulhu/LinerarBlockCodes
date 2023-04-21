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

        public ParityCheckMatrix(int _rows,int _columns) : base(_rows, _columns) 
        {
            ParityPartLength = _rows;
        }
    }

    public class GeneratorMatrix : Matrix
    {
        public GeneratorMatrix(int _rows, int _columns) : base(_rows, _columns) 
        { 

        }
    }
}

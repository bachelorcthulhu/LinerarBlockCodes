﻿namespace LinerarBlockCodes
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

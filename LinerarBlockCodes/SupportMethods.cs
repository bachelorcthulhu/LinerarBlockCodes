using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerarBlockCodes
{
    public static class SupportMethods
    {
        public static string MakeStringFromVector(GaloisField[] _vector)
        {
            string vectorAsString = string.Empty;

            for (int i = 0; i < _vector.Length; i++)
            {
                vectorAsString += _vector[i].ToChar();
            }

            return vectorAsString;
        }

        public static GaloisField[] MakeVectorFromString(string _vector)
        {
            GaloisField[] vector = new GaloisField[_vector.Length];

            for (int i = 0; i < _vector.Length; i++)
            {
                if (_vector[i] == '0')
                    vector[i].Value = GF.ElementZero;
                else
                    vector[i].Value = GF.ElementOne;
            }

            return vector;
        }

        public static GaloisField[] GetColumn<GaloisField>(this GaloisField[,] matrix, int _column)
        {
            var rowLength = matrix.GetLength(0);
            var columnVector = new GaloisField[rowLength];

            for (var i = 0; i < rowLength; i++)
                columnVector[i] = matrix[i, _column];

            return columnVector;
        }

        public static string MakeDualSyndrome(GaloisField[] _firstSyndrome,
            GaloisField[] _secondSyndrome)
        {
            int dualSyndromeLength = _firstSyndrome.Length;

            GaloisField[] dualSyndromGA = new GaloisField[dualSyndromeLength];

            for (int i = 0; i < _firstSyndrome.Length; i++)
            {
                dualSyndromGA[i] = _firstSyndrome[i] + _secondSyndrome[i];
            }

            return MakeStringFromVector(dualSyndromGA);
        }

        public static string MakeDualSyndrome(GaloisField[,] _data, int _firstSyndromeColumn,
            int _secondSyndromeColumn)
        {
            GaloisField[] firstSyndrome = GetColumn(_data, _firstSyndromeColumn);
            GaloisField[] secondSyndrome = GetColumn(_data, _secondSyndromeColumn);

            int dualSyndromeLength = firstSyndrome.Length;

            GaloisField[] dualSyndromGA = new GaloisField[dualSyndromeLength];

            for (int i = 0; i < dualSyndromeLength; i++)
            {
                dualSyndromGA[i] = firstSyndrome[i] + secondSyndrome[i];
            }

            return MakeStringFromVector(dualSyndromGA);
        }

        public static GaloisField[] MakeCodedWord(GaloisField[] _vector, GeneratorMatrix _gMatrix)
        {
            GaloisField[] codedVector = new GaloisField[_gMatrix.MessageLength];

            for (int i = 0; i < _gMatrix.GetRows(); i++) 
            {
                for (int j = 0; j < _gMatrix.GetColumns(); j++)
                {
                    codedVector[j] = codedVector[j] + (_vector[i] * _gMatrix.Data[i, j]);
                }
            }

            return codedVector;
        }

        public static GaloisField[] CheckSyndrome(GaloisField[] _codedWord, ParityCheckMatrix _hMatrix)
        {
            GaloisField[] decodedWord = new GaloisField[_hMatrix.IdentityPartLength];

            for (int i = 0; i < _hMatrix.GetRows(); i++)
            {
                for (int j = 0; j < _hMatrix.GetColumns(); j++)
                {
                    decodedWord[i] = decodedWord[i] + (_codedWord[j] * _hMatrix.Data[i, j]);
                }
            }

            return decodedWord;
        }

        public static int CheckOnSingleErrors(List<string> _mainSyndroms, GaloisField[] _syndrome)
        {
            string syndrome = MakeStringFromVector(_syndrome);

            for (int i = 0; i < _mainSyndroms.Count; i++)
            {
                if (_mainSyndroms[i] == syndrome)
                    return i;
            }

            return -1;
        }

        public static int[] CheckOnDualErrors(List<string> _dualSyndromes, GaloisField[] _syndrome)
        {
            string syndrome = MakeStringFromVector(_syndrome);
            int[] numbers = new int[2]; 

            for (int i = 0; i < _dualSyndromes.Count; i++)
            {
                if (_dualSyndromes[i] == syndrome)
                {
                    numbers[0] = i;
                    numbers[1] = i + 1;
                    return numbers;
                }
            }
            return null;
        }

        public static string ConvertToBinary(int _number, int _numberLength)
        {
            string binaryNumber = Convert.ToString(_number, 2);

            return binaryNumber.PadLeft(_numberLength, '0');
        }
    }
}

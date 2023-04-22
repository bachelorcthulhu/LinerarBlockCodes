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
    }
}

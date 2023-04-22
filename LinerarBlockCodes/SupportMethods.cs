using System.Numerics;

namespace LinerarBlockCodes
{
    public static class SupportMethods
    {
        public static string ConvertToBinary(int _number, int _necessaryLength)
        {
            string binaryNumber = Convert.ToString(_number, 2);

            //A new string that is equivalent to this instance, but right-aligned
            //and padded on the left with as many paddingChar characters as needed
            //to create a length of totalWidth. However, if totalWidth is less than
            //the length of this instance, the method returns a reference to the
            //existing instance. If totalWidth is equal to the length of this
            //instance, the method returns a new string that is identical to this instance.

            return binaryNumber.PadLeft(_necessaryLength, '0'); 
        }

        public static bool IsPowerOfTwo(int n)
        {
            return (int)(Math.Ceiling((Math.Log(n) / Math.Log(2))))
                  == (int)(Math.Floor(((Math.Log(n) / Math.Log(2)))));
        }

        public static string MakeDualSyndrom(GaloisField[] _firstSyndrom, GaloisField[] _secondSyndrom)
        {
            if (_firstSyndrom.Length != _secondSyndrom.Length)
            {
                throw new ArgumentException(String.Format("Длина синдромов не равна. Длина синдрома a: " +
                    "{0} Длина синдрома b: {1}", _firstSyndrom.Length, _secondSyndrom.Length));
            }

            string dualSyndrom = string.Empty;

            for (int i = 0; i < _firstSyndrom.Length; i++)
            {
                dualSyndrom += (_firstSyndrom[i] + _secondSyndrom[i]).ToChar();
            }

            return dualSyndrom;
        }

        public static GaloisField[] MakeGFArray(string _syndrom)
        {
            GaloisField[] syndrom = new GaloisField[_syndrom.Length];

            for(int i = 0; i < _syndrom.Length; i++)
            {
                syndrom[i].Value = (byte)_syndrom[i];
            }

            return syndrom;
        }

        public static void AddSyndroms(string _binaryNumber, List<string> _allSyndroms)
        {

            _allSyndroms.Insert(0, MakeDualSyndrom(SupportMethods.MakeGFArray(_binaryNumber),
                SupportMethods.MakeGFArray(_allSyndroms.First())));

            _allSyndroms.Insert(0, _binaryNumber);
        }
    }
}

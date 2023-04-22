global using GF = LinerarBlockCodes.GaloisFieldElements;

namespace LinerarBlockCodes
{
    public static class GaloisFieldElements
    {
        public static readonly byte ElementZero = 0;
        public static readonly byte ElementOne = 1;
    } 

    public struct GaloisField
    {

        public byte Value { get; set; }

        public GaloisField() {
            Value = 0;
        }

        public GaloisField(byte _value)
        {
            
            Value = _value;
        }

        public static GaloisField operator +(GaloisField _a, GaloisField _b)
        {
            return new GaloisField((byte)(_a.Value ^ _b.Value));
        }

        public static GaloisField operator -(GaloisField _a, GaloisField _b)
        {
            return new GaloisField((byte)(_a.Value ^ _b.Value));
        }

        public static GaloisField operator *(GaloisField _a, GaloisField _b)
        {
            return new GaloisField((byte)(_a.Value & _b.Value));
        }

        public string ToChar()
        {
            return Convert.ToString(Value);
        }
    }
}

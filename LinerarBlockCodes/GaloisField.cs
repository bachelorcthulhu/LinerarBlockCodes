global using GF = LinerarBlockCodes.GaloisFieldElements;
using System;

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
            if (_value > 1 ) {
                throw new ArgumentException(String.Format("GF(2) принимает значения только 0 и 1. Вы ввели: " +
                    "{0}", _value));
            }

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

        public char ToChar()
        {      
            return (char)Value;
        }
    }
}

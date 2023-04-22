using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinerarBlockCodes
{
    public static class UserInteraction
    {
        public static GaloisField[] InputNumber(int _messagePartLength)
        {
            string input;
            int inputNumber;

            string binaryNumber;

            while (true)
            {
                Console.WriteLine("Введите число в промежутке от 1 до {0}", Math.Pow(2, _messagePartLength));
                input = Console.ReadLine();

                try
                {
                    inputNumber = Int32.Parse(input);
                    if ((inputNumber <= Math.Pow(2, _messagePartLength)) & (inputNumber > 0))
                    {
                        binaryNumber = SupportMethods.ConvertToBinary(inputNumber, _messagePartLength);
                        Console.WriteLine("{0} будет выглядеть как {1}", inputNumber, binaryNumber);
                        return SupportMethods.MakeVectorFromString(binaryNumber);
                    }

                    Console.WriteLine("Вы ввели число либо меньше 1, либо больше {0}.", Math.Pow(2, _messagePartLength));
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Введенное '{input} не является числом.'");
                }
            }
        }

        public static void PrintVector(GaloisField[] _vector)
        {
            for (int i = 0; i < _vector.Length; i++)
            {
                Console.Write(_vector[i].Value);
            }
            Console.WriteLine();
        }

    }
}

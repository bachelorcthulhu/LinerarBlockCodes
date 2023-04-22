using LinerarBlockCodes;
using System.Numerics;

int codewordLength = 32;
int RedundantCheckingPart = 9;
int messagePartLength = codewordLength - RedundantCheckingPart;

string userInput;
int inputNumber;

GaloisField[] codedWord = new GaloisField[codewordLength];
GaloisField[] decodedSyndrom = new GaloisField[codewordLength];

ParityCheckMatrix parity32Matrix = new ParityCheckMatrix(9,32,true);
parity32Matrix.PrintMatrix();
parity32Matrix.PrintAllSyndromes();

GeneratorMatrix generator32Matrix = new GeneratorMatrix(23, 32);
generator32Matrix.FillMatrix(parity32Matrix);
generator32Matrix.PrintMatrix();



//GaloisField[] inputNumber = UserInteraction.InputNumber(23);
//GaloisField[] codedWord = SupportMethods.MakeCodedWord(inputNumber, generator32Matrix);
//UserInteraction.PrintVector(codedWord);
//GaloisField[] decodedWord = SupportMethods.DecodeWord(codedWord, parity32Matrix);
//UserInteraction.PrintVector(decodedSyndrom);

Console.WriteLine("Размеры матрицы - 9 на 32, вшиты в код, а именно programm.cs." +
        "Можно попробровать поменять их, но при этом стоит помнить о степенях двойки и общем принципа данного кодирования.");
Console.WriteLine();
Console.WriteLine("Введите 'convert', чтобы перевести число в двоичную систему исчисления с нужными разрядами");
Console.WriteLine("Введите 'code', чтобы закодировать информацию");
Console.WriteLine("Введите 'decode', чтобы декодировать информацию");
Console.WriteLine("Введите 'exit', чтобы выйти");

while (true)
{
    Console.WriteLine("Введите convert, code, decode или exit.");
    userInput = Console.ReadLine();

    if (userInput == "convert")
    {
        UserInteraction.InputNumber(23);
    }
    else if (userInput == "code")
    {
        Console.WriteLine("Введите в двоичной системе исчисления то, что желаете закодировать.");
        userInput = Console.ReadLine();
        try
        {
            codedWord = SupportMethods.MakeCodedWord(
                    SupportMethods.MakeVectorFromString(userInput), generator32Matrix);
            Console.Write("Закодирование слово выглядит так: ");
            UserInteraction.PrintVector(codedWord);
        }
        catch (FormatException)
        {
            Console.WriteLine("Вы неккоректно ввели число.");
        }
    }
    else if (userInput == "decode")
    {
        Console.WriteLine("Введите в двоичной системе исчисления то, что желаете декодировать.");
        userInput = Console.ReadLine();
        try
        {
            decodedSyndrom = SupportMethods.DecodeWord(
                    SupportMethods.MakeVectorFromString(userInput), parity32Matrix);
            Console.Write("Декодированный синдром выглядит так: ");
            UserInteraction.PrintVector(decodedSyndrom);
        }
        catch (FormatException)
        {
            Console.WriteLine("Вы неккоректно ввели число.");
        }
    }
    else if (userInput == "exit")
    {
        break;
    }
    else
        Console.WriteLine("Вы неправильно ввели команду");

}
  
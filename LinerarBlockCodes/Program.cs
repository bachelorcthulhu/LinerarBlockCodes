using LinerarBlockCodes;
using System.Numerics;

int codewordLength = 41;
int redundantCheckingPart = 9;
int messagePartLength = codewordLength - redundantCheckingPart;
string decodedSyndromString;

string userInput;
int inputNumber;
int syndromeNumber;
int[] dualSyndromesNumbers = new int[2];

GaloisField[] codedWord = new GaloisField[codewordLength];
GaloisField[] decodedSyndrom = new GaloisField[codewordLength];
GaloisField[] userCodedWord = new GaloisField[codewordLength];

ParityCheckMatrix parity32Matrix = new ParityCheckMatrix(redundantCheckingPart,codewordLength,true);
parity32Matrix.PrintMatrix();
parity32Matrix.PrintAllSyndromes();

GeneratorMatrix generator32Matrix = new GeneratorMatrix(messagePartLength, codewordLength);
generator32Matrix.FillMatrix(parity32Matrix);
generator32Matrix.PrintMatrix();



//GaloisField[] inputNumber = UserInteraction.InputNumber(23);
//GaloisField[] codedWord = SupportMethods.MakeCodedWord(inputNumber, generator32Matrix);
//UserInteraction.PrintVector(codedWord);
//GaloisField[] decodedWord = SupportMethods.DecodeWord(codedWord, parity32Matrix);
//UserInteraction.PrintVector(decodedSyndrom);

Console.WriteLine("Размеры матрицы - 9 на 41, вшиты в код, а именно programm.cs." +
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
        UserInteraction.InputNumber(messagePartLength);
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
            decodedSyndrom = SupportMethods.CheckSyndrome(
                    SupportMethods.MakeVectorFromString(userInput), parity32Matrix);
            userCodedWord = SupportMethods.MakeVectorFromString(userInput);

            Console.Write("Декодированный синдром выглядит так: ");
            UserInteraction.PrintVector(decodedSyndrom);
            decodedSyndromString = SupportMethods.MakeStringFromVector(decodedSyndrom);

            syndromeNumber = SupportMethods.CheckOnSingleErrors(parity32Matrix.MainSyndromes,decodedSyndrom);
            dualSyndromesNumbers = SupportMethods.CheckOnDualErrors(parity32Matrix.DualSyndromes, decodedSyndrom);

            if (syndromeNumber != -1){
                Console.WriteLine("Ошибка в {0} символе: {1}", syndromeNumber + 1, decodedSyndromString);
                Console.WriteLine();

                userCodedWord[syndromeNumber]++;
            }
            
            if (SupportMethods.CheckOnDualErrors(parity32Matrix.DualSyndromes, decodedSyndrom) != null) 
            {
                Console.WriteLine("Ошибка в {0} и {1} символах.", dualSyndromesNumbers[0] + 1,
                    dualSyndromesNumbers[1] + 1);
                Console.WriteLine();

                userCodedWord[dualSyndromesNumbers[0]]++;
                userCodedWord[dualSyndromesNumbers[1]]++;

            }

            decodedSyndromString = SupportMethods.MakeStringFromVector(userCodedWord);
            decodedSyndromString.Remove(0, decodedSyndromString.Length - redundantCheckingPart);
            Console.WriteLine("Изначальное сообщение: {0}", decodedSyndromString);
            
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
  
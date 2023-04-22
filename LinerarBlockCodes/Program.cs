using LinerarBlockCodes;

IdentityMatrix testMatrix = new IdentityMatrix(5,true);

testMatrix.PrintMatrix();

foreach(var syndrom in testMatrix.DualSyndromes)
{
    Console.WriteLine(syndrom);
}
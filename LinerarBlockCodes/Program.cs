using LinerarBlockCodes;

IdentityMatrix testMatrix = new IdentityMatrix(5);

testMatrix.PrintMatrix();

ParityCheckMatrix newTestMatrix = new ParityCheckMatrix(9, 32);
newTestMatrix.MakeTwoSyndromMatrix();
newTestMatrix.PrintMatrix();
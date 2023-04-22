using LinerarBlockCodes;

IdentityMatrix testMatrix = new IdentityMatrix(5,true);

testMatrix.PrintMatrix();

ParityCheckMatrix extraTestMatrix = new ParityCheckMatrix(9,32,true);
extraTestMatrix.PrintMatrix();

extraTestMatrix.PrintAllSyndromes();
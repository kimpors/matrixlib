using System;

namespace MatrixLib
{
    public class Matrix
    {
        public double[][] array;
        public int XLength { get;}
        public int YLength { get; }

        public Matrix(int xSize, int ySize)
        {
            XLength = xSize;
            YLength = ySize;
        }

        public Matrix SetRandomValues(int min = 0, int max = 0)
        {
            for (int y = 0; y < YLength; y++)
            {
                for (int x = 0; x < XLength; x++)
                {
                    array[x][y] = new Random().NextDouble() * (max - min + 1) + min;
                }
            }
            return this;
        }
    }
}
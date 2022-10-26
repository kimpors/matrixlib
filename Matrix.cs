namespace MatrixLib
{
    public class Matrix
    {
        public double[,] array;
        public int XLength { get; }
        public int YLength { get; }

        public Matrix(int xLength, int yLength)
        {
            XLength = xLength;
            YLength = yLength;

            array = new double[xLength, yLength];
        }

        public bool isEqualSize(Matrix matrix)
        {
            return XLength == matrix.XLength && YLength == matrix.YLength;
        }

        public Matrix SetRandomValues(int min = 0, int max = 0)
        {
            for (int y = 0; y < YLength; y++)
            {
                for (int x = 0; x < XLength; x++)
                {
                    array[x, y] = new Random().NextDouble() * (max - min + 1) + min;
                }
            }

            return this;
        }

        public Matrix Sum(Matrix matrix)
        {
            if (isEqualSize(matrix) == false)
                throw new ArgumentException("Don't same size with matrix");

            for (int y = 0; y < YLength; y++)
            {
                for (int x = 0; x < XLength; x++)
                {
                    array[x, y] += matrix.array[x, y];
                }
            }

            return this;
        }

        public Matrix Multiply(double number)
        {
            for (int y = 0; y < YLength; y++)
            {
                for (int x = 0; x < XLength; x++)
                {
                    array[x, y] *= number;
                }
            }

            return this;
        }

        public Matrix Sub(Matrix matrix)
        {
            return Sum(matrix.Multiply(-1));
        }
    }
}
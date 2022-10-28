namespace MatrixLib
{
    public class Matrix
    {
        private double[,] _array;
        public uint RowLength { get; }
        public uint ColumnLength { get; }

        public Matrix(uint rows, uint columns)
        {
            RowLength = rows;
            ColumnLength = columns;

            _array = new double[RowLength, ColumnLength];
        }

        public void SetMatrix(double[,] arr)
        {
            _array = arr;
        }
        public double this[int row, int column]
        {
            get { return _array[row, column]; }
            set { _array[row, column] = value; }
        }

        public bool isEqualSize(Matrix matrix)
        {
            return RowLength == matrix.RowLength && ColumnLength == matrix.ColumnLength;
        }

        public Matrix SetRandomValues(int min = 0, int max = 0)
        {
            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    this[y, x] = new Random().NextDouble() * (max - min + 1) + min;
                }
            }

            return this;
        }

        public Matrix Sum(Matrix matrix)
        {
            if (isEqualSize(matrix) == false)
                throw new ArgumentException("Don't same size with matrix");

            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    this[y, x] += matrix[y, x];
                }
            }

            return this;
        }

        public Matrix Multiply(double number)
        {
            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    this[y, x] *= number;
                }
            }

            return this;
        }

        public Matrix Multiply(Matrix a)
        {
            if (ColumnLength == a.RowLength == false)
                throw new ArgumentException("Matrices are inconsistent");

            Matrix result = new Matrix(RowLength, a.ColumnLength);

            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < a.ColumnLength; x++)
                {
                    for (int n = 0; n < ColumnLength; n++)
                    {
                        result[y, x] += this[y, n] * a[n, x];
                    }
                }
            }

            return result;
        }

        public Matrix Sub(Matrix matrix)
        {
            return Sum(matrix.Multiply(-1));
        }
    }
}
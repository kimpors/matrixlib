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

        public static Matrix operator +(Matrix a, Matrix b)
            => a.Sum(b);
        public static Matrix operator -(Matrix a, Matrix b)
            => a.Sub(b);
        public static Matrix operator *(Matrix a, double b)
            => a.Mul(b);
        public static Matrix operator *(Matrix a, Matrix b)
            => a.Mul(b);

        public void SetMatrix(double[,] arr)
        {
            _array = arr;
        }
        public double this[int row, int column]
        {
            get { return _array[row, column]; }
            set { _array[row, column] = value; }
        }


        public bool isEqualSize(Matrix a)
        {
            return RowLength == a.RowLength && ColumnLength == a.ColumnLength;
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

        public Matrix Sum(Matrix a)
        {
            if (isEqualSize(matrix) == false)
                throw new ArgumentException("Don't same size with matrix");

            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    this[y, x] += a[y, x];
                }
            }

            return this;
        }

        public Matrix Mul(double a)
        {
            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    this[y, x] *= a;
                }
            }

            return this;
        }

        public Matrix Mul(Matrix a)
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

        public Matrix Sub(Matrix a)
        {
            return Sum(a.Mul(-1));
        }
    }
}
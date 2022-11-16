namespace MatrixLib
{
    public class Matrix
    {
        private double[,] _array;
        public uint RowLength { get; }
        public uint ColumnLength { get; }
        private const int OPTIMAL_RANK = 2;

        public Matrix(uint size)
        {
            RowLength = ColumnLength = size;

            _array = new double[size, size];
        }
        public Matrix(uint rows, uint columns)
        {
            RowLength = rows;
            ColumnLength = columns;

            _array = new double[rows, columns];
        }

        public Matrix(double[,] array)
        {
            RowLength = (uint)array.GetLength(0);
            ColumnLength = (uint)array.GetLength(1);

            _array = (double[,])array.Clone();
        }

        public static Matrix operator +(Matrix a, Matrix b)
            => a.Sum(b);
        public static Matrix operator -(Matrix a, Matrix b)
            => a.Sub(b);
        public static Matrix operator *(Matrix a, Matrix b)
            => a.Mul(b);
        public static Matrix operator *(Matrix a, double b)
            => a.Mul(b);

        public static bool IsSquareMatrix(Matrix a)
            => a.ColumnLength == a.RowLength;

        public bool IsEqualSize(Matrix a)
            => RowLength == a.RowLength && ColumnLength == a.ColumnLength;

        public static double GetDeterminant(Matrix a)
        {
            if (IsSquareMatrix(a) == false)
                return double.NaN;

            uint size = a.ColumnLength;
            if (size < OPTIMAL_RANK)
            {
                return 1;
            }
            else if (size == OPTIMAL_RANK)
            {
                return a[0, 0] * a[1, 1] - a[1, 0] * a[0, 1];
            }

            double result = 0;
            Matrix temp = new Matrix(size - 1);

            for (int n = 0; n < size; n++)
            {
                for (int y = 1; y < size; y++)
                {
                    for (int x = 0, subx = 0; x < size; x++)
                    {
                        if (x == n)
                            continue;

                        temp[y - 1, subx] = a[y, x];
                        subx++;
                    }
                }

                result += a[0, n] * (n % 2 == 0 ? 1 : -1) * GetDeterminant(temp);
            }

            return result;
        }

        public double this[int row, int column]
        {
            get { return _array[row, column]; }
            set { _array[row, column] = value; }
        }

        public Matrix SetRandomValues(int min = 0, int max = 0)
        {
            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    this[y, x] = new Random().NextInt64(min, max);
                }
            }

            return this;
        }

        public Matrix Sum(Matrix a)
        {
            if (IsEqualSize(a) == false)
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
            if (ColumnLength != a.RowLength)
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
            => Sum(a.Mul(-1));

        public override string ToString()
        {
            string result = string.Empty;

            for (int y = 0; y < RowLength; y++)
            {
                for (int x = 0; x < ColumnLength; x++)
                {
                    result += this[y, x] + " |";
                }
                result += "\n";
            }

            return result;
        }
    }
}
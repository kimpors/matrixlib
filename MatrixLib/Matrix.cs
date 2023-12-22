namespace MatrixLib;

public class Matrix
{
    private double[,] _array;
    private const uint s_OptimalRank= 2;

    public uint Rows { get; }
    public uint Columns { get; }

    public Matrix(uint size)
    {
        Rows = Columns = size;

        _array = new double[size, size];
    }
    public Matrix(uint rows, uint columns)
    {
        Rows = rows;
        Columns = columns;

        _array = new double[rows, columns];
    }

    public Matrix(double[,] array)
    {
        Rows = (uint)array.GetLength(0);
        Columns = (uint)array.GetLength(1);

        _array = (double[,])array.Clone();
    }

    public static Matrix operator +(Matrix a, Matrix b) => a.Sum(b);
    public static Matrix operator -(Matrix a, Matrix b) => a.Sub(b);
    public static Matrix operator *(Matrix a, Matrix b) => a.Mul(b);
    public static Matrix operator *(Matrix a, double b) => a.Mul(b);

    public static bool IsSquare(Matrix a) => a.Rows == a.Columns;
    public bool IsEqualSize(Matrix a) => Rows == a.Columns && Columns == a.Columns;

    public static double GetDeterminant(Matrix a)
    {
        if (IsSquare(a) == false)
        {
            return double.NaN;
        }

        uint size = a.Columns;
        if (size < s_OptimalRank)
        {
            return 1;
        }
        else if (size == s_OptimalRank)
        {
            return a[0, 0] * a[1, 1] - a[1, 0] * a[0, 1];
        }

        double result = 0;
        Matrix Temp = new Matrix(size - 1);

        for (int n = 0; n < size; n++)
        {
            for (int y = 1; y < size; y++)
            {
                for (int x = 0, subx = 0; x < size; x++)
                {
                    if (x == n)
                        continue;

                    Temp[y - 1, subx] = a[y, x];
                    subx++;
                }
            }

            result += a[0, n] * (n % 2 == 0 ? 1 : -1) * GetDeterminant(Temp);
        }

        return result;
    }

    public static Matrix GetReverse(Matrix a)
    {
        if (double.IsNaN(Matrix.GetDeterminant(a)))
        {
          throw new ArgumentException("Can't get determinant. It's not square matrix");
        }

        uint size = a.Columns;
        double determinant = Matrix.GetDeterminant(a);

        Matrix Result = new Matrix(size);
        Matrix Temp = new Matrix(size - 1);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int y = 0, suby = 0; y < size; y++)
                {
                    if (y == i)
                        continue;

                    for (int x = 0, subx = 0; x < size; x++)
                    {
                        if (x == j)
                            continue;

                        Temp[suby, subx] = a[y, x];
                        subx++;
                    }

                    suby++;
                }

                Result[j, i] = ((i + j) % 2 == 0 ? 1 : -1) * Matrix.GetDeterminant(Temp);
            }
        }

        return Result * Math.Round(1 / determinant, 3);
    }

    public double this[int row, int column]
    {
        get { return _array[row, column]; }
        set { _array[row, column] = value; }
    }

    public Matrix SetRandomValues(int min = 0, int max = 0)
    {
        Matrix Result = new Matrix(Rows, Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Result[y, x] = new Random().NextInt64(min, max);
            }
        }

        return Result;
    }

    public Matrix Sum(Matrix a)
    {
        if (IsEqualSize(a) == false)
        {
          throw new ArgumentException("Don't same size with matrix");
        }

        Matrix Result = new Matrix(a.Rows, a.Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Result[y, x] = this[y, x] + a[y, x];
            }
        }

        return Result;
    }

    public Matrix Mul(double a)
    {
        Matrix Result = new Matrix(Rows, Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Result[y, x] = Math.Round(this[y, x] * a, 3);
            }
        }

        return Result;
    }

    public Matrix Mul(Matrix a)
    {
        if (Columns != a.Rows)
        {
            throw new ArgumentException("Matrices are inconsistent(ColumnLength != a.RowLength)");
        }

        Matrix Result = new Matrix(Rows, a.Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < a.Columns; x++)
            {
                for (int n = 0; n < Columns; n++)
                {
                    Result[y, x] += this[y, n] * a[n, x];
                }

                Result[y, x] = Math.Round(Result[y, x], 3);
            }
        }

        return Result;
    }

    public Matrix Sub(Matrix a) => Sum(a.Mul(-1));

    public override string ToString()
    {
        string result = string.Empty;

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                result += this[y, x] + " |";
            }

            result += "\n";
        }

        return result;
    }
}

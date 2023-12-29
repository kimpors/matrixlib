using System.Text;

namespace MatrixLib;

public class Matrix
{
    private double[,] _array;
    private const uint s_OptimalRank= 2;

    public uint Rows { get; }
    public uint Columns { get; }

    ///  <summary>
    ///   Initilize new empty square matrix
    ///  </summary>
    ///  <param name="size">Length of rows and columns</param>
    ///  <returns>New empty square matrix</returns>
    public Matrix(uint size)
    {
        Rows = Columns = size;
        _array = new double[size, size];
    }

    ///  <summary>
    ///   Initilize new empty matrix
    ///  </summary>
    ///  <param name="rows">Length of rows</param>
    ///  <param name="columns">Length of columns</param>
    ///  <returns>New empty matrix</returns>
    public Matrix(uint rows, uint columns)
    {
        Rows = rows;
        Columns = columns;

        _array = new double[rows, columns];
    }

    ///  <summary>
    ///   Initilize new matrix using 2 dimension array
    ///  </summary>
    ///  <param name="array">2 dimension array</param>
    ///  <returns>New matrix with same data as in 'array'</returns>
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

    ///  <summary>
    ///   Convert from 'Matrix' to 2 dimension double array
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Returns 2 dimension array</returns>
    public static double[,] Raw(Matrix a) => a._array;

    ///  <summary>
    ///   Check if matrix have same rows and columns
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Returns true if rows and colums are equal</returns>
    public static bool IsSquare(Matrix a) => a.Rows == a.Columns;

    ///  <summary>
    ///   Checks if matrices have equal size.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Return true if matrices have same rows and columns</returns>
    public bool IsEqualSize(Matrix a) => Rows == a.Rows && Columns == a.Columns;

    ///  <summary>
    ///   Rounds a number to the nearest integer.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <param name="precise">The accuracy of the numerical calculations performed</param>
    ///  <returns>Matrix with round data</returns>
    public static Matrix Round(Matrix a, uint precise = 2)
    {
      Matrix Result = new(a.Rows, a.Columns);

      for (int y = 0; y < a.Rows; y++)
      {
        for (int x = 0; x < a.Columns; x++)
        {
          Result[y, x] = Math.Round(a[y, x], (int)precise);
        }
      }

      return Result;
    }

    ///  <summary>
    ///   Rounds a number up to the next lower whole number.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Matrix with round to floor data</returns>
    public static Matrix Floor(Matrix a)
    {
      Matrix Result = new(a.Rows, a.Columns);

      for (int y = 0; y < a.Rows; y++)
      {
        for (int x = 0; x < a.Columns; x++)
        {
          Result[y, x] = Math.Floor(a[y, x]);
        }
      }

      return Result;
    }

    ///  <summary>
    ///   Rounds a number up to the next higher whole number.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Matrix round to ceil data</returns>
    public static Matrix Ceil(Matrix a)
    {
      Matrix Result = new(a.Rows, a.Columns);

      for (int y = 0; y < a.Rows; y++)
      {
        for (int x = 0; x < a.Columns; x++)
        {
          Result[y, x] = Math.Ceiling(a[y, x]);
        }
      }

      return Result;
    }

    ///  <summary>
    ///    Calculate determinant.
    ///  </summary>
    ///  <summary>
    ///    The determinant is scalar value that can be calculated from the elements of the matrix.
    ///  </summary>
    ///  <summary>
    ///    It's denoted by the symbol det(A) or sometimes by vertical bars around the matrix, such as ∣A∣.
    ///  </summary>
    ///  <summary>
    ///   # Be aware. If is not square matrix then returns 'double.NaN'.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>double value</returns>
    public static double Determinant(Matrix a)
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
        Matrix Temp = new(size - 1);

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

            result += a[0, n] * (n % 2 == 0 ? 1 : -1) * Determinant(Temp);
        }

        return result;
    }

    ///  <summary>
    ///   A method that returns the reverse (or inverse) of a matrix.
    ///  </summary>
    ///  <summary>
    ///   # Be aware. Method throw exception if matrix don't have determinant.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Matrix</returns>
    public static Matrix Reverse(Matrix a)
    {
        if (double.IsNaN(Matrix.Determinant(a)))
        {
          throw new ArgumentException("Can't get determinant. It's not square matrix");
        }

        uint size = a.Columns;
        double determinant = Matrix.Determinant(a);

        Matrix Result = new(size);
        Matrix Temp = new(size - 1);

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

                Result[j, i] = ((i + j) % 2 == 0 ? 1 : -1) * Matrix.Determinant(Temp);
            }
        }

        return Result * Math.Round(1 / determinant, 3);
    }

    public double this[int row, int column]
    {
        get { return _array[row, column]; }
        set { _array[row, column] = value; }
    }

    ///  <summary>
    ///   Generate matrix with random data.  
    ///  </summary>
    ///  <param name="min">Min value in range</param>
    ///  <param name="max">Max value in range</param>
    ///  <returns>New matrix filled with random data.</returns>
    public Matrix Randomize(int min = 0, int max = 0)
    {
        Matrix Result = new(Rows, Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Result[y, x] = new Random().NextInt64(min, max);
            }
        }

        return Result;
    }

    ///  <summary>
    ///   Add two matrices.
    ///  </summary>
    ///  <summary>
    ///   # Be aware if matrices don't same size then method throw exception.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Return sum of 2 matrices</returns>
    public Matrix Sum(Matrix a)
    {
        if (IsEqualSize(a) == false)
        {
          throw new ArgumentException("Don't same size with matrix");
        }

        Matrix Result = new(a.Rows, a.Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Result[y, x] = _array[y, x] + a[y, x];
            }
        }

        return Result;
    }

    ///  <summary>
    ///   Multiply matrix by value.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Return multiplication matrix by value</returns>
    public Matrix Mul(double a)
    {
        Matrix Result = new(Rows, Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Result[y, x] = _array[y, x] * a;
            }
        }

        return Result;
    }

    ///  <summary>
    ///   Multiply 2 matrices.
    ///  </summary>
    ///  <summary>
    ///   # Be aware if 'Columns' first matrix != 'Rows' in second matrix then method throw exception.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Return multiplication of 2 matrices</returns>
    public Matrix Mul(Matrix a)
    {
        if (Columns != a.Rows)
        {
            throw new ArgumentException("Matrices are inconsistent(ColumnLength != a.RowLength)");
        }

        Matrix Result = new(Rows, a.Columns);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < a.Columns; x++)
            {
                for (int n = 0; n < Columns; n++)
                {
                    Result[y, x] += _array[y, n] * a[n, x];
                }
            }
        }

        return Result;
    }

    ///  <summary>
    ///    Add two matrices.
    ///  </summary>
    ///  <param name="a">Object of 'Matrix' class</param>
    ///  <returns>Return substraction of 2 matrices</returns>
    public Matrix Sub(Matrix a) => Sum(a.Mul(-1));

    ///  <summary>
    ///    Return matrix as a string.
    ///  </summary>
    public override string ToString()
    {
        StringBuilder result = new("[");

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
              if (x == Columns - 1)
              {
                result.Append(_array[y, x] + "]");
              }
              else
              {
                result.Append(_array[y, x] + ", ");
              }
            }

            if (y != Rows - 1)
            {
              result.Append("\n[");
            }
        }

        return result.ToString();
    }
}

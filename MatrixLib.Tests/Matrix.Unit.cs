namespace MatrixLib.Tests;

public class Tests
{
  [TestCaseSource(typeof(Data), nameof(Data.Squares))]
  public bool IsSquare(Matrix a)
    => Matrix.IsSquare(a);

  [TestCaseSource(typeof(Data), nameof(Data.EqualSizes))]
  public bool IsEqualSize(Matrix a, Matrix b)
    => a.IsEqualSize(b);

  [TestCaseSource(typeof(Data), nameof(Data.Determinants))]
  public double Determinant(Matrix a) 
    => Matrix.Determinant(a);

  [TestCaseSource(typeof(Data), nameof(Data.Sums))]
  public double[,] Sum(Matrix a, Matrix b)
    => Matrix.Raw(Matrix.Round(a + b));

  [TestCaseSource(typeof(Data), nameof(Data.Subs))]
  public double[,] Sub(Matrix a, Matrix b)
    => Matrix.Raw(Matrix.Round(a - b));

  [TestCaseSource(typeof(Data), nameof(Data.MulsByValue))]
  public double[,] MulByValue(Matrix a, double b)
    => Matrix.Raw(Matrix.Round(a * b));

  [TestCaseSource(typeof(Data), nameof(Data.Muls))]
  public double[,] Mul(Matrix a, Matrix b)
    => Matrix.Raw(Matrix.Round(a * b));

  [TestCaseSource(typeof(Data), nameof(Data.Strings))]
  public string String(Matrix a) => a.ToString();
}

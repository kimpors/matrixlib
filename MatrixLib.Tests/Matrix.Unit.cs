namespace MatrixLib.Tests;

public class Tests
{
  [TestCase((uint)0, (uint)0, ExpectedResult = true)]
  [TestCase((uint)1, (uint)1, ExpectedResult = true)]
  [TestCase((uint)2, (uint)3, ExpectedResult = false)]
  public bool IsSquare(uint rows, uint columns)
  {
    return Matrix.IsSquare(new(rows, columns));
  }

  [TestCase((uint)0, (uint)0, (uint)0, (uint)0, ExpectedResult = true)]
  [TestCase((uint)1, (uint)1, (uint)2, (uint)2, ExpectedResult = false)]
  public bool IsEqualSize(uint aRows, uint aColumns, uint bRows, uint bColumns)
  {
    return new Matrix(aRows, aColumns).IsEqualSize(new Matrix(bRows, bColumns));
  }
}

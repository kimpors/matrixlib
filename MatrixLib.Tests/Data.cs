using System.Collections;

namespace MatrixLib.Tests;

public static class Data
{
  private static readonly Matrix[] _matrices1;
  private static readonly Matrix[] _matrices2;
  private static readonly Matrix[] _matrices3;

  static Data()
  {
    _matrices1 = new Matrix[]
    {
        new Matrix(new double[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}),
        new Matrix(new double[,] {{1, 2, 2}, {2, 1, 2}, {2, 2, 1}}),
        new Matrix(new double[,] {{1.5, 1.5, 1.5}, {2.5, 2.5, 2.5}, {3.5, 3.5, 3.5}}),
        new Matrix (new double[,] {{25.5, 100, 25}, {11, 4, 45}, {34, 2, 123}})
    };

    _matrices2 = new Matrix[]
    {
        new Matrix(new double[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}),
        new Matrix(new double[,] {{3, 3, 3}, {4, 4, 4}, {5, 5, 5}}),
        new Matrix(new double[,] {{0.25, 0.3, 0.4}, {0.9, 0.7, 1.1}, {3.2, 12.2, 5.3}}),
        new Matrix(new double[,] {{100, 2500, 7800}, {12.5, 50000, 9.8}, {39, 1230, 444.3}})
    };

    _matrices3 = new Matrix[]
    {
        new Matrix(new double[,] {{1.5, 1.5, 1.5}, {2.4, 2.4, 2.4}, {3.6, 3.6, 3.6}}),
        new Matrix(new double[,] {{0.5, 0.15, 0.25}, {0.35, 0.45, 0.65}, {0.75, 0.85, 0.95}}),
        new Matrix(new double[,] {{0.005, 0.0150, 0.0250}, {0.0450, 0.0650, 1.805}, {0.752, 0.070, 0.340}}),
        new Matrix(new double[,] {{250.505, 9009.1150, 32.025}, {80_000.750, 400.65, 0.85}, {130.52, 58.7, 909.14}}),
    };
  }

  public static IEnumerable Squares
  {
    get
    {
      yield return new TestCaseData(new Matrix(0)).Returns(true);
      yield return new TestCaseData(new Matrix(1)).Returns(true);
      yield return new TestCaseData(new Matrix(2, 3)).Returns(false);
      yield return new TestCaseData(new Matrix(5, 1)).Returns(false);
    }
  }

  public static IEnumerable EqualSizes
  {
    get
    {
      yield return new TestCaseData(new Matrix(1), new Matrix(2)).Returns(false);
      yield return new TestCaseData(new Matrix(5, 3), new Matrix(3, 5)).Returns(false);
      yield return new TestCaseData(new Matrix(2, 3), new Matrix(2, 3)).Returns(true);
    }
  }

  public static IEnumerable Determinants
  {
    get
    {
      yield return new TestCaseData(_matrices1[0]).Returns(0);
      yield return new TestCaseData(_matrices1[1]).Returns(5);
      yield return new TestCaseData(_matrices1[2]).Returns(0);
      yield return new TestCaseData(_matrices1[3]).Returns(25101);
    }
  }

  public static IEnumerable Sums
  {
    get
    {
      yield return new TestCaseData(_matrices1[0], _matrices2[0]).Returns(new double[,] {{2, 2, 2}, {2, 2, 2}, {2, 2, 2}});
      yield return new TestCaseData(_matrices1[1], _matrices2[1]).Returns(new double[,] {{4, 5, 5}, {6, 5, 6}, {7, 7, 6}});
      yield return new TestCaseData(_matrices1[2], _matrices2[2]).Returns(new double[,] {{1.75, 1.8, 1.9}, {3.4, 3.2, 3.6}, {6.7, 15.7, 8.8}});
      yield return new TestCaseData(_matrices1[3], _matrices2[3]).Returns(new double[,] {{125.5, 2600, 7825}, {23.5, 50004, 54.8}, {73, 1232, 567.3}});
    }
  }

  public static IEnumerable Subs
  {
    get
    {
      yield return new TestCaseData(_matrices1[0], _matrices2[0]).Returns(new double[,] {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}});
      yield return new TestCaseData(_matrices1[1], _matrices2[1]).Returns(new double[,] {{-2, -1, -1}, {-2, -3, -2}, {-3, -3, -4}});
      yield return new TestCaseData(_matrices1[2], _matrices2[2]).Returns(new double[,] {{1.25, 1.2, 1.1}, {1.6, 1.8, 1.4}, {0.3, -8.7, -1.8}});
      yield return new TestCaseData(_matrices1[3], _matrices2[3]).Returns(new double[,] {{-74.5, -2400, -7775}, {-1.5, -49996, 35.2}, {-5, -1228, -321.3}});
    }
  }

  public static IEnumerable MulsByValue
  {
    get
    {
      yield return new TestCaseData(_matrices1[0], 1d).Returns(new double[,] {{1, 1, 1}, {1, 1, 1},  {1, 1, 1}});
      yield return new TestCaseData(_matrices1[1], 5d).Returns(new double[,] {{5, 10, 10}, {10, 5, 10}, {10, 10, 5}});
      yield return new TestCaseData(_matrices1[2], 2.5d).Returns(new double[,] {{3.75, 3.75, 3.75}, {6.25, 6.25, 6.25}, {8.75, 8.75, 8.75}});
      yield return new TestCaseData(_matrices1[3], 0.3d).Returns(new double[,] {{7.65, 30, 7.5}, {3.3, 1.2, 13.5}, {10.2, 0.6, 36.9}});
    }
  }

  public static IEnumerable Muls
  {
    get
    {
      yield return new TestCaseData(_matrices1[0], _matrices2[0]).Returns(new double[,] {{3, 3, 3}, {3, 3, 3},  {3, 3, 3}});
      yield return new TestCaseData(_matrices1[1], _matrices2[1]).Returns(new double[,] {{21, 21, 21}, {20, 20, 20}, {19, 19, 19}});
      yield return new TestCaseData(_matrices1[2], _matrices2[2]).Returns(new double[,] {{6.52, 19.8, 10.2}, {10.88, 33, 17}, {15.23, 46.2, 23.8}});
      yield return new TestCaseData(_matrices1[3], _matrices2[3]).Returns(new double[,] {{4775, 5094500, 210987.5}, {2905, 282850, 105832.7}, {8222, 336290, 319868.5}});

    }
  }

  public static IEnumerable Strings
  {
    get
    {
      yield return new TestCaseData(_matrices1[0]).Returns("[1, 1, 1]\n[1, 1, 1]\n[1, 1, 1]");
      yield return new TestCaseData(_matrices1[1]).Returns("[1, 2, 2]\n[2, 1, 2]\n[2, 2, 1]");
      yield return new TestCaseData(_matrices1[2]).Returns("[1.5, 1.5, 1.5]\n[2.5, 2.5, 2.5]\n[3.5, 3.5, 3.5]");
      yield return new TestCaseData(_matrices1[3]).Returns("[25.5, 100, 25]\n[11, 4, 45]\n[34, 2, 123]");
    }
  }

  public static IEnumerable Rounds
  {
    get
    {
      yield return new TestCaseData(_matrices3[0]).Returns(new double[,] {{2, 2, 2}, {2, 2, 2}, {4, 4, 4}});
      yield return new TestCaseData(_matrices3[1]).Returns(new double[,] {{0, 0, 0}, {0, 0, 1}, {1, 1, 1}});
      yield return new TestCaseData(_matrices3[2]).Returns(new double[,] {{0, 0, 0}, {0, 0, 2}, {1, 0, 0}});
      yield return new TestCaseData(_matrices3[3]).Returns(new double[,] {{251, 9009, 32}, {80_001, 401, 1}, {131, 59, 909}});
    }
  }

  public static IEnumerable Floors
  {
    get
    {
      yield return new TestCaseData(_matrices3[0]).Returns(new double[,] {{1, 1, 1}, {2, 2, 2}, {3, 3, 3}});
      yield return new TestCaseData(_matrices3[1]).Returns(new double[,] {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}});
      yield return new TestCaseData(_matrices3[2]).Returns(new double[,] {{0, 0, 0}, {0, 0, 1}, {0, 0, 0}});
      yield return new TestCaseData(_matrices3[3]).Returns(new double[,] {{250, 9009, 32}, {80_000, 400, 0}, {130, 58, 909}});
    }
  }

  public static IEnumerable Ceils
  {
    get
    {
      yield return new TestCaseData(_matrices3[0]).Returns(new double[,] {{2, 2, 2}, {3, 3, 3}, {4, 4, 4}});
      yield return new TestCaseData(_matrices3[1]).Returns(new double[,] {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}});
      yield return new TestCaseData(_matrices3[2]).Returns(new double[,] {{1, 1, 1}, {1, 1, 2}, {1, 1, 1}});
      yield return new TestCaseData(_matrices3[3]).Returns(new double[,] {{251, 9010, 33}, {80_001, 401, 1}, {131, 59, 910}});
    }
  }
}

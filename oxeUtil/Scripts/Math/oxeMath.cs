using UnityEngine;

public static class oxeMath {

  public static int rootLinear(float A, float B, out float root) {
    root = float.NaN;
    if (Mathf.Approximately(A, 0.0f)) {
      return 0;
    }
    root = -B / A;
    return 1;
  }

  // quadratic solver.
  // returns the number of real roots found
  public static int rootsQuadratic(float A, float B, float C, out float root1, out float root2) {
    root1 = float.NaN;
    root2 = float.NaN;
    
    // the quadratic is invalid to use for linear equations.
    if (Mathf.Approximately(A, 0.0f)) {
      return rootLinear(B, C, out root1);
    }
    
    float BSquaredMinus4AC = (B * B) - (4.0f * A * C);

    if (BSquaredMinus4AC < 0.0f) {
      // all solutions are imaginary
      return 0;
    }
    
    root1 = (-B + Mathf.Sqrt(BSquaredMinus4AC)) / (2.0f * A);

    if (Mathf.Approximately(BSquaredMinus4AC, 0.0f)) {
      return 1;
    }
    

    root2 = (-B - Mathf.Sqrt(BSquaredMinus4AC)) / (2.0f * A);
    
    return 2;
  }

}

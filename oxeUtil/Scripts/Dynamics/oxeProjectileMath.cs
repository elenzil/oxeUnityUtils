using UnityEngine;

public static class oxeProjectileMath {
  /// <summary>
  /// calculate the velocity vector for a projectile to hit a moving target.
  /// the target has constant velocity.
  /// the projectile has constant speed.
  /// from https://docs.google.com/document/d/1VBvFU6tPs_IUeGfhodQoKFUpxrrfzFa4Vpg9K3UAXlY/edit#heading=h.ozmaxtv0ti3g
  /// </summary>
  /// <returns><c>true</c>, if a collision course is possible, <c>false</c> otherwise.</returns>
  /// <param name="targPos">initial position of the target</param>
  /// <param name="targVel">velocity of the target</param>
  /// <param name="projPos">initial position of the projectile</param>
  /// <param name="projSpd">speed of the projectile</param>
  /// <param name="projVel">(out) the resulting velocity of the projectile, if any</param>
  public static bool linearCollisionCourse(Vector3 targPos, Vector3 targVel, Vector3 projPos, float projSpd, out Vector3 projVel) {
    projVel = Vector3.zero;

    Vector3 Kp = targPos - projPos;

    float a = (targVel.x * targVel.x) + (targVel.y * targVel.y) + (targVel.z * targVel.z) - (projSpd * projSpd);
    float b = ((Kp.x * targVel.x) + (Kp.y * targVel.y) + (Kp.z * targVel.z)) * 2f;
    float c = (Kp.x * Kp.x) + (Kp.y * Kp.y) + (Kp.z * Kp.z);

    float BSquaredMinus4AC = (b * b) - (4f * a * c);

    if (BSquaredMinus4AC < 0f) {
      // all solutions are imaginary
      return false;
    }

    float t1 = (-b + Mathf.Sqrt(BSquaredMinus4AC)) / (2f * a);
    float t2 = (-b - Mathf.Sqrt(BSquaredMinus4AC)) / (2f * a);
    float t = float.NaN;

    t = Mathf.Min(t1, t2);
    if (t < 0) {
      t = Mathf.Max(t1, t2);
    }

    if (t < 0) {
      // both solutions are negative
      return false;
    }

    projVel = (Kp + (targVel * t)) / t;

    return true;
  }
}

using UnityEngine;

public class oxeRotater : MonoBehaviour {

  public Vector3 axis    = Vector3.up;
  public float   deg_sec = 6.0f;
  public bool    active  = true;

	void Update () {
    if (active) {
      transform.Rotate(axis, deg_sec * Time.deltaTime);
    }
	}
}

using UnityEngine;
using System.Collections;

public class floatsController : MonoBehaviour {

  void Start() {
    floatTest();
  }

  void floatTest() {
    float a = 1.5f;
    float b = 0.3f;
    float c = 5.0f;
    float d = 5.00001f;
    Debug.Log("(1.5f / 0.3f) == " + c + ": " + ((a / b) == c));
    Debug.Log("Mathf.Approximately(1.5f / 0.3f, 5.0f):    " + Mathf.Approximately(a / b, c));
    Debug.Log("Mathf.Approximately(1.5f / 0.3f, 5.0001f): " + Mathf.Approximately(a / b, d));

    /*
    ouput:
    (1.5f / 0.3f) == 5: False
    Mathf.Approximately(1.5f / 0.3f, 5.0f):    True
    Mathf.Approximately(1.5f / 0.3f, 5.0001f): False
    */
  }
}

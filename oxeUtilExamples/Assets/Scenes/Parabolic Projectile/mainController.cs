using UnityEngine;
using UnityEngine.UI;

public class mainController : MonoBehaviour {
  public Slider sliderTheta;
  public Slider sliderPosX;
  public Slider sliderPosY;
  public Slider sliderSpeed;

  public GameObject speedInvalid;

  public Button buttonQuestion;
  public Button buttonAnswer;

  public Transform targ;
  public Transform launcher;

  public Transform tragectoryContainer;
  public Transform tragectoryMarker;
  public int       numTragectoryMarkers = 20;

  private const float G       =  9.8f;
  private const float simTime = 30f;

  void Start() {
    for (int n = 0; n < numTragectoryMarkers; ++n) {
      Transform t = GameObject.Instantiate<Transform>(tragectoryMarker);
      t.SetParent(tragectoryContainer);
      t.localScale = Vector3.one;
    }

    sliderTheta.onValueChanged.AddListener(onChangedIndependentVariable);
    sliderPosX .onValueChanged.AddListener(onChangedIndependentVariable);
    sliderPosY .onValueChanged.AddListener(onChangedIndependentVariable);

    buttonQuestion.onClick.AddListener(onClickToggleAnswer);
    buttonAnswer  .onClick.AddListener(onClickToggleAnswer);

    Update();
    recalculateSpeed();
  }

  void Update() {
    updateUI();
    resimulate();
  }

  void updateUI() {
//    float f = Time.time * 5f;

//    sliderPosX.value += Mathf.Cos(f) * 1f;
//    sliderPosY.value += Mathf.Sin(f) * 1f;

    launcher.localEulerAngles = sliderTheta.value * Vector3.forward;
    targ    .localPosition    = new Vector2(sliderPosX.value, sliderPosY.value);
  }

  void onChangedIndependentVariable(float unused) {
    recalculateSpeed();
  }

  void onClickToggleAnswer() {
    buttonAnswer.gameObject.SetActive(!buttonAnswer.gameObject.activeSelf);
  }

  void recalculateSpeed() {
    // speed = +/- sqrt( GX^2 / (2C * (SX - CY)) )
    // where
    // X, Y = location of target
    // C, S = cosine & sine of theta
    // G = gravity

    float theta = sliderTheta.value * Mathf.Deg2Rad;
    float C     = Mathf.Cos(theta);
    float S     = Mathf.Sin(theta);
    float X     = targ.localPosition.x;
    float Y     = targ.localPosition.y;

    float speed = float.NaN;

    float C2_times_SX_minus_CY = 2f * C * ((S * X) - (C * Y));
    float G_times_X_SQUARED    = G * X * X;


    if (Mathf.Approximately(C2_times_SX_minus_CY, 0f)) {
      // divide by zero
    }
    else if (C2_times_SX_minus_CY < 0f) {
      // imaginary answer
    }
    else {
      // hokay
      speed = Mathf.Sqrt(G_times_X_SQUARED / C2_times_SX_minus_CY);
    }

//    Debug.Log("speed = " + speed.ToString("0.0"));

    if (!float.IsNaN(speed)) {
      sliderSpeed.value = speed;
      speedInvalid.SetActive(false);
    }
    else {
      speedInvalid.SetActive(true);
    }
  }

  void resimulate() {
    float  t = 0;
    float dt = simTime/(float)tragectoryContainer.childCount;

    float speed = sliderSpeed.value;
    float theta = sliderTheta.value * Mathf.Deg2Rad;
    float V0x = Mathf.Cos(theta) * speed;
    float V0y = Mathf.Sin(theta) * speed;

    for (int n = 0; n < tragectoryContainer.childCount; ++n) {
      float x = V0x * t;
      float y = V0y * t - 0.5f * G * t * t;

      tragectoryContainer.GetChild(n).localPosition = new Vector2(x, y);

      t += dt;
    }
  }
}

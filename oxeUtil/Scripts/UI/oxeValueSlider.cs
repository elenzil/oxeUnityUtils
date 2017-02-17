using UnityEngine;
using UnityEngine.UI;

public class oxeValueSlider : MonoBehaviour {
  public Slider slider;
  public Text   text;

  void Start() {
    slider.onValueChanged.AddListener(onChangedSlider);
    updateValue();
  }

  void onChangedSlider(float unused) {
    updateValue();
  }

  void updateValue() {
    text.text = slider.value.ToString("0.0");
  }
}

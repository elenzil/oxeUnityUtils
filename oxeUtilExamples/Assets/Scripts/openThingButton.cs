using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Button))]
public class openThingButton : MonoBehaviour {
  public string  theThingName;
  public OpenWhat typeOfThing;

  private Button theButton;

  public enum OpenWhat {
    scene,
    url,
  }

  void Start() {
    theButton = GetComponent<Button>();
    if (theButton == null) {
      Debug.LogError("no button component. shutting down.");
      enabled = false;
      return;
    }

    theButton.onClick.AddListener(onClickTheButton);
  }

  void onClickTheButton() {
    if (string.IsNullOrEmpty(theThingName)) {
      Debug.LogError("no thing specified");
      return;
    }

    switch (typeOfThing) {
      default: {
        Debug.LogError("unhandled type of thing: " + typeOfThing.ToString());
        break;
      }
      case OpenWhat.scene: {
        SceneManager.LoadScene(theThingName, LoadSceneMode.Single);
        break;
      }
      case OpenWhat.url: {
        Application.OpenURL(theThingName);
        break;
      }
    }
  }
}

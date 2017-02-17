using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class oxeDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

  private Vector3 dPos;
  private Transform trans;

  void Start() {
    trans = transform;
  }

  public void OnBeginDrag(PointerEventData ped) {
    Vector3 v = Camera.main.ScreenToWorldPoint(ped.position);
    dPos = trans.position - v;
  }

  public void OnDrag(PointerEventData ped) {
    updatePos(ped);
  }

  public void OnEndDrag(PointerEventData ped) {
    updatePos(ped);
  }

  void updatePos(PointerEventData ped) {
    Vector3 v = Camera.main.ScreenToWorldPoint(ped.position);
    trans.position = v + dPos;
  }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class projectileController : MonoBehaviour {
  public Transform     flyingTarget;
  public Transform     flyingProjectile;
  public Transform     markerTarget;
  public Transform     markerTargetVelocity;
  public Transform     markerProjectile;
  public Transform     markerProjectileSpeed;
  public RectTransform indicatorProjectileSpeed;
  public Text          textTime;
  public RectTransform canvas;
  public Transform     pathContainer;

  private Vector3 targetVelocity;
  private Vector3 projectileVelocity;
  private float   projectileSpeed;

  private const float timeSpan        = 4f;
  private const int   numPathMarkers  = 80;
  private const float pathMarkerScale = 0.3f;

  private float simStartTime;

  private List<Transform> pathMarkersTarget     = new List<Transform>();
  private List<Transform> pathMarkersProjectile = new List<Transform>();


  void Start() {
    SimTime = 0;

    for (int n = 0; n < numPathMarkers; ++n) {
      Transform tt = Instantiate<Transform>(flyingTarget    );
      Transform tp = Instantiate<Transform>(flyingProjectile);
      tt.SetParent(pathContainer);
      tp.SetParent(pathContainer);
      tt.SetAsFirstSibling();
      tp.SetAsFirstSibling();
      tt.localScale = Vector3.one * pathMarkerScale;
      tp.localScale = Vector3.one * pathMarkerScale;
      pathMarkersTarget    .Add(tt);
      pathMarkersProjectile.Add(tp);
    }
  }

  void Update() {
    recalcStuff();

    if (SimTime > timeSpan) {
      SimTime = 0;
    }

    float t = SimTime;

    flyingTarget    .position = markerTarget    .position + (targetVelocity     * t);
    flyingProjectile.position = markerProjectile.position + (projectileVelocity * t);

    textTime.text = t.ToString("0.0");

    indicatorProjectileSpeed.offsetMin = Vector2.one * -1f * projectileSpeed / canvas.localScale.x;
    indicatorProjectileSpeed.offsetMax = Vector2.one *  1f * projectileSpeed / canvas.localScale.y;

    for (int n = 0; n < numPathMarkers; ++n) {
      float f = timeSpan * (float)n / (float)(numPathMarkers - 1);
      pathMarkersTarget    [n].position = markerTarget    .position + (targetVelocity     * f);
      pathMarkersProjectile[n].position = markerProjectile.position + (projectileVelocity * f);
    }
  }

  float SimTime {
    get {
      return Time.time - simStartTime;
    }
    set {
      simStartTime = Time.time + value;
    }
  }

  void recalcStuff() {
    targetVelocity  =  markerTargetVelocity .position - markerTarget    .position;
    projectileSpeed = (markerProjectileSpeed.position - markerProjectile.position).magnitude;
    projectileVelocity = new Vector3(1, 1, 0) * projectileSpeed;

    bool succ = oxeProjectileMath.linearCollisionCourse(markerTarget.position, targetVelocity, markerProjectile.position, projectileSpeed, out projectileVelocity);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
  public int start;
  public int delay;

  int currentDelay;
  Vector3 origin;

  void Start()
  {
    currentDelay = delay;
    origin = transform.localPosition;
    transform.localPosition = new Vector3(start, origin.y, origin.z);
  }

  void FixedUpdate()
  {
    if(currentDelay > 0) currentDelay --;
    else transform.localPosition = Vector3.Lerp(transform.localPosition, origin, 0.1f);

    if(transform.localPosition.x + 0.1f > origin.x
    && transform.localPosition.x - 0.1f < origin.x) Destroy(this);
  }
}

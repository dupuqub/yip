using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
  public int delay;
  public int start;
  public int end;

  int currentDelay;
  Vector3 origin;
  Vector3 future;

  void Start()
  {
    currentDelay = delay;
    origin = transform.localPosition;
    future = new Vector3(end, origin.y, origin.z);
    transform.localPosition = new Vector3(start, origin.y, origin.z);
  }

  void FixedUpdate()
  {
    // Move.
    if(currentDelay > 0) currentDelay --;
    else transform.localPosition = Vector3.Lerp(transform.localPosition, future, 0.1f);

    // End.
    if(transform.localPosition.x + 0.1f > end
    && transform.localPosition.x - 0.1f < end) Destroy(this);
  }
}

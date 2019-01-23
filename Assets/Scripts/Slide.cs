using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
  public int start;
  public int delay;

  int currentDelay = 0;

  Vector3 origin;
  Vector3 oldPos;
  Vector3 newPos;

  void Start()
  {
    currentDelay = delay;
    origin = transform.localPosition;
    transform.localPosition = new Vector3(start, origin.y, origin.z);
  }

  void FixedUpdate()
  {
    if(currentDelay > 0) currentDelay --;
    else
    {
      newPos = origin;
      oldPos = transform.localPosition;
      transform.localPosition = Vector3.Lerp(oldPos, newPos, 0.1f);
    }

    if(transform.localPosition.x + 1f > origin.x
    && transform.localPosition.x - 1f < origin.x) Destroy(this, 0);
  }
}

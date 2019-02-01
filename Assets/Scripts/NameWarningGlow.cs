using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameWarningGlow : MonoBehaviour
{
  public float alpha; // From 0 to 1.
  public int index; // From 0 to 3.

  void Update()
  {
    Vector3 outsidePosition = new Vector3(0f, 580f, 0f);
    Vector3 insidePosition = Vector3.zero;

         if(index == 0) insidePosition = new Vector3(272f, 41f, 0f);
    else if(index == 1) insidePosition = new Vector3(182f, -109f, 0f);
    else if(index == 2) insidePosition = new Vector3(92f, -259f, 0f);
    else if(index == 3) insidePosition = new Vector3(2f, -409f, 0f);

    if(alpha > 0)
    {
      alpha -= 0.05f;
      transform.localPosition = insidePosition;
      GetComponent<CanvasRenderer>().SetAlpha(alpha);
    }

    else transform.localPosition = outsidePosition;
  }
}

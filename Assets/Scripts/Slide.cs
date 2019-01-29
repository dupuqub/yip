using UnityEngine;

public class Slide : MonoBehaviour
{
  public int delay;
  public bool move;
  public bool hiding;
  public Vector3 hide;
  public Vector3 show;

  int delayNow;
  Vector3 target;

  void Start()
  {
    transform.localPosition = hide;
    delayNow = delay;
    hiding = true;
  }

  void FixedUpdate()
  {
    if(move)
    {
      target = hiding ? show : hide;

      // Move.
      if(delayNow > 0) delayNow --;
      else transform.localPosition = Vector3.Lerp(transform.localPosition, target, 0.1f);

      // End.
      if(transform.localPosition.x + 0.1f > target.x
      && transform.localPosition.x - 0.1f < target.x
      && transform.localPosition.y + 0.1f > target.y
      && transform.localPosition.y - 0.1f < target.y
      && transform.localPosition.z + 0.1f > target.z
      && transform.localPosition.z - 0.1f < target.z)
      {
        move = false;
        hiding = !hiding;
      }
    }
  }
}

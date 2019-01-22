using UnityEngine;

public class GamepadXbox360
{
  // Colors.
  public bool A = Input.GetButton("A");
  public bool B = Input.GetButton("B");
  public bool X = Input.GetButton("X");
  public bool Y = Input.GetButton("Y");

  // Options.
  public bool BACK = Input.GetButton("BACK");
  public bool START = Input.GetButton("START");

  // Sticks.
  public bool LSB = Input.GetButton("LSB");
  public bool RSB = Input.GetButton("RSB");
  public float LSX = Input.GetAxis("LSX"); // -1 to 1.
  public float LSY = Input.GetAxis("LSY"); // -1 to 1.
  public float RSX = Input.GetAxis("RSX"); // -1 to 1.
  public float RSY = Input.GetAxis("RSY"); // -1 to 1.

  // Uppers.
  public bool LB = Input.GetButton("LB");
  public bool RB = Input.GetButton("RB");
  public float LT = Input.GetAxis("LT"); // 0 to 1.
  public float RT = Input.GetAxis("RT"); // 0 to 1.

  // Arrows.
  public float PADX = Input.GetAxis("PADX"); // -1 to 1.
  public float PADY = Input.GetAxis("PADY"); // -1 to 1.
}

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMain : MonoBehaviour
{
  // "Input Field" breaks Unity's UI navigation's flow.
  // Every scene with "Input Field" must therefore have custom UI navigation.

  int delay = 20;
  int currentDelay = 0;

  void Update()
  {
    // Navigation.
    float PADX = Input.GetAxis("PADX");
    float PADY = Input.GetAxis("PADY");
    float KEYX = Input.GetAxis("KEYX");
    float KEYY = Input.GetAxis("KEYY");

    bool LEFT = PADX < 0 || KEYX < 0;
    bool RIGHT = PADX > 0 || KEYX > 0;
    bool DOWN = PADY < 0 || KEYY < 0;
    bool UP = PADY > 0 || KEYY > 0;

    bool navigating = PADX != 0 || PADY != 0 || KEYY != 0 || KEYX != 0;
    GameObject current = EventSystem.current.currentSelectedGameObject;

    // Gamepad and keyboard behaviour.
    if(!navigating) currentDelay = 0;
    else if(currentDelay > 0) currentDelay --;
    else
    {
      currentDelay = delay;

      //........................................................................
      if(current == null) Tools.UISelect("Settings");

      //........................................................................
      else if(current.name == "Settings")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Credits");
        else if(UP) Tools.UISelect("Exit");
      }

      //........................................................................
      else if(current.name == "Credits")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Exit");
        else if(UP) Tools.UISelect("Settings");
      }

      else if(current.name == "Exit")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Settings");
        else if(UP) Tools.UISelect("Credits");
      }

      //........................................................................
      else if(current.name.Substring(0, 5) == "Input")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Settings");
        else if(RIGHT) Tools.UISelect("Play" + index);
        else if(DOWN) Tools.UISelect("Input" + indexDown.ToString());
        else if(UP) Tools.UISelect("Input" + indexUp.ToString());
      }

      //........................................................................
      else if(current.name.Substring(0, 4) == "Play")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Input" + index);
        else if(RIGHT) Tools.UISelect("Erase" + index);
        else if(DOWN) Tools.UISelect("Play" + indexDown.ToString());
        else if(UP) Tools.UISelect("Play" + indexUp.ToString());
      }

      //........................................................................
      else if(current.name.Substring(0, 5) == "Erase")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Play" + index);
        else if(DOWN) Tools.UISelect("Erase" + indexDown.ToString());
        else if(UP) Tools.UISelect("Erase" + indexUp.ToString());
      }
    }
  }
}

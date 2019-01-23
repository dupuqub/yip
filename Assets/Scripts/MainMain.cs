using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMain : MonoBehaviour
{
  // "Input Field" breaks Unity's UI navigation's flow.
  // Every scene containing "Input Field" must therefore have custom UI navigation.

  int delay = 20;
  int currentDelay = 0;

  void Update()
  {
    float PADX = Input.GetAxis("PADX");
    float PADY = Input.GetAxis("PADY");
    float KEYX = Input.GetAxis("KEYX");
    float KEYY = Input.GetAxis("KEYY");
    float MOUSEX = Input.GetAxis("MOUSEX");
    float MOUSEY = Input.GetAxis("MOUSEY");

    bool LEFT = PADX < 0 || KEYX < 0;
    bool RIGHT = PADX > 0 || KEYX > 0;
    bool DOWN = PADY < 0 || KEYY < 0;
    bool UP = PADY > 0 || KEYY > 0;

    bool navigating = PADX != 0 || PADY != 0 || KEYY != 0 || KEYX != 0;
    GameObject current = EventSystem.current.currentSelectedGameObject;

    if(MOUSEX != 0 || MOUSEY != 0) Tools.UISelect("First");

    if(currentDelay > 0) currentDelay --;

    else if(navigating)
    {
      if(current.name == "First")
      {
        currentDelay = delay;
        Tools.UISelect("Settings");
      }
      else if(current.name == "Settings")
      {
        currentDelay = delay;
        if(RIGHT) Tools.UISelect("Input0");
        if(DOWN) Tools.UISelect("Credits");
        if(UP) Tools.UISelect("Exit");
      }
      else if(current.name == "Credits")
      {
        currentDelay = delay;
        if(RIGHT) Tools.UISelect("Input0");
        if(DOWN) Tools.UISelect("Exit");
        if(UP) Tools.UISelect("Settings");
      }
      else if(current.name == "Exit")
      {
        currentDelay = delay;
        if(RIGHT) Tools.UISelect("Input0");
        if(DOWN) Tools.UISelect("Settings");
        if(UP) Tools.UISelect("Credits");
      }
      else if(current.name.Substring(0, 5) == "Input")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

        currentDelay = delay;
        if(LEFT) Tools.UISelect("Settings");
        if(RIGHT) Tools.UISelect("Play" + index);
        if(DOWN) Tools.UISelect("Input" + indexDown.ToString());
        if(UP) Tools.UISelect("Input" + indexUp.ToString());
      }
      else if(current.name.Substring(0, 4) == "Play")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

        currentDelay = delay;
        if(LEFT) Tools.UISelect("Input" + index);
        if(RIGHT) Tools.UISelect("Erase" + index);
        if(DOWN) Tools.UISelect("Play" + indexDown.ToString());
        if(UP) Tools.UISelect("Play" + indexUp.ToString());
      }
      else if(current.name.Substring(0, 5) == "Erase")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

        currentDelay = delay;
        if(LEFT) Tools.UISelect("Play" + index);
        if(DOWN) Tools.UISelect("Erase" + indexDown.ToString());
        if(UP) Tools.UISelect("Erase" + indexUp.ToString());
      }
    }
  }
}

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainSketch : MonoBehaviour
{
  void Start()
  {
    // Continue old save.
    if(Tools.FileExists("World.json"))
    {
    }

    // Start new save.
    else
    {
    }
  }

  void Update()
  {
    // Adapt to mouse behaviour.
    if(Input.GetAxis("MOUSEX") != 0
    || Input.GetAxis("MOUSEY") != 0)
    {
      EventSystem.current.SetSelectedGameObject(null);
    }

    // Adapt to gamepad behaviour.
    if(Input.GetAxis("PADX") != 0
    || Input.GetAxis("PADY") != 0)
    {
      if(EventSystem.current.currentSelectedGameObject == null)
      {
        GameObject settings = GameObject.Find("SettingsButton");
        EventSystem.current.SetSelectedGameObject(settings);
      }
    }
  }
}

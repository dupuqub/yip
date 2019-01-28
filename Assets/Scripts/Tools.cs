using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tools
{
  //....................................................................................................................
  public static bool FileExists(string address)
  {
    return File.Exists(Application.streamingAssetsPath + address);
  }

  //....................................................................................................................
  public static string GetFile(string address)
  {
    return File.ReadAllText(Application.streamingAssetsPath + address);
  }

  //....................................................................................................................
  public static void UISelect(string target)
  {
    EventSystem.current.SetSelectedGameObject(GameObject.Find(target));
  }

  //....................................................................................................................
  // Facilitates color building in Unity.
  // (if it receives input from 0 to 255, it will output from 0 to 1)

  public static float C(int source)
  {
    return (float)source / 255;
  }

  //....................................................................................................................
  // Removes hover effects on buttons when keyboard or gamepad are being used.
  // (I do not fully understand this method, check source for more information)
  // https://answers.unity.com/questions/1130654/clearing-mouse-hover-effect-when-cursor-is-hidden.html

  public static void ClearHover()
  {
    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> raycasts = new List<RaycastResult>();

    pointer.position = Input.mousePosition;
    EventSystem.current.RaycastAll(pointer, raycasts);

    if(raycasts.Count > 0)
    {
      foreach(RaycastResult raycast in raycasts)
      {
        Button hovered = raycast.gameObject.GetComponent<Button>();

        if(hovered) hovered.OnPointerExit(pointer);
      }
    }
  }
}

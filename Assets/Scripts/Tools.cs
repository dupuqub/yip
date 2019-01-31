using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tools
{
  //....................................................................................................................
  // Directory management.

  public static bool DirExists(string address)
  {return Directory.Exists($"{Application.streamingAssetsPath}/{address}");}
  
  public static void DirDelete(string address)
  {Directory.Delete($"{Application.streamingAssetsPath}/{address}", true);}
  
  public static void DirCreate(string address)
  {Directory.CreateDirectory($"{Application.streamingAssetsPath}/{address}");}

  //....................................................................................................................
  // File management.

  public static bool FileExists(string address)
  {return File.Exists($"{Application.streamingAssetsPath}/{address}");}

  public static void FileDelete(string address)
  {File.Delete($"{Application.streamingAssetsPath}/{address}");}

  public static void FileCreate(string address, string newSave)
  {File.WriteAllText($"{Application.streamingAssetsPath}/{address}", newSave);}

  public static string FileRead(string address)
  {return File.ReadAllText($"{Application.streamingAssetsPath}/{address}");}

  //....................................................................................................................
  // General.

  public static void UISelect(string target)
  {EventSystem.current.SetSelectedGameObject(target == "" ? null : GameObject.Find(target));}

  public static string Title(string word)
  {return $"{word.Substring(0, 1).ToUpper()}{word.Substring(1)}";}

  //....................................................................................................................
  // Facilitates color building in Unity.
  // (if it receives input from 0 to 255, it will output from 0 to 1)

  public static float C(int source)
  {return (float)source / 255;}

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

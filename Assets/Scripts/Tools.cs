using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tools
{
  public static bool FileExists(string address)
  {
    return File.Exists(Application.streamingAssetsPath + address);
  }

  public static string GetFile(string address)
  {
    return File.ReadAllText(Application.streamingAssetsPath + address);
  }

  public static void UISelect(string target)
  {
    EventSystem.current.SetSelectedGameObject(GameObject.Find(target));
  }
}

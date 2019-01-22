using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
}

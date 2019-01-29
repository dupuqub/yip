using UnityEngine;

public class LobbyMain : MonoBehaviour
{
  void Start()
  {
    LobbyTools.UpdateLanguage();
  }

  void Update()
  {
    LobbyTools.UpdateNavigation();
  }
}

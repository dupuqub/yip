using UnityEngine;

public class LobbyMain : MonoBehaviour
{
  void Start()
  {
    LobbyTools.UpdateLang();
  }

  void Update()
  {
    LobbyTools.UpdateUI();
  }
}

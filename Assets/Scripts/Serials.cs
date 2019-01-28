public class Serials
{
  [System.Serializable]
  public class _Common
  {
    public string lang;
    public int account;
  }

  [System.Serializable]
  public class LobbyLang
  {
    public string id;
    public string name;
    public string lastLogin;
    public string newPlayer;
    public string question;
    public string settings;
    public string exit;
  }

  [System.Serializable]
  public class SaveMain
  {
    public string name;
    public string lastLogin;
  }
}

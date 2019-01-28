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
    public string lastLogin;
    public string newPlayer;
    public string question;
    public string settings;
    public string credits;
    public string exit;
  }

  [System.Serializable]
  public class Account
  {
    public string name;
    public string lastLogin;
  }
}

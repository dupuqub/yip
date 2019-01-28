using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LobbyMain : MonoBehaviour
{
  //....................................................................................................................
  void Start()
  {
    string _CommonJson = Tools.GetFile("/Sources/_Common.json");
    Serials._Common _CommonSnap = JsonUtility.FromJson<Serials._Common>(_CommonJson);

    string LangJson = Tools.GetFile($"/Langs/{_CommonSnap.lang}/Lobby.json");
    Serials.LobbyLang LangSnap = JsonUtility.FromJson<Serials.LobbyLang>(LangJson);

    Text question = GameObject.Find("Question").transform.GetChild(0).GetComponent<Text>();
    Text settings = GameObject.Find("Settings").transform.GetChild(0).GetComponent<Text>();
    Text credits = GameObject.Find("Credits").transform.GetChild(0).GetComponent<Text>();
    Text exit = GameObject.Find("Exit").transform.GetChild(0).GetComponent<Text>();

    question.text = LangSnap.question;
    settings.text = LangSnap.settings;
    credits.text = LangSnap.credits;
    exit.text = LangSnap.exit;

    for(int index = 0; index < 4; index ++)
    {
      GameObject account = GameObject.Find($"Account{index}");
      GameObject input = account.transform.Find($"Input{index}").gameObject;
      InputField field = input.GetComponent<InputField>();
      Text placeholder = field.placeholder.GetComponent<Text>();
      Text last = account.transform.Find("Last").gameObject.GetComponent<Text>();

      string address = $"/Accounts/Account{index}.json";
      bool exists = Tools.FileExists(address);

      if(exists)
      {
        string AccountJson = Tools.GetFile(address);
        Serials.Account AccountSnap = JsonUtility.FromJson<Serials.Account>(AccountJson);

        placeholder.text = AccountSnap.name;
        placeholder.color = new Color(Tools.C(51), Tools.C(34), Tools.C(85));
        last.text = $"{LangSnap.lastLogin} - {AccountSnap.lastLogin}";
      }
      else
      {
        placeholder.text = LangSnap.newPlayer;
        last.text = "";
      }
    }
  }

  //....................................................................................................................
  int delay = 20;
  int currentDelay = 0;

  void Update()
  {
    // "Input Field" breaks Unity's UI navigation's flow.
    // Every scene with "Input Field" must therefore have custom UI navigation.

    float MOUSEX = Input.GetAxis("MOUSEX");
    float MOUSEY = Input.GetAxis("MOUSEY");
    float ARROWX = Input.GetAxis("ARROWX");
    float ARROWY = Input.GetAxis("ARROWY");

    bool LEFT = ARROWX < 0;
    bool RIGHT = ARROWX > 0;
    bool DOWN = ARROWY < 0;
    bool UP = ARROWY > 0;

    GameObject current = EventSystem.current.currentSelectedGameObject;
    bool inputing = current && current.name.Length > 5 && current.name.Substring(0, 5) == "Input";
    bool navigating = ARROWX != 0 || ARROWY != 0;
    bool mousing = MOUSEX != 0 || MOUSEY != 0;

         if(mousing && !inputing) EventSystem.current.SetSelectedGameObject(null);
    else if(navigating) Tools.ClearHover();

         if(!navigating) currentDelay = 0;
    else if(currentDelay > 0) currentDelay --;
    else
    {
      currentDelay = delay;

      if(current == null) Tools.UISelect("Settings");

      else if(current.name == "Settings")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Credits");
        else if(UP) Tools.UISelect("Exit");
      }

      else if(current.name == "Credits")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Exit");
        else if(UP) Tools.UISelect("Settings");
      }

      else if(current.name == "Exit")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Settings");
        else if(UP) Tools.UISelect("Credits");
      }

      else if(current.name.Substring(0, 5) == "Input")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Settings");
        else if(RIGHT) Tools.UISelect("Play" + index);
        else if(DOWN) Tools.UISelect("Input" + indexDown.ToString());
        else if(UP) Tools.UISelect("Input" + indexUp.ToString());
      }

      else if(current.name.Substring(0, 4) == "Play")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Input" + index);
        else if(RIGHT) Tools.UISelect("Erase" + index);
        else if(DOWN) Tools.UISelect("Play" + indexDown.ToString());
        else if(UP) Tools.UISelect("Play" + indexUp.ToString());
      }

      else if(current.name.Substring(0, 5) == "Erase")
      {
        string index = current.name.Substring(current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Play" + index);
        else if(DOWN) Tools.UISelect("Erase" + indexDown.ToString());
        else if(UP) Tools.UISelect("Erase" + indexUp.ToString());
      }
    }
  }
}

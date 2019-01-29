using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LobbyTools : MonoBehaviour
{
  //....................................................................................................................
  // "Input Field" breaks Unity's UI navigation's flow.
  // Every scene with "Input Field" must therefore have custom UI navigation.

  public static int delay = 20;
  public static int currentDelay = 0;

  public static void UpdateNavigation()
  {
    float MOUSEX = Input.GetAxis("MOUSEX");
    float MOUSEY = Input.GetAxis("MOUSEY");
    float ARROWX = Input.GetAxis("ARROWX");
    float ARROWY = Input.GetAxis("ARROWY");

    bool LEFT = ARROWX < 0;
    bool RIGHT = ARROWX > 0;
    bool DOWN = ARROWY < 0;
    bool UP = ARROWY > 0;

    GameObject Current = EventSystem.current.currentSelectedGameObject;

    bool inputing = Current && Current.name.Length > 5 && Current.name.Substring(0, 5) == "Input";
    bool navigating = ARROWX != 0 || ARROWY != 0;
    bool mousing = MOUSEX != 0 || MOUSEY != 0;

         if(mousing && !inputing) Tools.UISelect(null);
    else if(navigating) Tools.ClearHover();

         if(!navigating) currentDelay = 0;
    else if(currentDelay > 0) currentDelay --;
    else
    {
      currentDelay = delay;

      if(Current == null) Tools.UISelect("Language");

      else if(Current.name == "Language")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Settings");
        else if(UP) Tools.UISelect("Exit");
      }

      else if(Current.name == "Settings")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Exit");
        else if(UP) Tools.UISelect("Language");
      }

      else if(Current.name == "Exit")
      {
             if(RIGHT) Tools.UISelect("Input0");
        else if(DOWN) Tools.UISelect("Language");
        else if(UP) Tools.UISelect("Settings");
      }

      else if(Current.name.Substring(0, 5) == "Input")
      {
        string index = Current.name.Substring(Current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Language");
        else if(RIGHT) Tools.UISelect("Play" + index);
        else if(DOWN) Tools.UISelect("Input" + indexDown.ToString());
        else if(UP) Tools.UISelect("Input" + indexUp.ToString());
      }

      else if(Current.name.Substring(0, 4) == "Play")
      {
        string index = Current.name.Substring(Current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Input" + index);
        else if(RIGHT) Tools.UISelect("Erase" + index);
        else if(DOWN) Tools.UISelect("Play" + indexDown.ToString());
        else if(UP) Tools.UISelect("Play" + indexUp.ToString());
      }

      else if(Current.name.Substring(0, 5) == "Erase")
      {
        string index = Current.name.Substring(Current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Play" + index);
        else if(DOWN) Tools.UISelect("Erase" + indexDown.ToString());
        else if(UP) Tools.UISelect("Erase" + indexUp.ToString());
      }
    }
  }

  //....................................................................................................................
  public static void UpdateLanguage()
  {
    string _CommonJson = Tools.GetFile("/Sources/_Common.json");
    Serials._Common _Common = JsonUtility.FromJson<Serials._Common>(_CommonJson);

    string LangJson = Tools.GetFile($"/Langs/{_Common.lang}/Lobby.json");
    Serials.LobbyLang Lang = JsonUtility.FromJson<Serials.LobbyLang>(LangJson);

    Text Question = GameObject.Find("Question").transform.GetChild(0).GetComponent<Text>();
    Text Language = GameObject.Find("Language").transform.GetChild(0).GetComponent<Text>();
    Text Settings = GameObject.Find("Settings").transform.GetChild(0).GetComponent<Text>();
    Text Exit = GameObject.Find("Exit").transform.GetChild(0).GetComponent<Text>();
    Text Title = GameObject.Find("Appendix").transform.GetChild(0).GetChild(0).GetComponent<Text>();

    Question.text = Lang.question;
    Language.text = $"{Lang.name}    <size=30px><color=#DDCCFF88><i>{Lang.id}</i></color></size>";
    Settings.text = Lang.settings;
    Exit.text = Lang.exit;
    Title.text = Lang.settings;

    for(int index = 0; index < 4; index ++)
    {
      GameObject Save = GameObject.Find($"Save{index}");
      GameObject Input = Save.transform.Find($"Input{index}").gameObject;
      InputField Field = Input.GetComponent<InputField>();
      Text Placeholder = Field.placeholder.GetComponent<Text>();
      Text Last = Save.transform.Find("Last").gameObject.GetComponent<Text>();

      bool directoryExists = Tools.DirExists($"/Saves/Save{index}");

      if(directoryExists)
      {
        string SaveMainJson = Tools.GetFile($"/Saves/Save{index}/Main.json");
        Serials.SaveMain SaveMain = JsonUtility.FromJson<Serials.SaveMain>(SaveMainJson);

        Placeholder.text = SaveMain.name;
        Placeholder.color = new Color(Tools.C(51), Tools.C(34), Tools.C(85));
        Last.text = $"{Lang.lastSave} - {SaveMain.lastSave}";
      }
      else
      {
        Placeholder.text = Lang.newPlayer;
        Last.text = "";
      }
    }
  }

  //....................................................................................................................
  public void PressLanguage()
  {
    string _CommonJson = Tools.GetFile("/Sources/_Common.json");
    Serials._Common _Common = JsonUtility.FromJson<Serials._Common>(_CommonJson);

    // Only list containing every language.
    string[] languages = {"English", "Portuguese"};
    int maxIndex = languages.Length - 1;
    int oldIndex = Array.IndexOf(languages, _Common.lang);
    int newIndex = (oldIndex + 1) > maxIndex ? 0 : oldIndex + 1;
    string newLang = languages[newIndex];

    _Common.lang = newLang;

    string address = "/Sources/_Common.json";
    string newSave = $"{JsonUtility.ToJson(_Common, true)}\n";
    Tools.SetFile(address, newSave);

    UpdateLanguage();
  }

  //....................................................................................................................
  public void PressSettings()
  {
    string _CommonJson = Tools.GetFile("/Sources/_Common.json");
    Serials._Common _Common = JsonUtility.FromJson<Serials._Common>(_CommonJson);

    string LangJson = Tools.GetFile($"/Langs/{_Common.lang}/Lobby.json");
    Serials.LobbyLang Lang = JsonUtility.FromJson<Serials.LobbyLang>(LangJson);

    Slide Appendix = GameObject.Find("Appendix").GetComponent<Slide>();

    if(!Appendix.move) Appendix.move = true;
    else Appendix.hiding = !Appendix.hiding;
  }

  //....................................................................................................................
  public void PressExit()
  {
    Application.Quit();
    Debug.Log("Exit");
  }

  //....................................................................................................................
  public void PressPlay(int index)
  {
    Debug.Log($"Play {index}");
  }

  //....................................................................................................................
  public void PressErase(int index)
  {
    Debug.Log($"Erase {index}");
  }
}

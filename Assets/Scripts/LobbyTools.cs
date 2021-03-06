﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LobbyTools : MonoBehaviour
{
  //....................................................................................................................
  // "Input Field" breaks Unity's UI navigation's flow.
  // Every scene with "Input Field" must therefore have custom UI navigation.

  public static int delay = 30;
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

      else if(Current.name.Length > 4
      && Current.name.Substring(0, 5) == "Input")
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

      else if(Current.name.Length > 3
      && Current.name.Substring(0, 4) == "Play")
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

      else if(Current.name.Length > 4
      && Current.name.Substring(0, 5) == "Erase")
      {
        string index = Current.name.Substring(Current.name.Length - 1);
        int indexInt = Int32.Parse(index);
        int indexDown = indexInt + 1 > 3 ? 0 : indexInt + 1;
        int indexUp = indexInt - 1 < 0 ? 3 : indexInt - 1;

             if(LEFT) Tools.UISelect("Play" + index);
        else if(DOWN) Tools.UISelect("Erase" + indexDown.ToString());
        else if(UP) Tools.UISelect("Erase" + indexUp.ToString());
      }

      else if(Current.name.Length > 2
      && Current.name.Substring(0, 3) == "Yes")
      {
        string index = Current.name.Substring(Current.name.Length - 1);
        if(LEFT || RIGHT) Tools.UISelect("No" + index);
      }

      else if(Current.name.Length > 1
      && Current.name.Substring(0, 2) == "No")
      {
        string index = Current.name.Substring(Current.name.Length - 1);
        if(LEFT || RIGHT) Tools.UISelect("Yes" + index);
      }
    }
  }

  //....................................................................................................................
  public static void UpdateLanguage()
  {
    string json_Common = Tools.FileRead("Sources/_Common.json");
    var _Common = JsonUtility.FromJson<Serials._Common>(json_Common);

    string jsonLang = Tools.FileRead($"Langs/{_Common.lang}/Lobby.json");
    var Lang = JsonUtility.FromJson<Serials.LobbyLang>(jsonLang);

    var Question = GameObject.Find("Question").transform.GetChild(0).GetComponent<Text>();
    var Language = GameObject.Find("Language").transform.GetChild(0).GetComponent<Text>();
    var Settings = GameObject.Find("Settings").transform.GetChild(0).GetComponent<Text>();
    var Exit = GameObject.Find("Exit").transform.GetChild(0).GetComponent<Text>();
    var Title = GameObject.Find("Appendix").transform.GetChild(0).GetChild(0).GetComponent<Text>();

    Question.text = Lang.question;
    Language.text = $"{Lang.name}    <size=30px><color=#DDCCFF88><i>{Lang.id}</i></color></size>";
    Settings.text = Lang.settings;
    Exit.text = Lang.exit;
    Title.text = Lang.settings;

    for(int index = 0; index < 4; index ++)
    {
      // Confirms.
      var LastSave = GameObject.Find($"Confirm{index}").transform.GetChild(2).GetComponent<Text>();
      LastSave.text = Lang.erase;

      // Saves.
      GameObject Save = GameObject.Find($"Save{index}");
      GameObject Input = GameObject.Find($"Input{index}");
      var Field = Input.GetComponent<InputField>();
      var Placeholder = Field.placeholder.GetComponent<Text>();
      var Last = Save.transform.Find("Last").gameObject.GetComponent<Text>();

      bool directoryExists = Tools.DirExists($"Saves/Save{index}");

      if(directoryExists)
      {
        string jsonSaveMain = Tools.FileRead($"Saves/Save{index}/Main.json");
        var SaveMain = JsonUtility.FromJson<Serials.SaveMain>(jsonSaveMain);

        Placeholder.text = SaveMain.name;
        Placeholder.color = new Color(Tools.C(51), Tools.C(34), Tools.C(85));
        Last.text = $"{Lang.lastSave} - {SaveMain.lastSave}";
      }
      else
      {
        Placeholder.text = Lang.newPlayer;
        Placeholder.color = new Color(Tools.C(170), Tools.C(153), Tools.C(204), Tools.C(128));
        Last.text = "";
      }
    }
  }

  //....................................................................................................................
  public static void DisableAll()
  {
    GameObject.Find("Language").GetComponent<Button>().interactable = false;
    GameObject.Find("Settings").GetComponent<Button>().interactable = false;
    GameObject.Find("Exit").GetComponent<Button>().interactable = false;

    for(int index = 0; index < 4; index ++)
    {
      GameObject.Find($"Input{index}").GetComponent<InputField>().interactable = false;
      GameObject.Find($"Play{index}").GetComponent<Button>().interactable = false;
      GameObject.Find($"Erase{index}").GetComponent<Button>().interactable = false;
      GameObject.Find($"Yes{index}").GetComponent<Button>().interactable = false;
      GameObject.Find($"No{index}").GetComponent<Button>().interactable = false;
    }
  }

  //....................................................................................................................
  // No "coroutine-methods" can be static.

  public void PlaySlide()
  {
    IEnumerator Routine()
    {
      GameObject.Find("Title").GetComponent<Slide>().move = true;
      yield return new WaitForSeconds(0.2f);
      GameObject.Find("Language").GetComponent<Slide>().move = true;
      yield return new WaitForSeconds(0.1f);
      GameObject.Find("Settings").GetComponent<Slide>().move = true;
      yield return new WaitForSeconds(0.1f);
      GameObject.Find("Exit").GetComponent<Slide>().move = true;
      yield return new WaitForSeconds(0.3f);
      GameObject.Find("PinkBox").GetComponent<Slide>().move = true;
      yield return new WaitForSeconds(0.1f);
      GameObject.Find("Question").GetComponent<Slide>().move = true;

      for(int index = 0; index < 4; index ++)
      {
        // Saves
        yield return new WaitForSeconds(0.1f);
        GameObject.Find($"Save{index}").GetComponent<Slide>().move = true;

        // Confirms.
        var Confirm = GameObject.Find($"Confirm{index}").GetComponent<Slide>();

        if(!Confirm.move)
        {
          if(!Confirm.hiding) Confirm.move = true;
        }
        else if(Confirm.hiding) Confirm.hiding = false;
      }

      yield return new WaitForSeconds(1f);
      SceneManager.LoadScene("Sketch");
    }

    DisableAll();
    StartCoroutine(Routine());
  }

  //....................................................................................................................
  // No "press-methods" can be static.

  public void PressLanguage()
  {
    string json_Common = Tools.FileRead("Sources/_Common.json");
    var _Common = JsonUtility.FromJson<Serials._Common>(json_Common);

    string[] languages = _Common.langs;
    int maxIndex = languages.Length - 1;
    int oldIndex = Array.IndexOf(languages, _Common.lang);
    int newIndex = (oldIndex + 1) > maxIndex ? 0 : oldIndex + 1;
    string newLang = languages[newIndex];

    _Common.lang = newLang;

    string address = "Sources/_Common.json";
    string newSave = JsonUtility.ToJson(_Common, true);
    Tools.FileCreate(address, newSave);

    UpdateLanguage();
  }

  //....................................................................................................................
  public void PressSettings()
  {
    // Gather sources.
    string json_Common = Tools.FileRead("Sources/_Common.json");
    var _Common = JsonUtility.FromJson<Serials._Common>(json_Common);

    string jsonLang = Tools.FileRead($"Langs/{_Common.lang}/Lobby.json");
    var Lang = JsonUtility.FromJson<Serials.LobbyLang>(jsonLang);

    // Show appendix.
    var Appendix = GameObject.Find("Appendix").GetComponent<Slide>();

    if(!Appendix.move) Appendix.move = true;
    else Appendix.hiding = !Appendix.hiding;

    // Hide confirmations.
    for(int index = 0; index < 4; index ++)
    {
      var Confirm = GameObject.Find($"Confirm{index}").GetComponent<Slide>();
      
      if(!Confirm.move)
      {
        if(!Confirm.hiding) Confirm.move = true;
      }
      else if(Confirm.hiding) Confirm.hiding = false;
    }
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
    GameObject Input = GameObject.Find($"Input{index}");
    var Field = Input.GetComponent<InputField>();

    // Continue game.
    if(Tools.DirExists($"Saves/Save{index}"))
    {
      PlaySlide();
    }

    // New game.
    else if(Field.text != "")
    {
      string dirPath = $"Saves/Save{index}";
      string modelPath = $"Saves/Model";
      DateTime date = DateTime.Now;

      string jsonSaveMain = Tools.FileRead($"{modelPath}/Main.json");
      var SaveMain = JsonUtility.FromJson<Serials.SaveMain>(jsonSaveMain);

      SaveMain.name = Field.text;
      SaveMain.lastSave = $"{date.ToString("HH:mm")} - {date.ToString("dd/MM/yyyy")}";

      string newSaveMain = JsonUtility.ToJson(SaveMain, true);

      Tools.DirCreate(dirPath);
      Tools.FileCreate($"{dirPath}/Main.json", newSaveMain);

      PlaySlide();
    }

    // New game (empty name).
    else
    {
      var Warning = GameObject.Find("NameWarning").GetComponent<NameWarningGlow>();

      Warning.index = index;
      Warning.alpha = 1; 
    }
  }

  //....................................................................................................................
  public void PressErase(string action)
  {
    // "action" is a combination of 1 letter and 1 number.
    // The letter might be "C" for "cancel", "S" for "start" or "F" for "finish".
    // The number is from 0 to 3 (which button was pressed).

    int number = Int32.Parse(action.Substring(1));
    string letter = action.Substring(0, 1);
    string address = $"Saves/Save{number}";

    // Delete folder and its meta.
    if(letter == "F" && Tools.DirExists(address))
    {
      Tools.DirDelete(address);
      Tools.FileDelete($"{address}.meta");
      UpdateLanguage();
    }

    // Navigate.
    if(letter == "S") Tools.UISelect($"No{number}");
    else Tools.UISelect($"Play{number}");

    // Move confirmations.
    for(int index = 0; index < 4; index ++)
    {
      var Confirm = GameObject.Find($"Confirm{index}").GetComponent<Slide>();

      // Pressed confirm.
      if(number == index)
      {
        if(!Confirm.move) Confirm.move = true;
        else Confirm.hiding = letter == "S";
      }

      // Other confirms.
      else if(!Confirm.move)
      {
        if(!Confirm.hiding) Confirm.move = true;
      }
      else if(Confirm.hiding) Confirm.hiding = false;
    }
  }
}

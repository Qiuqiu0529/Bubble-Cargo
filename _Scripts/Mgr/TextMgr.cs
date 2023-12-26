using UnityEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System;

public class TextMgr : Singleton<TextMgr> //change language, not use now 
{
    public TextTable textTable;
    public string language;
    public event Action changeText;


    private void Start()
    {
        language=PlayerPrefs.GetString(Global.languageSet,Global.english);
        DialogueManager.SetLanguage( language);
        changeText?.Invoke();
    }

    public void SetChinese()
    {
        
        language=Global.chinese;
        PlayerPrefs.SetString(Global.languageSet,language);
        DialogueManager.SetLanguage( language);
        changeText?.Invoke();
        
    }
    public void SetEnglish()
    {
        language=Global.english;
        PlayerPrefs.SetString(Global.languageSet,language);
        DialogueManager.SetLanguage( language);
        changeText?.Invoke();
    }

    public string GetText(string textID)
    {
        if (textTable.GetFieldTextForLanguage(textID, language) != null)
        {
            return textTable.GetFieldTextForLanguage(textID, language);
        }
        return "";
    }
}

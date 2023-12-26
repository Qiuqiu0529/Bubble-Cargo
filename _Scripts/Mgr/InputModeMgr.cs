using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModeMgr : Singleton<InputModeMgr>
{
    public GameObject arduinoInput, keyboardInput;

    public BubbleController bubbleController;

    void Start()
    {
        string inputmode = PlayerPrefs.GetString(Global.inputMode, Global.keyboardInput);
        if (inputmode == Global.keyboardInput)
        {
            keyboardInput.SetActive(true);
            arduinoInput.SetActive(false);
        }
        else
        {
            keyboardInput.SetActive(false);
            arduinoInput.SetActive(true);
        }
    }
}

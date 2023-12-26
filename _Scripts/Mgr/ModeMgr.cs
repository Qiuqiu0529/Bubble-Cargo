using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeMgr : Singleton<ModeMgr>
{
    public GameMode gameMode;
    public float targetZenTime;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetZenTarget(int minute)
    {
        targetZenTime=minute*60;
    }
    public void SetNormal()
    {
        gameMode=GameMode.normal;
    }
    public void ToggleMode()
    {
        if(gameMode==GameMode.normal)
        {
            gameMode=GameMode.zen;
        }
        else
        {
            gameMode=GameMode.normal;
        }
    }
}

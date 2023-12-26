using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataMgr : Singleton<DataMgr> // handle save and load, not use right now
{
    public int allToyCount=8;
    private string savePath;
    GameData loadedData;
    public Toy[] toys;

    private void Awake()
    {
        base.Awake();
        savePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        loadedData=LoadGameData();
    }

    void Start()
    {
        if(ModeMgr.Instance.gameMode==GameMode.normal)
        {
            if(loadedData.totalCompletedToys>0)
            {

            }
        }
    }

    public void Save()
    {
        SaveGameData(loadedData);
    }

    public void SaveGameData(GameData gameData)
    {
        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(savePath, jsonData);
    }

    public GameData LoadGameData()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            return new GameData( 0, new List<ToyData>());
        }
    }

}

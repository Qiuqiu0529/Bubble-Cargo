using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCompletedToys; 
    public List<ToyData> toyData; 
    public GameData(int completedToys, List<ToyData> toys)
    {
        totalCompletedToys = completedToys;
        toyData = toys;
    }
}
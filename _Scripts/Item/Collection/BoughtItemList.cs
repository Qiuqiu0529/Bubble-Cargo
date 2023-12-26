using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "BoughtItemList", menuName = "Inventory/BoughtItemList")]
public class BoughtItemList : SerializedScriptableObject
{
    public Dictionary<ItemName,int> boughtList=new Dictionary<ItemName, int>();
    public void AddItem(ItemName itemName)
    {
        if(boughtList.ContainsKey(itemName))
        {
            boughtList[itemName]++;
        }
        else
        {
            boughtList.Add(itemName,1);
        }
    }

    public int Contain(ItemName itemName)
    {
        if(boughtList.ContainsKey(itemName))
        {
            return  boughtList[itemName];
        }
        return 0;
    }
}

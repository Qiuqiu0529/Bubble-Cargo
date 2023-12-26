using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ItemName itemName;
    public int coin;

    public void Buy()
    {
        ShopMgr.Instance.CanBuy(coin,itemName);
    }
}

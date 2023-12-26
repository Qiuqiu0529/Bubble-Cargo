using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;
using NovaSamples.UIControls;
using System.Net.Http.Headers;

public class ItemUIList : MonoBehaviour
{
    public ListView listView;
    public ItemDataList_SO itemDataList_SO;
   // int selectIndex = 0;
    public void BindItemUI(Nova.Data.OnBind<ItemDetails> evt, ItemUIVisual visual, int index)
    {
        ItemDetails itemDetails=evt.UserData;
        visual.shopItem.itemName=itemDetails.itemName;
        visual.shopItem.coin=itemDetails.needCoin;
        visual.itemNameBlock.Text=itemDetails.nameBlockText;
        visual.itemPic.SetImage(itemDetails.itemSprite);
        visual.coinBlock.Text=itemDetails.needCoin.ToString();
    }
    private void Awake()
    {
        listView.AddDataBinder<ItemDetails, ItemUIVisual>(BindItemUI);
        listView.SetDataSource(itemDataList_SO.itemDetailsList);
    }

    private void OnDestroy()
    {
        listView.RemoveDataBinder<ItemDetails, ItemUIVisual>(BindItemUI);
    }
}

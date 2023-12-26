using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;

public class ItemCollectionUIList : MonoBehaviour
{
    public ListView listView;
    public ItemDataList_SO itemDataList_SO;
    public BoughtItemList boughtItemList;
    public List<ItemCollectionInfo> itemCollectionInfos = new List<ItemCollectionInfo>();
    public void BindItemUI(Nova.Data.OnBind<ItemCollectionInfo> evt, ItemCollectionVisual visual, int index)
    {
        ItemCollectionInfo itemDetails = evt.UserData;
        visual.itemNameBlock.Text = itemDetails.nameBlockText;
        visual.itemPic.SetImage(itemDetails.itemSprite);
        visual.numBlock.Text = itemDetails.holdCount.ToString();
    }

    private void Awake()
    {
        listView.AddDataBinder<ItemCollectionInfo, ItemCollectionVisual>(BindItemUI);
        
    }

    private void OnDestroy()
    {
        listView.RemoveDataBinder<ItemCollectionInfo, ItemCollectionVisual>(BindItemUI);
    }
    void OnEnable()
    {
        SetBoughtItemList();
    }

    public void SetBoughtItemList()
    {
        itemCollectionInfos.Clear();
        foreach(var item in itemDataList_SO.itemDetailsList)
        {
            if(boughtItemList.Contain(item.itemName)>0)
            {
                ItemCollectionInfo itemDetails=new ItemCollectionInfo();
                itemDetails.nameBlockText=item.nameBlockText;
                itemDetails.itemSprite=item.itemSprite;
                itemDetails.holdCount=boughtItemList.Contain(item.itemName);
                itemCollectionInfos.Add(itemDetails);
            }
        }
        listView.SetDataSource(itemCollectionInfos);
    }
}

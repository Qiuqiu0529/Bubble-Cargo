using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using Nova;

public class ShopMgr : Singleton<ShopMgr>
{
    public int coinUI;
    public int coinCount;
    public TextBlock coinBlock;
    public BoughtItemList boughtItemList;
    // Start is called before the first frame update
    void OnEnable()
    {
        coinUI=0;
        //PlayerPrefs.SetInt(Global.coin,30);///等待注释
        coinCount=PlayerPrefs.GetInt(Global.coin,0);
    }

    public void CountCoin()
    {
        StartCoroutine(CountCoinIenum());
    }
    public bool CanBuy(int need,ItemName itemName)
    {
        if(coinCount>=need)
        {
            coinCount-=need;
            PlayerPrefs.SetInt(Global.coin,coinCount);
            StartCoroutine(CountCoinIenum());
            boughtItemList.AddItem(itemName);
            return true;
        }
        return false;
    }

    public IEnumerator CountCoinIenum()
    {
        float tempStartTime = Time.time;
        float elapsedTime = 0;
        float duration = 1;
        int tempStartValue=coinUI;
        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - tempStartTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float progress = MMTweenDefinitions.EaseOut_Quadratic(t);

            int newCoinValue = (int)Mathf.Lerp(tempStartValue, coinCount, progress);
            coinUI=newCoinValue;
            coinBlock.Text = newCoinValue.ToString();
            yield return new WaitForFixedUpdate();

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using Nova;
public class GameMgr : Singleton<GameMgr> // handle game flow
{
    bool end;
    public float startTime;
    public float gameDuration;
    public float targetZenTime;
    public int coinCount;
    public int totalToyAmount;
    public int completeToyAmount;
    public TextBlock coinBlock, toyBlock;
    public GameMode gameMode;

    public MMF_Player finalEndFB, addCompleteToyFB, addCoinFB,zenEndFB,pauseEndFB,returnFB;
    public BubbleController bubbleController;
    [SerializeField] TextBlock endTimeBlock, endCoinBlock, endToyBlock;
    public GameObject[] stars;
    

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        coinCount = 0;
        gameMode=ModeMgr.Instance.gameMode;
        targetZenTime=ModeMgr.Instance.targetZenTime;
    }
    void Update()
    {
        if (end)
        {
            return;
        }
        // check Zen mode end condition
        if (gameMode == GameMode.zen)
        {
            if (Time.time - startTime >= targetZenTime)
            {
                End();
                zenEndFB.PlayFeedbacks();
            }
        }
    }

    public void End()
    {
        end = true;
        gameDuration = Time.time - startTime;
        bubbleController.Pause();
        endCoinBlock.Text = "";
        endTimeBlock.Text = "";
        endToyBlock.Text = "";
    }

     // counts and displays stars based on completed toy amount
    public void CountStar()
    {
        if(completeToyAmount>=6)
        {
            foreach(var star in stars)
            {
                star.SetActive(true);
            }
        }
        else if(completeToyAmount>=3)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(false);
        }
        else if(completeToyAmount>=1)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }
    }


    public void EndCountToy()
    {
        StartCoroutine(EndCountToyIenum());
    }
    public void EndCountCoin()
    {
        int temp=PlayerPrefs.GetInt(Global.coin,0);
        temp+=coinCount;
        PlayerPrefs.SetInt(Global.coin,temp);
        StartCoroutine(EndCountCoinIenum());
    }
    public void EndCountTime()
    {
        
        StartCoroutine(EndCountTimeIenum());
    }

    public IEnumerator EndCountToyIenum() // count smoothly
    {
        float tempStartTime = Time.time;
        float elapsedTime = 0;
        float duration = 1;
        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - tempStartTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float progress = MMTweenDefinitions.EaseOut_Quadratic(t);

            int newToyValue = (int)Mathf.Lerp(0, completeToyAmount, progress);
            endToyBlock.Text = newToyValue.ToString();
            yield return new WaitForFixedUpdate();

        }
    }
    public IEnumerator EndCountTimeIenum()
    {
        Debug.Log(gameDuration);
        float tempStartTime = Time.time;
        float elapsedTime = 0;
        float duration = 1;
        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - tempStartTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float progress = MMTweenDefinitions.EaseOut_Quadratic(t);

            int newTimeCount = (int)Mathf.Lerp(0, gameDuration, progress);
            int second = Mathf.FloorToInt(newTimeCount % 60);
            int minute = Mathf.FloorToInt(newTimeCount / 60);
            endTimeBlock.Text = minute.ToString() + " : " + second.ToString();

            yield return new WaitForFixedUpdate();

        }
    }

    public IEnumerator EndCountCoinIenum()
    {
        float tempStartTime = Time.time;
        float elapsedTime = 0;
        float duration = 1;
        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - tempStartTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float progress = MMTweenDefinitions.EaseOut_Quadratic(t);

            int newCoinValue = (int)Mathf.Lerp(0, coinCount, progress);
            endCoinBlock.Text = newCoinValue.ToString();
            yield return new WaitForFixedUpdate();

        }
    }
    
    public void PauseEnd()
    {
        if(end)
        {
            return;
        }
        End();
        if (gameMode == GameMode.zen)
        {
            returnFB.PlayFeedbacks();
        }
        else
        {
            //存位置
            //DataMgr.Instance.Save();
            pauseEndFB.PlayFeedbacks();
        }
    }

    public void AddCompleteToy()
    {
        ++completeToyAmount;
        addCompleteToyFB.PlayFeedbacks();
        bubbleController.ReSetPos();
        if (completeToyAmount >= totalToyAmount && gameMode == GameMode.normal)
        {
            End();
            DataMgr.Instance.Save();
            finalEndFB.PlayFeedbacks();
        }
    }
    public void SetToyBlock()
    {
        toyBlock.Text = completeToyAmount.ToString();
    }
    public void AddCoin()
    {
        ++coinCount;
        addCoinFB.PlayFeedbacks();
    }

    public void SetCoinBlock()
    {
        coinBlock.Text = coinCount.ToString();
    }

}

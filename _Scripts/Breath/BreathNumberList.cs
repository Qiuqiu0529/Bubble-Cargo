using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;
using NovaSamples.UIControls;
using MoreMountains.Feedbacks;

public class BreathNumberList : MonoBehaviour
{
    public ListView listView;
    public NumberVisuals select;
    public List<NumberVisuals> breathNumberVisuals = new List<NumberVisuals>();
    public List<BreathNumber> breathNumbers = new List<BreathNumber>();
    int selectIndex = 0;
    public void BindChapter(Nova.Data.OnBind<BreathNumber> evt, NumberVisuals numberVisual, int index)
    {
        BreathNumber breathNumber = evt.UserData;
        if (breathNumber.breathTime > 0)
        {
            numberVisual.number.Text = breathNumber.breathTime.ToString();
        }
        else
        {
            numberVisual.number.Text = " ";
        }
        numberVisual.UpdateVisualState(VisualState.Default);

        breathNumberVisuals.Add(numberVisual);

    }
    private void Awake()
    {

        listView.AddDataBinder<BreathNumber, NumberVisuals>(BindChapter);
        listView.AddGestureHandler<Gesture.OnClick, NumberVisuals>(HandleContactClicked);
        listView.AddGestureHandler<Gesture.OnScroll, NumberVisuals>(HandleDrag);
        selectIndex = -1;
     

        for (int i = 0; i < 30; ++i)
        {
            BreathNumber breathNumber = new BreathNumber();
            breathNumber.breathTime = i + 1;
            breathNumbers.Add(breathNumber);

        }


        listView.SetDataSource(breathNumbers);

    }

    private void OnDestroy()
    {
        listView.RemoveDataBinder<BreathNumber, NumberVisuals>(BindChapter);
        listView.RemoveGestureHandler<Gesture.OnClick, NumberVisuals>(HandleContactClicked);
        listView.RemoveGestureHandler<Gesture.OnScroll, NumberVisuals>(HandleDrag);
    }

   
    private void HandleDrag(Gesture.OnScroll evt, NumberVisuals target, int index)
    {
        Debug.Log("OnScroll");
    }


    private void HandleContactClicked(Gesture.OnClick evt, NumberVisuals target, int index)
    {
       
       if(index+1>0)
       {
         ModeMgr.Instance.SetZenTarget(index+1);
       }
       
        listView.JumpToIndex(index);
        selectIndex = index;
        if (select != null)
        {
            select.UpdateVisualState(VisualState.Default);
        }
        select = target;
        target.UpdateVisualState(VisualState.Pressed);
    }


}

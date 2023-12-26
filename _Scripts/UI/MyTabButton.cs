using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Nova;
using NovaSamples.UIControls;

public class MyTabButton : UIControl<ButtonVisuals>
{
    public bool selected;
    public SelectOneButton selectOneButton;
    public AudioClip uisound;

    [Tooltip("Event fired when the button is clicked.")]
    public UnityEvent OnClicked = null;

    protected void OnEnable()
    {
        if (View.TryGetVisuals(out ButtonVisuals visuals))
        {
            if(selected)
            {
                visuals.UpdateVisualState(VisualState.Pressed);
            }
            else
            {
                visuals.UpdateVisualState(VisualState.Default);
            }
        }
        View.UIBlock.AddGestureHandler<Gesture.OnClick, ButtonVisuals>(HandleClicked);
        View.UIBlock.AddGestureHandler<Gesture.OnHover, ButtonVisuals>(ButtonVisuals.HandleHovered);
       View.UIBlock.AddGestureHandler<Gesture.OnUnhover, ButtonVisuals>(Unhovered);
       
    }
    public void Select()
    {

        if (View.TryGetVisuals(out ButtonVisuals visuals))
        {
            visuals.UpdateVisualState(VisualState.Hovered);
        }

    }

    public void Unhovered(Gesture.OnUnhover evt,  ButtonVisuals visuals)
    {
        if(!selected)
        {
            visuals.UpdateVisualState(VisualState.Default);
        }
    }

    public void Default()
    {
        selected=false;
        if (View.TryGetVisuals(out ButtonVisuals visuals))
        {
            visuals.UpdateVisualState(VisualState.Default);
        }
    }

    protected void OnDisable()
    {
       View.UIBlock.RemoveGestureHandler<Gesture.OnClick, ButtonVisuals>(HandleClicked);
       View.UIBlock.RemoveGestureHandler<Gesture.OnHover, ButtonVisuals>(ButtonVisuals.HandleHovered);
       View.UIBlock.RemoveGestureHandler<Gesture.OnUnhover, ButtonVisuals>(Unhovered);
    }

    public virtual void RemoveClicked()
    {
        View.UIBlock.AddGestureHandler<Gesture.OnClick, ButtonVisuals>(HandleClicked);
    }

    protected void HandleClicked(Gesture.OnClick evt, ButtonVisuals visuals)
    {
        Debug.Log("HandleClicked");
        selected=true;
        selectOneButton.SelectOne(this);
        OnClicked?.Invoke();
        UISound.Instance.PlayUISound(uisound);
    }
}

using System.Collections;
using System.Collections.Generic;
using Nova;
using UnityEngine;

public class SelectOneButton : MonoBehaviour
{
    public List<MyTabButton> myButtons;
    public void SelectOne(MyTabButton select)
    {
        select.Select();
        foreach(var button in myButtons)
        {
            if(button!=select)
            {
                button.Default();
            }
        }
    }

}

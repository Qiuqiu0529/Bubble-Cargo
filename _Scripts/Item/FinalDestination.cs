using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDestination : MonoBehaviour
{
   
    public void WriteActiveData(bool activeState)
    {
        gameObject.SetActive(activeState);
    }
}

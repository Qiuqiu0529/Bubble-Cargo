using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private string numericPropertyName = "_HsvShift";
    [SerializeField] private float scrollSpeed;

    [Header("There are 3 modifiers, just pick 1")]
    [Space, SerializeField] private bool backAndForth;
    [SerializeField] private float maxValue = 1f;
    private float iniValue;
    private bool goingUp;


    public Material[] mats;
    List<int> propertyShaderIDs=new List<int>();

    private int propertyShaderID;
    private float currValue;

    private void Start()
    { 

        foreach(var mat in mats)
        {
             if (mat.HasProperty(numericPropertyName))
             {
                propertyShaderID = Shader.PropertyToID(numericPropertyName);
                propertyShaderIDs.Add(propertyShaderID);
             } 
            else DestroyComponentAndLogError(gameObject.name + "'s Material doesn't have a " + numericPropertyName + " property");
          
            mat.SetFloat(propertyShaderID, 0);
            Debug.Log(propertyShaderID);

        }
        currValue = 0;
      
    }

    private bool isValid = true;
    private void Update()
    {

        for(int i=0;i<mats.Length;++i)
        {
            mats[i].SetFloat(propertyShaderIDs[i], currValue);
        }
    }

    private void FlipGoingUp()
    {
        goingUp = !goingUp;
        scrollSpeed *= -1f;
    }

    public void ChangeMatColor(float scale)
    {
        //Debug.Log(scale);
        currValue=180*scale;
    }

    private void DestroyComponentAndLogError(string logError)
    {
        //Debug.LogError(logError);
        Destroy(this);
    }

   
}

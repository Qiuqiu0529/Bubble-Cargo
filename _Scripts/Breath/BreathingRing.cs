using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;
public class BreathingRing : MonoBehaviour
{
    public Transform ring; 
    public Transform trailObj; 
    public float breatheScaleAmount = 0.2f; 
    public float radius = 2f;
    public float moveSpeed = 2f;
    private float angle = 0f;

    private void Update()
    {        
        float breatheScale = 1f + breatheScaleAmount;
        float x = ring.localPosition.x + Mathf.Cos(angle) * radius * breatheScale;
        float y = ring.localPosition.y + Mathf.Sin(angle) * radius * breatheScale;
        trailObj.localPosition = new Vector3(x, y, 0);
        angle += Time.deltaTime * moveSpeed;
        // moving in circles
        if (angle >= 2 * Mathf.PI)
        {
            angle = 0f;
        }
    }
}

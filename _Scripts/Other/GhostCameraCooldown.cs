using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
public class GhostCameraCooldown : MonoBehaviour
{
    public MMGhostCamera ghostCamera;
    void Start()
    {
        StartCoroutine(CoolDownCamera());
    }
    IEnumerator CoolDownCamera()
    {
        yield return new WaitForSeconds(2f);
        ghostCamera.enabled = true;
    }

}

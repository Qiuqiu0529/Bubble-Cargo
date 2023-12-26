using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeMgrWarpper : MonoBehaviour
{
   void Start()
   {
       ModeMgr.Instance.SetNormal();
   }
   public void ToggleMode()
   {
      ModeMgr.Instance.ToggleMode();
   }
}

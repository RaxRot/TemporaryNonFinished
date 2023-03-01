using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : Point
{
   protected override void OnTriggerEnter2D(Collider2D col)
   {
      base.OnTriggerEnter2D(col);
      
      GameManager.Instance.SaveBestTime();
   }
}

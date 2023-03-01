using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
   protected virtual void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag(TagManager.PLAYER_TAG))
      {
         PlayerController playerController = col.GetComponent<PlayerController>();
         playerController.KnockBack();
      }
   }
}

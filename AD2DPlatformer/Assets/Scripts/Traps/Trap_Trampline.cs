using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trampline : MonoBehaviour
{
    [SerializeField] private float pushForce = 25f;

    private bool _canBeUsed = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG) && _canBeUsed)
        {
            _canBeUsed = false;
            
            GetComponent<Animator>().SetTrigger(TagManager.OBJECT_ACTIVATE_TRIGGER);
            
            col.GetComponent<PlayerController>().Push(pushForce);
        }
    }

    public void CanUseAgain()
    {
        _canBeUsed = true;
    }
}

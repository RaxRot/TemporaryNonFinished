using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Switcher : MonoBehaviour
{
    private Animator _anim;

    [SerializeField] private Trap_Fire myTrap;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            _anim.SetTrigger(TagManager.TRAP_PRESSED_ANIMATION_TRIGGER);
            myTrap.FireSwitch();
            myTrap.ResetFireSwitchAfter(7f);
        }
    }
}

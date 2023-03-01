using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : Trap
{
    private Animator _anim;
    
    [SerializeField] private bool isWorking;
    [SerializeField] private float repeatRate = 3f;
    [SerializeField] private bool hasSwitcher;
    

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!hasSwitcher)
        {
            InvokeRepeating("FireSwitch",0f,repeatRate);
        }
    }

    private void Update()
    {
        _anim.SetBool(TagManager.IS_WORKING_ANIMATION_PARAMETR,isWorking);
    }

    public void FireSwitch()
    {
        isWorking = !isWorking;
    }

    public void ResetFireSwitchAfter(float seconds)
    {
        Invoke("FireSwitch",seconds);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (isWorking)
        {
            base.OnTriggerEnter2D(col);
        }
    }
}

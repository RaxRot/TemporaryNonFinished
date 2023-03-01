using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{
    [SerializeField] private float speed = 2f;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        Rb.velocity = new Vector2(speed *transform.localScale.x, Rb.velocity.y); 

        CollisionsCheck();
        
        if (wallDetected || !groundDetected)
        {
            moveRight = !moveRight;
        }
        
        SwitchDirection();
        
    }
    
}

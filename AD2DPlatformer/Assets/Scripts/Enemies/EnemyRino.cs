using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRino : Enemy
{
    [SerializeField] private float speed = -3f;
    [SerializeField] private float detectionSpeed = -6f;

    [SerializeField] private LayerMask playerDetectionLayer;
    [SerializeField] private Transform playerCheckPosition;
    [SerializeField] private float distanceToCheckPlayer = 12f;
    private bool _isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        _isPlayerDetected = Physics2D.Raycast(playerCheckPosition.position, Vector2.left*transform.localScale.x, distanceToCheckPlayer,
            playerDetectionLayer);

        if (!_isPlayerDetected)
        {
            Rb.velocity = new Vector2(speed *transform.localScale.x, Rb.velocity.y); 
        }
        else
        {
            Rb.velocity = new Vector2(detectionSpeed *transform.localScale.x, Rb.velocity.y); 
        }

        CollisionsCheck();
        
        if (wallDetected || !groundDetected)
        {
            moveRight = !moveRight;
        }
        
        SwitchDirection();
    }
}

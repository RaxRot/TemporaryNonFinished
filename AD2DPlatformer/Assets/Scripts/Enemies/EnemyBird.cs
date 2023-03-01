using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBird : Enemy
{
    
    [SerializeField] private float moveSpeed = 2f;
    
    [Header("Position Fly Info")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private Vector3 targetPosition;
    
    private float previousXPos;

    protected override void Start()
    {
        base.Start();
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
    }

    private void Update()
    {
        MoveToTargetPos();
    }

    private void MoveToTargetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            previousXPos = transform.position.x;
        }
        
        ChangeFacingDirection();
    }

    private void ChangeFacingDirection()
    {
        if (transform.position.x>previousXPos)
        {
            Sr.flipX = true;
        }else if (transform.position.x<previousXPos)
        {
            Sr.flipX = false;
        }
    }
}

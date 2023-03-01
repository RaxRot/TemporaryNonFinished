using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed = 8f;

    [SerializeField] private bool isMovDown;

    [SerializeField] private float lifeTime = 5f;
    private float _counter;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _counter += Time.deltaTime;
        if (_counter>=lifeTime)
        {
            Destroy(gameObject);
        }
        
        if (isMovDown)
        {
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(moveSpeed, 0f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            col.GetComponent<PlayerController>().KnockBack();
        }
        
        Destroy(gameObject);
    }

    public void ShouldMoveRight(bool isMovingRight)
    {
        if (!isMovingRight)
        {
            moveSpeed *= -1f;
        }
    }
}

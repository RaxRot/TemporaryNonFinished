using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected Rigidbody2D Rb;
    protected SpriteRenderer Sr;

    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    
    protected bool wallDetected;
    protected bool groundDetected;
    protected bool moveRight;

    protected Vector3 temp;

    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();

        moveRight = Random.Range(0,2)==0;

    }

    public virtual void Damage()
    {
        Anim.SetTrigger(TagManager.ENEMY_GOT_HIT_ANIMATION_TRIGGER);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<PlayerController>()!=null)
        {
            PlayerController player = col.collider.GetComponent<PlayerController>();
            player.KnockBack();
        }
    }
    
    protected virtual void CollisionsCheck()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
    }

    protected void SwitchDirection()
    {
        temp = transform.localScale;
        
        if (moveRight)
        {
            temp.x = -1f;
        }
        else
        { 
            temp.x = 1f;
        }

        transform.localScale = temp;
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGost : Enemy
{

    private CircleCollider2D _collider;

    [SerializeField] private float moveSpeed = 3f;

    private bool _canDamagePlayer;
    private bool _canMove;
    
    [Header("Position Fly Info")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private Vector3 targetPosition;
    private Vector3 _spawnPosition;
    
    private float previousXPos;
    
    protected override void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        
        base.Start();
        
        _canDamagePlayer = true;
        
        GostBehaviour();

        _canMove = true;
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
    }

    private void Update()
    {
        if (_canMove)
        {
            MoveGost();
        }
    }

    private void MoveGost()
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

    public void Appear()
    {
        _canMove = true;
        _collider.enabled = true;
        _canDamagePlayer = true;
        Sr.enabled = true;
    }

    public void Dissapear()
    {
        _canMove = false;
        _collider.enabled = false;
        _canDamagePlayer = false;
        Sr.enabled = false;
    }
    
    public override void Damage()
    {
        if (_canDamagePlayer)
        {
            base.Damage();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>()!=null && _canDamagePlayer)
        {
            PlayerController player = col.GetComponent<PlayerController>();
            player.KnockBack();
        }
    }

    private void GostBehaviour()
    {
        StartCoroutine("_GostBehaviourCo");
    }

    private IEnumerator _GostBehaviourCo()
    {
        yield return new WaitForSeconds(6f);
        
        Anim.SetTrigger(TagManager.ENEMY_DISSAPEAR_ANIMATION_TRIGGER);
        
        yield return new WaitForSeconds(4f);
        
        
        _spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        transform.position = _spawnPosition;
        
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        
        Anim.SetTrigger(TagManager.ENEMY_APPEAR_ANIMATION_TRIGGER);

        GostBehaviour();
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trap_Saw : Trap
{
    private Animator _anim;

    [SerializeField] private bool isWorking;

    [SerializeField] private Transform[] movePoints;
    private int _currentPointIndex;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float distanceToChangePoint = 0.1f;

    [SerializeField] private float minCoolDown = 1f;
    [SerializeField] private float maxCoolDown = 3f;
    private float _coolDown;
    private float _coolDownTimer;

    private Vector3 _temp;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ControlMovement();
    }

    private void ControlMovement()
    {
        _coolDownTimer -= Time.deltaTime;
        isWorking = _coolDownTimer < 0;
        _anim.SetBool(TagManager.IS_WORKING_ANIMATION_PARAMETR,isWorking);

        if (isWorking)
        {
            transform.position=Vector3.MoveTowards
                (transform.position,movePoints[_currentPointIndex].position,speed*Time.deltaTime);
            
            Flip();
        }
        
        if (Vector3.Distance(transform.position, movePoints[_currentPointIndex].position) <= distanceToChangePoint)
        {
            _currentPointIndex++;

            if (_currentPointIndex>=movePoints.Length)
            {
                _currentPointIndex = 0;
            }

            _coolDown = Random.Range(minCoolDown, maxCoolDown);
            _coolDownTimer = _coolDown;
        }
        
    }

    private void Flip()
    {
        _temp = transform.localScale;
        
        if (transform.position.x<movePoints[_currentPointIndex].position.x)
        {
            _temp.x = -1f;
        }
        else
        {
            _temp.x = 1f;
        }

        transform.localScale = _temp;
    }
}

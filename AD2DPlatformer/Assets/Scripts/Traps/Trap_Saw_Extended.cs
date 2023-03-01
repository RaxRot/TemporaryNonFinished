using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw_Extended : MonoBehaviour
{
     private Animator _anim;
    
    private bool _isWorking;
    [SerializeField] private Transform[] movePoints;
    private int _currentPointIndex;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float distanceToChangePoint = 0.1f;

    private bool _isMoveForward;
    
    private Vector3 _temp;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _isWorking = true;
        _anim.SetBool(TagManager.IS_WORKING_ANIMATION_PARAMETR,_isWorking);
        _isMoveForward = true;
    }

    private void Update()
    {
        ControlMovement();
        
        Flip();
    }

    private void ControlMovement()
    {

        if (_isMoveForward)
        {
            transform.position=Vector3.MoveTowards
                (transform.position,movePoints[_currentPointIndex].position,speed*Time.deltaTime);
        
            if (Vector3.Distance(transform.position, movePoints[_currentPointIndex].position) <= distanceToChangePoint)
            {
                _currentPointIndex++;

                if (_currentPointIndex>=movePoints.Length)
                {
                    _isMoveForward = false;
                    _currentPointIndex = movePoints.Length-1;
                }
            }
        }
        else
        {
            transform.position=Vector3.MoveTowards
                (transform.position,movePoints[_currentPointIndex].position,speed*Time.deltaTime);
            
            if (Vector3.Distance(transform.position, movePoints[_currentPointIndex].position) <= distanceToChangePoint)
            {
                _currentPointIndex--;

                if (_currentPointIndex<=0)
                {
                    _isMoveForward = true;
                    _currentPointIndex = 0;
                }
            }
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

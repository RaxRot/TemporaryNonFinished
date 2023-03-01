using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trap_Spiked_Ball : Trap
{
    private Rigidbody2D _rb;

    [SerializeField] private Vector2 pushDirection;

    [SerializeField] private float minTimeToNewPush = 10;
    [SerializeField] private float maxTimeToNewPush = 25;
    private float _timeToNewPush;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //PushSpike();
        
        _rb.AddForce(pushDirection,ForceMode2D.Impulse);
    }

    private void PushSpike()
    {
        StartCoroutine(nameof(_PushSpikeCo));
    }

    private IEnumerator _PushSpikeCo()
    {
        _rb.AddForce(pushDirection,ForceMode2D.Impulse);

        _timeToNewPush = Random.Range(minTimeToNewPush, maxTimeToNewPush);

        yield return new WaitForSeconds(_timeToNewPush);
        
        StartCoroutine(nameof(_PushSpikeCo));
    }
    
}

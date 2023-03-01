using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Point
{
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        PlayerManager.Instance.respawnPoint = transform;
    }
}

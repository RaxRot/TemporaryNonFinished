using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            GetComponent<Animator>().SetTrigger(TagManager.OBJECT_ACTIVATE_TRIGGER);
        }
    }
}

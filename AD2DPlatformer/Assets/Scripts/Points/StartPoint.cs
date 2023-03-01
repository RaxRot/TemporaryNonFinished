using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform respPoint;
    
    private void Awake()
    {
        PlayerManager.Instance.respawnPoint = respPoint;
        
        PlayerManager.Instance.PlayerRespawn();
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            if (other.transform.position.x>transform.position.x)
            {
                if (!GameManager.Instance.startTime)
                {
                    GameManager.Instance.startTime = true;
                }
                
                GetComponent<Animator>().SetTrigger(TagManager.POINT_TOUCH_TRIGGER);
            }
        }
    }
}

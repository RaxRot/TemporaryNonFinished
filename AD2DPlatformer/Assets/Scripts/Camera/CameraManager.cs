using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject myCamera;

    private void Start()
    {
        myCamera.GetComponent<CinemachineVirtualCamera>().Follow =
            PlayerManager.Instance.currentPlayer.transform;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>()!=null)
        {
            myCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>()!=null)
        {
            myCamera.SetActive(false);
        }
    }
}

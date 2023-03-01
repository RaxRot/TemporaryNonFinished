using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulse;

    [SerializeField] private Vector3 shakeDirection;

    public void ScreenShake(int facingDirection)
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x * facingDirection, shakeDirection.y);
        impulse.GenerateImpulse();
    }
}

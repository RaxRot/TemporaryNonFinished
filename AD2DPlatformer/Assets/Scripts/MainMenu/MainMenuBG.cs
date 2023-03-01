using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    private Material _material;
    
    [SerializeField] private Vector2 scrollSpeed;
    
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        _material.mainTextureOffset +=scrollSpeed*Time.deltaTime;
    }
}

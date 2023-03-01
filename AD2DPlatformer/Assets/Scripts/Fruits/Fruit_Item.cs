using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit_Item : MonoBehaviour
{
    public enum FruitType
    {
        Apple,
        Banana,
        Chery,
        Kiwi,
        Melon,
        Orange,
        PineApple,
        StrawBerry
    }
    public FruitType myFruitType;

    private Animator _anim;
    
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] fruitsImage;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        for (int i = 0; i < _anim.layerCount; i++)
        {
            _anim.SetLayerWeight(i,0);
        }
        
        _anim.SetLayerWeight((int)myFruitType,1);
        
    }

    private void OnValidate()
    {
        sr.sprite = fruitsImage[(int)myFruitType];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            PlayerManager.Instance.fruits++;
            
            Destroy(gameObject);
            
            //gm fruits++player.fruits;
        }
    }
}

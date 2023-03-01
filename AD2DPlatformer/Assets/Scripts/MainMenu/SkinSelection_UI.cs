using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int skinId;

    [SerializeField] private bool[] skinPurchased;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject equipButton;

    [SerializeField] private int[] priceForSkin;

    private string _price; 

    private void Start()
    {
        skinPurchased[0] = true;
        
        _price = "Price: ";

        SetUpSkinInfo();
    }


    public void NextSkin()
    {
        skinId++;

        if (skinId>3)
        {
            skinId = 0;
        }
        
        SetUpSkinInfo();
    }

    public void PreviousSkin()
    {
        skinId--;

        if (skinId<0)
        {
            skinId = 3;
        }
        
        SetUpSkinInfo();
    }

    private void SetUpSkinInfo()
    {
        equipButton.SetActive(skinPurchased[skinId]);
        buyButton.SetActive(!skinPurchased[skinId]);

        if (!skinPurchased[skinId])
        {
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text =_price + priceForSkin[skinId];
        }

        animator.SetInteger(TagManager.SKIN_ID_ANIMATION_PARAMETR,skinId);
    }

    public void Buy()
    {
        ///
        skinPurchased[skinId] = true;
        
        SetUpSkinInfo();
    }

    public void Select()
    {
        PlayerManager.Instance.chosenSkinId = skinId;
        Debug.Log("Skin was equiped");
    }
}

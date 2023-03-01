using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    private GameObject _newBtn;
    
    [SerializeField] private Transform parentForButtons;

    [SerializeField] private bool[] levelOpen;
    

    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i])
            {
                return;
            }
            
            string sceneName = "Level " + i;
            string levelToLoadName = "Level" + i;
            _newBtn = Instantiate(levelButton, parentForButtons); 
            //_newBtn.AddComponent<Button>().onClick.AddListener(() => LoadLevel(_levelToLoadName));
            _newBtn.GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelToLoadName));
            _newBtn.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
        }
    }

    private void LoadLevel(string nameToLoad)
    {
        GameManager.Instance.SaveGameDifficulty();
        
        SceneManager.LoadScene(nameToLoad);
    }
}

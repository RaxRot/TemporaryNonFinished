using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int difficulty;

    [Header("Timer")] 
    public float timer;
    public bool startTime;

    [Header("Level Info")] 
    public int levelNumber;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (difficulty==0)
        {
            difficulty = PlayerPrefs.GetInt(TagManager.GAME_DIFFICULTY_PREFS);
        }
        
        print(PlayerPrefs.GetFloat("Level"+levelNumber+"BestTime"));
    }

    private void Update()
    {
        if (startTime)
        {
            timer += Time.deltaTime;
        }
    }

    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt(TagManager.GAME_DIFFICULTY_PREFS,difficulty);
    }

    public void SaveBestTime()
    {
        startTime = false;
        
        float lastTime=PlayerPrefs.GetFloat("Level"+levelNumber+"BestTime");

        if (timer<lastTime)
        {
            PlayerPrefs.SetFloat("Level"+levelNumber+"BestTime",timer);
        }
        
        timer = 0;
    }
    
}

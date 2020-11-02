﻿using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public PlayerStatistics SavedPlayerData = new PlayerStatistics();

    public static GlobalControl Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
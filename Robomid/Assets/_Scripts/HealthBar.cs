using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    private PlayerState playerStats;

    void Start()
    {
        healthBar = GetComponent<Slider>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        healthBar.maxValue = playerStats.LocalPlayerData.BaseHP;
        healthBar.value = playerStats.LocalPlayerData.HP;
    }

    void Update()
    {
        healthBar.value = playerStats.LocalPlayerData.HP;
    }
}

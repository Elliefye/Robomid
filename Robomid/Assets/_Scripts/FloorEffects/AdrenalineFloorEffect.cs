using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AdrenalineFloorEffect : MonoBehaviour
{
    private List<AIController> EnemiesAi = new List<AIController>();
    private List<PlayerState> PlayersAi = new List<PlayerState>();

    void Start()
    {
        SceneManager.sceneLoaded += OnLevelLoad;
    }

    void OnLevelLoad(Scene scene, LoadSceneMode sceneMode)
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            EnemiesAi.Add(enemy.GetComponent<AIController>());
        }

        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            PlayersAi.Add(player.GetComponent<PlayerState>());
        }
    }

    void Update()
    {
        int enemyHP;
        int baseEnemySpeed;
        foreach (var enemyAi in EnemiesAi)
        {
            enemyHP = enemyAi.Health;
            baseEnemySpeed = enemyAi.DefaultSpeed;
            enemyAi.MoveSpeed = baseEnemySpeed + (baseEnemySpeed - baseEnemySpeed * enemyHP / 10);
        }
        
        int playerHP;
        int basePlayerSpeed;
        foreach (var playerAi in PlayersAi)
        {
            playerHP = playerAi.LocalPlayerData.HP;
            basePlayerSpeed = playerAi.LocalPlayerData.BaseSpeed;
            playerAi.LocalPlayerData.Speed = basePlayerSpeed + (basePlayerSpeed - basePlayerSpeed * playerHP / 100);
        }
    }
}

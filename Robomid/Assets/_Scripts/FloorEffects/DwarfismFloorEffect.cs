using UnityEngine;
using UnityEngine.SceneManagement;

public class DwarfismFloorEffect : MonoBehaviour
{
    void Start()
    {
        SceneManager.sceneLoaded += OnLevelLoad;
    }

    void OnLevelLoad(Scene scene, LoadSceneMode sceneMode)
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.transform.localScale /= 1.5f;
        }

        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            player.transform.localScale /= 1.5f;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachGoal : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //if (enemies.Length == 0)
            //{
            GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            DungeonGeneration dungeonGeneration = dungeon.GetComponent<DungeonGeneration>();
            dungeonGeneration.ResetDungeon();

            col.gameObject.GetComponent<PlayerState>().LocalPlayerData.DirectionFrom = string.Empty;
            col.gameObject.GetComponent<PlayerState>().SavePlayer();

            SceneManager.LoadScene("Shop");
            //}
        }
    }
}

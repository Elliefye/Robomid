using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterRoom : MonoBehaviour
{

    [SerializeField]
    string Direction;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            //if (enemies.Length == 0)
            //{
                var dungeon = GameObject.FindGameObjectWithTag("Dungeon");
                var dungeonGeneration = dungeon.GetComponent<DungeonGeneration>();

                var room = dungeonGeneration.GetCurrentRoom();
                room.Clear();
                dungeonGeneration.MoveToRoom(room.Neighbor(Direction));

                col.gameObject.GetComponent<PlayerState>().LocalPlayerData.DirectionFrom = Direction;
                col.gameObject.GetComponent<PlayerState>().SavePlayer();

                SceneManager.LoadScene("Demo");
            //}
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterRoom : MonoBehaviour
{

    [SerializeField]
    string direction;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            var dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            var dungeonGeneration = dungeon.GetComponent<DungeonGeneration>();

            var room = dungeonGeneration.CurrentRoom();
            dungeonGeneration.MoveToRoom(room.Neighbor(direction));

            col.gameObject.GetComponent<PlayerState>().localPlayerData.DirectionFrom = direction;
            col.gameObject.GetComponent<PlayerState>().SavePlayer();

            SceneManager.LoadScene("Demo");
        }
    }
}

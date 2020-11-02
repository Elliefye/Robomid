using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerStatistics LocalPlayerData = new PlayerStatistics();

    public void SavePlayer()
    {
        GlobalControl.Instance.SavedPlayerData = LocalPlayerData;
    }

    void Start()
    {
        LocalPlayerData = GlobalControl.Instance.SavedPlayerData;
        MovePlayerToDoor();
    }

    void MovePlayerToDoor()
    {
        switch (LocalPlayerData.DirectionFrom)
        {
            case "N":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door S").transform.position + Vector3.up * 1.2f;
                break;
            case "E":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door W").transform.position + Vector3.right * 1.2f;
                break;
            case "S":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door N").transform.position + Vector3.down * 1.2f;
                break;
            case "W":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door E").transform.position + Vector3.left * 1.2f;
                break;
        }
    }
}

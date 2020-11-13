using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public int DistanceAway = 10;
    private Transform Player;

    public void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    public void LateUpdate()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y, Player.position.z - DistanceAway);
    }
}

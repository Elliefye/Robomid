using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public int DistanceAway = 10;
    private Transform player;

    public void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    public void LateUpdate()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, player.position.z - DistanceAway);
    }
}

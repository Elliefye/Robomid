using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string itemName;
    public int quantity;

    private GameObject player;
    private PlayerState playerState;
 
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        playerState = player.GetComponent<PlayerState>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(itemName == "Coin")
            {
                playerState.LocalPlayerData.Money += quantity;
                Destroy(gameObject);
            }
            else if(itemName == "Heart")
            {
                playerState.LocalPlayerData.HP += quantity;
                Destroy(gameObject);
            }

        }
    }

}

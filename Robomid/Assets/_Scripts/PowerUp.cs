using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string itemName;
    public int quantity;
    private bool follow = false;

    private GameObject player;
    private PlayerState playerState;
 
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        playerState = player.GetComponent<PlayerState>();
        StartCoroutine(waitForPickup());
    }

    private void FixedUpdate()
    {
        if(follow)
        {
            Vector2 relativePos = player.transform.position - transform.position;
            float speed = 5f;

            transform.Translate(relativePos * speed * Time.deltaTime);
        }
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

    private IEnumerator waitForPickup()
    {
        yield return new WaitForSeconds(3);
        follow = true;
    }
}

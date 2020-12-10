using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string ItemName;
    public int Quantity;
    private bool IsFollowing = false;

    private GameObject player;
    private GameObject holding;
    private PlayerMovement playerMovement;
    private HoldWeapon holdWeapon;
    private PlayerState playerState;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        holding = GameObject.FindWithTag("Holding");
        playerMovement = player.GetComponent<PlayerMovement>();
        holdWeapon = holding.GetComponent<HoldWeapon>();
        playerState = player.GetComponent<PlayerState>();
        StartCoroutine(WaitForPickup());
    }

    private void FixedUpdate()
    {
        if (IsFollowing)
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
            other.GetComponent<PlayerMovement>().PlayItemSound();
            if (ItemName == "Coin")
            {
                playerState.LocalPlayerData.Money += Quantity;
                Destroy(gameObject);
            }
            else if (ItemName == "Heart")
            {
                playerState.LocalPlayerData.HP += Quantity;
                Destroy(gameObject);
            }
            else if (ItemName == "GoldHeart")
            {
                playerState.LocalPlayerData.HP += Quantity;
                Destroy(gameObject);
            }
            else if (ItemName == "Key")
            {
                playerState.LocalPlayerData.Keys += Quantity;
                Destroy(gameObject);
            }
            else if (ItemName == "Reset")
            {
                holdWeapon.showWeapon = false;
                playerMovement.CanAttack = false;
                Destroy(gameObject);
            }

        }
    }

    private IEnumerator WaitForPickup()
    {
        yield return new WaitForSeconds(1.5f);
        IsFollowing = true;
    }
}

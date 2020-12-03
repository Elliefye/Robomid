using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{

    public string ItemName;
    private GameObject player;
    private GameObject holding;
    private PlayerMovement playerMovement;
    private HoldWeapon holdWeapon;
    private PlayerState playerState;
    private bool isInRange = false;
    public GameObject[] itemDrops;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        holding = GameObject.FindWithTag("Holding");
        playerMovement = player.GetComponent<PlayerMovement>();
        holdWeapon = holding.GetComponent<HoldWeapon>();
        playerState = player.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            DropWeapon();
            PickUpWeapon();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
        }
    }

    public void PickUpWeapon()
    {
        if (isInRange)
        {
            if (ItemName == "BoomZooka")
            {
                playerState.LocalPlayerData.currentWeapon = WeaponEnums.Boomzooka;
                Destroy(gameObject);
            }
            else if (ItemName == "TaserPhaser")
            {
                playerState.LocalPlayerData.currentWeapon = WeaponEnums.TaserPhaser;
                Destroy(gameObject);
            }
            else if (ItemName == "LaserPointer")
            {
                playerState.LocalPlayerData.currentWeapon = WeaponEnums.LaserPointer9000;
                Destroy(gameObject);
            }
            else if (ItemName == "PlasmaShooter")
            {
                playerMovement.CanAttack = true;
                holdWeapon.showWeapon = true;
                playerState.LocalPlayerData.currentWeapon = WeaponEnums.PlasmaShooter;
                Destroy(gameObject);
            }
            else if (ItemName == "SemiManualGifle")
            {
                playerState.LocalPlayerData.currentWeapon = WeaponEnums.SemiManualGifle;
                Destroy(gameObject);
            }

        }
    }

    private void DropWeapon()
    {
        int currentWeapon = (int)playerState.LocalPlayerData.currentWeapon;

        if (currentWeapon > itemDrops.Length)
        {
            currentWeapon = 0;
        }

        if (currentWeapon != 0 && isInRange)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject item = Instantiate(itemDrops[currentWeapon], transform.position, transform.rotation);
        }
    }
}

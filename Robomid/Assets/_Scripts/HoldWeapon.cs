using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HoldWeapon : MonoBehaviour
{
    public Sprite[] upgrades;
    private int spriteIndex = 0;

    private GameObject player;
    private PlayerState playerState;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        player = GameObject.FindWithTag("Player");
        playerState = player.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (Input.GetButtonDown("FireRight") || Input.GetButtonDown("FireLeft") || Input.GetButtonDown("FireUp") || Input.GetButtonDown("FireDown"))
        { 
            var weaponType = playerState.LocalPlayerData.currentWeapon;
            spriteIndex = (int)weaponType;

            if (spriteIndex > 2)
            {
                spriteIndex = 0;
            }
            StartCoroutine(ExampleCoroutine());
        }
    }


    IEnumerator ExampleCoroutine()
    {
        GetComponent<SpriteRenderer>().sprite = upgrades[spriteIndex];
        yield return new WaitForSeconds(0.03f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
    }


}
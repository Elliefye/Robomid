using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using System.Security.AccessControl;

public class Chest : MonoBehaviour
{
    //public List<GameObject> powerUps;
    public GameObject[] powerUps;
    public Transform spawnPoint;
    public bool isLocked;

    //LootTable 
    public int[] table =
    {
        //percentage for getting a certain item
        40, // Money
        10, // BaseHP
        10, // Defence
        10, // Speed
        10, //Damage
        10, // Defence
        10 // Speed
    };

    [SerializeField]
    private SpriteRenderer spriteRendered;

    [SerializeField]
    private Sprite openSprite;

    private int total;
    bool isOpen = false;
    private int randomNumber;
    private GameObject player;
    private PlayerState playerState;

    private void Start()
    {
            player = GameObject.FindWithTag("Player");
            playerState = player.GetComponent<PlayerState>();

        //Template logic not implemented yet
        /*
        foreach(var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        foreach(var weight in table)
        {
            if(randomNumber <= weight)
            {
                //Debug.Log("Award: " + weight);
                return;
            }
            else
            {
                randomNumber -= weight;
            }
        }

        
        for (int i = 0; i < table.Lenght; i++)
        {
            if (randomNumber <= table[i])
            {
                //Debug.Log("Award: " + weight);
                //example lights[i].setActive(true)
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
        */


    }

    public void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isLocked)
            {
                UnlockChest();
            }
            else
            {
                spriteRendered.sprite = openSprite;
                OpenChest();
                isOpen = true;
            }
        }
    }

    void OpenChest()
    {
        if (!isOpen) 
        {
            GameObject item = Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPoint.position, spawnPoint.rotation) as GameObject;
        }
    }

    void UnlockChest()
    {
        if (playerState.LocalPlayerData.Keys >= 1)
        {
            playerState.LocalPlayerData.Keys--;
            isLocked = false;
        }
    }
}

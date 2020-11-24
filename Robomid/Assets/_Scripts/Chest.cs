using UnityEngine;

public class Chest : MonoBehaviour
{
    //public List<GameObject> PowerUps;
    public GameObject[] PowerUps;
    public Transform SpawnPoint;
    public bool IsLocked;

    //LootTable 
    public int[] table =
    {
        //percentage for getting a certain item
        40, // Money
        10, // BaseHP
        10, // Defence
        10, // Speed
        10, //Damaged
        10, // Defence
        10 // Speed
    };

    [SerializeField]
    private SpriteRenderer SpriteRendered;

    [SerializeField]
    private Sprite OpenSprite;

    private int total;
    public bool IsOpen = false;
    private int randomNumber;
    private GameObject Player;
    private PlayerState PlayerState;

    private void Start()
    {
            Player = GameObject.FindWithTag("Player");
            PlayerState = Player.GetComponent<PlayerState>();

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
            if (IsLocked)
            {
                UnlockChest();
            }
            else
            {
                SpriteRendered.sprite = OpenSprite;
                OpenChest();
                IsOpen = true;
            }
        }
    }

    void OpenChest()
    {
        if (!IsOpen) 
        {
            GameObject item = Instantiate(PowerUps[Random.Range(0, PowerUps.Length)], SpawnPoint.position, SpawnPoint.rotation);
        }
    }

    void UnlockChest()
    {
        if (PlayerState.LocalPlayerData.Keys >= 1)
        {
            PlayerState.LocalPlayerData.Keys--;
            IsLocked = false;
        }
    }
}

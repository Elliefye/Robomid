using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public PlayerStatistics LocalPlayerData = new PlayerStatistics();
    public bool invincible = false;
    public Text playerHealthDisplay;

    public void SavePlayer()
    {
        GlobalControl.Instance.SavedPlayerData = LocalPlayerData;
    }

    void Start()
    {
        LocalPlayerData = GlobalControl.Instance.SavedPlayerData;
        MovePlayerToDoor();
    }

    private void Update()
    {
        playerHealthDisplay.text = LocalPlayerData.HP.ToString();
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

    public void Damage(GameObject damager)
    {
        if(!invincible)
        {
            if (damager.name == "AK-5000(Clone)")
            {
                LocalPlayerData.HP -= 10;
            }
            GetComponent<PlayerMovement>().damaged = true;

            if (LocalPlayerData.HP <= 0)
            {
                //cia reik playint death animation bet neideta movement??? tai tsg sunaikina
                //GetComponent<PlayerMovement>().death = true; --sita atkomentuot ir istrint sekancia eilute kai idesim
                Destroy(gameObject);
                playerHealthDisplay.text = "Game over";
            }
        }
    }
}

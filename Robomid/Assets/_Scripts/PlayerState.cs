using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour, IShopCustomer
{
    public PlayerStatistics LocalPlayerData = new PlayerStatistics();
    public bool IsInvincible = false;
    public Text PlayerHealthDisplay;

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
        PlayerHealthDisplay.text = LocalPlayerData.HP.ToString();
    }

    void MovePlayerToDoor()
    {
        switch (LocalPlayerData.DirectionFrom)
        {
            case "N":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door S").transform.position + Vector3.up * 1.4f;
                break;
            case "E":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door W").transform.position + Vector3.right * 1.2f;
                break;
            case "S":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door N").transform.position + Vector3.down * 1.4f;
                break;
            case "W":
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("Door E").transform.position + Vector3.left * 1.2f;
                break;
        }
    }

    public void Damage(GameObject damager)
    {
        if (!IsInvincible)
        {
            //TODO paimti duomenis is pacio game object o ne hardcoded values pagal name.
            if (damager.name == "AK-5000(Clone)")
            {
                LocalPlayerData.HP -= 10;
            }

            GetComponent<PlayerMovement>().IsDamaged = true;

            if (LocalPlayerData.HP <= 0)
            {
                var pm = GetComponent<PlayerMovement>();
                if (pm.IsDead == false)
                    pm.death = true;
                PlayerHealthDisplay.text = "Game over";
            }
        }
    }

    public void Consumable(GameObject consumable)
    {
        //TODO paimti duomenis is pacio game object o ne hardcoded values pagal name.
        if (consumable.name == "Coin(Clone)")
        {
            LocalPlayerData.HP -= 10;
        }
        Destroy(gameObject);

    }

    public void BoughtItem(GameObject shopItem)
    {
        UnityEngine.Debug.Log("Bought " + shopItem.name);
        switch (shopItem.name)
        {
            default:
            case "Health":
                LocalPlayerData.HP += 10;
                break;
            case "TaserPhaser":
                LocalPlayerData.currentWeapon = 1;
                break;
            case "Boomzooka":
                LocalPlayerData.currentWeapon = 2;
                break;
            case "LaserPointer9000":
                //LocalPlayerData.currentWeapon = 3;
                break;
        }
    }
}

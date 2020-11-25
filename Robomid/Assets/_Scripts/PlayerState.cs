using UnityEngine;

public class PlayerState : MonoBehaviour, IShopCustomer
{
    public PlayerStatistics LocalPlayerData = new PlayerStatistics();
    public bool IsInvincible = false;

    public void SavePlayer()
    {
        GlobalControl.Instance.SavedPlayerData = LocalPlayerData;
        GlobalControl.Instance.SaveGame();
    }

    void Start()
    {
        LocalPlayerData = GlobalControl.Instance.SavedPlayerData;
        MovePlayerToDoor();
    }

    void MovePlayerToDoor()
    {
        try
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
                default:
                    break;
            }
        }
        catch
        {
            return;
        }
    }

    public void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            LocalPlayerData.HP -= damage;

            GetComponent<PlayerMovement>().IsDamaged = true;

            if (LocalPlayerData.HP <= 0)
            {
                var pm = GetComponent<PlayerMovement>();
                pm.Die();
            }
        }
    }

    public void BoughtItem(GameObject shopItem)
    {
        switch (shopItem.name)
        {
            case "Health":
                LocalPlayerData.HP += 10;
                break;
            case "TaserPhaser":
                LocalPlayerData.currentWeapon = WeaponEnums.TaserPhaser;
                break;
            case "Boomzooka":
                LocalPlayerData.currentWeapon = WeaponEnums.Boomzooka;
                break;
            case "LaserPointer9000":
                LocalPlayerData.currentWeapon = WeaponEnums.LaserPointer9000;
                break;
            default:
                break;
        }
    }

    public int GetCurrentHealth()
    {
        return LocalPlayerData.HP;
    }
}

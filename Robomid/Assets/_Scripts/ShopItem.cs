using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public static int GetPrice(string itemName)
    {
        switch (itemName)
        {
            default:
            case "Health": return 10;
            case "Boomzooka": return 100;
            case "TaserPhaser": return 90;
            case "LaserPointer": return 80;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private IShopCustomer shopCustomer;
    private PlayerStatistics LocalPlayerData;
    public Text PlayerMoneyDisplay;

    private void Start()
    {
        Hide();
        LocalPlayerData = GlobalControl.Instance.SavedPlayerData;
    }
    
    private void Update()
    {
        PlayerMoneyDisplay.text = LocalPlayerData.Money.ToString();
    }

    public void TryBuyItem(GameObject shopItem)
    {
        if (LocalPlayerData.Money >= ShopItem.GetPrice(shopItem.name))
        {
            LocalPlayerData.Money -= ShopItem.GetPrice(shopItem.name);
            shopCustomer.BoughtItem(shopItem);
        }
        
    }

    public void Show(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

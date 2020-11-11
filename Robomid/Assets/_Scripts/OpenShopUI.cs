using UnityEngine;

public class OpenShopUI : MonoBehaviour
{
    [SerializeField]
    private UI_Shop shopUI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if(shopCustomer != null)
        {
            shopUI.Show(shopCustomer);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            shopUI.Hide();
        }
    }
}

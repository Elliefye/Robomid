using UnityEngine;

public class FloorEffectDisplay : MonoBehaviour
{
    void Start()
    {
        foreach (FloorEffectEnums floorEffect in GlobalControl.Instance.FloorEffects)
        {
            transform.GetChild((int)floorEffect).gameObject.SetActive(true);
        }
    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloorEffectDisplay : MonoBehaviour
{
    GameObject currentEffect;
    void Start()
    {
        foreach (FloorEffectEnums floorEffect in GlobalControl.Instance.FloorEffects)
        {
            currentEffect = transform.GetChild((int)floorEffect).gameObject;
            currentEffect.SetActive(true);
        }
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        Image image = currentEffect.GetComponent<Image>();
        for(int i = 0; i < 5; i++)
        {
            switch (image.color.a.ToString())
            {
                case "0":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

}

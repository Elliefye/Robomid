using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloorEffectDisplay : MonoBehaviour
{
    GameObject currentEffect;
    [HideInInspector]
    public bool FirstTrigger = false;
    void Start()
    {
        if(GlobalControl.Instance.FloorEffects.Count != 0)
        {
            foreach (FloorEffectEnums floorEffect in GlobalControl.Instance.FloorEffects)
            {
                currentEffect = transform.GetChild((int)floorEffect).gameObject;
                currentEffect.SetActive(true);
            }
            if (FirstTrigger)
            {
                StartCoroutine(Blink());
            }
        }
    }

    IEnumerator Blink()
    {
        FirstTrigger = false;
        for(int i = 0; i < 7; i++)
        {
            switch (currentEffect.activeSelf)
            {
                case true:
                    currentEffect.SetActive(false);
                    yield return new WaitForSeconds(0.3f);
                    break;
                case false:
                    currentEffect.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                    break;
                default:
                    throw new System.Exception("Floor effect had an unexpected state: " + currentEffect.activeSelf);
            }
        }
        currentEffect.SetActive(true);
    }

}

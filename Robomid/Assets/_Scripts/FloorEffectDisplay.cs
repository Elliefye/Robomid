using System;
using System.Collections;
using System.Collections.Generic;
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

using System;
using UnityEngine;

public static class FloorEffectResolver
{
    public static void AddFloorEffect(GameObject gameObject, FloorEffectEnums floorEffectEnum)
    {
        switch ((int)floorEffectEnum)
        {
            case 0:
                gameObject.AddComponent<GigantismFloorEffect>();
                break;
            case 1:
                gameObject.AddComponent<DwarfismFloorEffect>();
                break;
            case 2:
                gameObject.AddComponent<ClaustrophobiaFloorEffect>();
                break;
            default:
                throw new ArgumentOutOfRangeException("Invalid effect");
        }
    }
}

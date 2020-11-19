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
            case 3:
                gameObject.AddComponent<AdrenalineFloorEffect>();
                break;
            case 4:
                gameObject.AddComponent<ZoomFloorEffect>();
                break;
            case 5:
                gameObject.AddComponent<VignetteFloorEffect>();
                break;
            case 6:
                gameObject.AddComponent<RecoilFloorEffect>();
                break;
            default:
                throw new ArgumentOutOfRangeException("Invalid effect");
        }
    }
}

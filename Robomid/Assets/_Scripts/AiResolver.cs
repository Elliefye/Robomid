using System;
using UnityEngine;

public static class AiResolver
{
    public static IAiLogic GetLogic(GameObject gameObject, AiEnums aiEnum)
    {
        switch ((int)aiEnum)
        {
            case 0:
                return gameObject.AddComponent<RhobatAi>();
            case 1:
                return gameObject.AddComponent<AlogAi>();
            case 2:
                return gameObject.AddComponent<HovereyeAi>();
            case 3:
                return gameObject.AddComponent<RobuddyAi>();
            case 4:
                return gameObject.AddComponent<RhobotAi>();
            case 5:
                return gameObject.AddComponent<RocketBotAi>();
            case 6:
                return gameObject.AddComponent<AK5000Ai>();
            default:
                throw new ArgumentOutOfRangeException("Invalid ai");
        }
    }
}

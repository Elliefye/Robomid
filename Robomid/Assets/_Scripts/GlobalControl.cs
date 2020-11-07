using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public PlayerStatistics SavedPlayerData = new PlayerStatistics();
    public readonly List<FloorEffectEnums> FloorEffects = new List<FloorEffectEnums>();

    public static GlobalControl Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddFloorEffect(FloorEffectEnums floorEffect)
    {
        GlobalControl.Instance.FloorEffects.Add(floorEffect);
        FloorEffectResolver.AddFloorEffect(Instance.gameObject, floorEffect);
    }
}

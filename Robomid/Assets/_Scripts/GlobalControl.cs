using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public PlayerStatistics SavedPlayerData = new PlayerStatistics();
    public List<FloorEffectEnums> FloorEffects = new List<FloorEffectEnums>();

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
        foreach (var floorEffect in Instance.FloorEffects)
        {
            FloorEffectResolver.AddFloorEffect(gameObject, floorEffect);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GlobalControl : MonoBehaviour
{
    public PlayerStatistics SavedPlayerData;
    public List<FloorEffectEnums> FloorEffects = new List<FloorEffectEnums>();

    public static GlobalControl Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            SavedPlayerData = LoadGame();
            FloorEffects = SavedPlayerData.FloorEffects;
            if (GameObject.FindWithTag("Player") == null)
                return;
            foreach(var floorEffect in FloorEffects)
            {
                FloorEffectResolver.AddFloorEffect(gameObject, floorEffect);
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddFloorEffect(FloorEffectEnums floorEffect)
    {
        Instance.FloorEffects.Add(floorEffect);
        FloorEffectResolver.AddFloorEffect(Instance.gameObject, floorEffect);
    }

    public void SaveGame(PlayerStatistics data)
    {
        data.FloorEffects = FloorEffects;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/pdata.rbm");
            bf.Serialize(file, data);
            file.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Game failed to save: \n" + e);
        }
    }

    public void SaveGame()
    {
        SavedPlayerData.FloorEffects = FloorEffects;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/pdata.rbm");
            bf.Serialize(file, SavedPlayerData);
            file.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Game failed to save: \n" + e);
        }
    }

    public PlayerStatistics LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/pdata.rbm"))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/pdata.rbm", FileMode.Open);
                var data = (PlayerStatistics)bf.Deserialize(file);
                file.Close();
                return data;
            }
            catch (Exception e)
            {
                Debug.Log("Game failed to load: \n" + e);
            }
        }
        return new PlayerStatistics();
    }
}

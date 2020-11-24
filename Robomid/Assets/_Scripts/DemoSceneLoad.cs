using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoSceneLoad : MonoBehaviour
{
    public Text map;
    public Text map2;
    void Start()
    {
        map.text = DungeonGeneration.Instance.PrintGrid();
        map2.text = Minimap.Instance.PrintMinimap();
    }
}

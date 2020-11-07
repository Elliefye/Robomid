using UnityEngine;

public class ClaustrophobiaFloorEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateWall", 2.0f, 5f);
    }

    // Update is called once per frame
    void CreateWall()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            var wallObject = (GameObject)Instantiate(Resources.Load($"FloorEffectResources/ClaustrophobiaWall"));
            wallObject.transform.position = enemy.transform.position;
        }
    }
}

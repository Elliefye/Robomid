using UnityEngine;

public class DwarfismFloorEffect : MonoBehaviour
{
    void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.transform.localScale /= 1.5f;
        }

        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            player.transform.localScale /= 1.5f;
        }
    }
}

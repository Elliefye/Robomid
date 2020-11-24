using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachGoal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
           //var enemies = GameObject.FindGameObjectsWithTag("Enemy");
           //if (enemies.Length == 0)
           // {
                ResetDungeon();
                SavePlayerData(collider);
                AddFloorEffect();

                SceneManager.LoadScene("Shop");
            //}
        }
    }

    private static void SavePlayerData(Collider2D col)
    {
        PlayerState playerState = col.gameObject.GetComponent<PlayerState>();
        playerState.LocalPlayerData.DirectionFrom = string.Empty;
        playerState.LocalPlayerData.CompletedFloors++;
        playerState.SavePlayer();
    }

    private void ResetDungeon()
    {
        var dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        var dungeonGeneration = dungeon.GetComponent<DungeonGeneration>();
        dungeonGeneration.ResetDungeon();
    }

    private void AddFloorEffect()
    {
        var values = System.Enum.GetValues(typeof(FloorEffectEnums))
            .Cast<FloorEffectEnums>()
            .Select(f => (int)f).ToList();
        
        var existingValues = GlobalControl.Instance.FloorEffects.ConvertAll(f => (int)f);

        values.RemoveAll(c => existingValues.Contains(c));

        if(values.Any())
        {
            var randomEffect = (FloorEffectEnums)values[Random.Range(0, values.Count)];
            GlobalControl.Instance.AddFloorEffect(randomEffect);
        }
    }
}

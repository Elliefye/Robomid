﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachGoal : MonoBehaviour
{
    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    private void OnEnable()
    {
        StopCoroutine(Flash());
        StartCoroutine(Flash());
    }

    void FixedUpdate()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = OpenSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
           var enemies = GameObject.FindGameObjectsWithTag("Enemy");
           if (enemies.Length == 0)
            {
                ResetDungeon();
                SavePlayerData(collider);
                AddFloorEffect();
                LevelAudio.Instance.Destroy();

                SceneManager.LoadScene("Shop");
            }
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
        Minimap.Instance.ResetMinimap();
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

    IEnumerator Flash()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        while(true)
        {
            sr.color = new Color(125, 84, 84);
            yield return new WaitForSeconds(0.3f);
            sr.color = Color.red;
            yield return new WaitForSeconds(0.3f);
        }
    }
}

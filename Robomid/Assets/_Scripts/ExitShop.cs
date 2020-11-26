using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitShop : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerState player = col.gameObject.GetComponent<PlayerState>();
            player.LocalPlayerData.DirectionFrom = "";
            player.LocalPlayerData.HP = player.LocalPlayerData.BaseHP;
            Minimap.Instance.ResetMinimap();

            SceneManager.LoadScene("Demo");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class RecoilFloorEffect : MonoBehaviour
{
    void Start()
    {
        SceneManager.sceneLoaded += OnLevelLoad;
    }
    // Start is called before the first frame update
    void OnLevelLoad(Scene scene, LoadSceneMode sceneMode)
    {
        //Smth wrong
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>().Recoil = true;
    }

}

using UnityEngine;

public class RecoilFloorEffect : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Smth wrong
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>().Recoil = true;
        Debug.Log(player.GetComponent<PlayerMovement>().Recoil);
    }

}

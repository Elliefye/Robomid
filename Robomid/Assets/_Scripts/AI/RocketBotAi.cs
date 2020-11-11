using UnityEngine;

public class RocketBotAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    void Start()
    {
        GetComponent<AIController>().Health = 100;
    }

    void Update()
    {

    }
}

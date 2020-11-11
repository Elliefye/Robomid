using UnityEngine;

public class RhobotAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    void Start()
    {
        GetComponent<AIController>().Health = 20;
    }

    void Update()
    {

    }
}

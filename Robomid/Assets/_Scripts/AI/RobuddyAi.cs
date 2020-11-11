using UnityEngine;

public class RobuddyAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    void Start()
    {
        GetComponent<AIController>().Health = 15;
    }

    void Update()
    {

    }
}

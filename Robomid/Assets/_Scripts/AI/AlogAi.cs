using UnityEngine;

public class AlogAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    void Start()
    {
        GetComponent<AIController>().Health = 13;
    }

    void Update()
    {

    }
}

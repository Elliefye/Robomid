using UnityEngine;

public class RhobatAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    void Start()
    {
        GetComponent<AIController>().Health = 5;
    }

    void Update()
    {

    }
}

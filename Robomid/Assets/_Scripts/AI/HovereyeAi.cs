using UnityEngine;

public class HovereyeAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    void Start()
    {
        GetComponent<AIController>().Health = 7;
    }

    void Update()
    {

    }
}

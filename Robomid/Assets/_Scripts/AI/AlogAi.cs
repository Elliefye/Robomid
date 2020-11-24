using UnityEngine;

public class AlogAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    public void Scale(int Level)
    {
        GetComponent<AIController>().Health += Level * 25/2;
        GetComponent<AIController>().Damage += Level * 5/2;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerState>().ReceiveDamage(GetComponent<AIController>().Damage);
        }
    }

    void Start()
    {
        GetComponent<AIController>().Health = 25;
        GetComponent<AIController>().Damage = 5;
    }

    void Update()
    {

    }
}

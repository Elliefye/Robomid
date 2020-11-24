using UnityEngine;

public class RobuddyAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    public void Scale(int level)
    {
        GetComponent<AIController>().Health += level * 15/2;
        GetComponent<AIController>().Damage += level * 10/2;
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
        GetComponent<AIController>().Health = 15;
        GetComponent<AIController>().Damage = 10;
    }

    void Update()
    {

    }
}

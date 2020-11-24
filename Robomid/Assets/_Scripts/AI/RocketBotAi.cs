using UnityEngine;

public class RocketBotAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    public void Scale(int level)
    {
        GetComponent<AIController>().Health += level * 100/2;
        GetComponent<AIController>().Damage += level * 30/2;
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
        GetComponent<AIController>().Health = 100;
        GetComponent<AIController>().Damage = 30;
    }

    void Update()
    {

    }
}

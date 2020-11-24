using UnityEngine;

public class RhobotAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    public void Scale(int level)
    {
        GetComponent<AIController>().Health += level * 20/2;
        GetComponent<AIController>().Damage += level * 15/2;
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
        GetComponent<AIController>().Health = 20;
        GetComponent<AIController>().Damage = 15;
    }

    void Update()
    {

    }
}

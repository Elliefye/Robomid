using UnityEngine;

public class RhobatAi : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        
    }

    public void Scale(int level)
    {
        GetComponent<AIController>().Damage += level * 15/2;
        GetComponent<AIController>().Health += level * 15/2;
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
        GetComponent<AIController>().Damage = 10;
        GetComponent<AIController>().Health = 15;
    }

    void Update()
    {

    }
}

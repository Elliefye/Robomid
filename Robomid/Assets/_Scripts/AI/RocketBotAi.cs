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
        var player = GameObject.FindGameObjectWithTag("Player");
        var distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer <= GetComponent<AIController>().SightRange && (transform.position.y > player.transform.position.y + 0.1 || transform.position.y < player.transform.position.y - 0.1))
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, player.transform.position.y), GetComponent<AIController>().MoveSpeed * Time.deltaTime);
        }
    }
}

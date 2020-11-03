using UnityEngine;

public class AK5000Ai : MonoBehaviour, IAiLogic
{
    public void Attack()
    {
        GameObject bullet = GetComponent<AIController>().bullet;
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //reik padaryt kad saudytu i playeri o ne transform.forward
        bulletInstance.GetComponent<Bullet>().SetValues(transform.forward, gameObject);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

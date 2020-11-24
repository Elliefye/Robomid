using UnityEngine;

public class AK5000Ai : MonoBehaviour, IAiLogic
{
    void Start()
    {
        GetComponent<AIController>().Health = 50;
        GetComponent<AIController>().Damage = 10;
    }

    public void Attack()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 relativedir = Snapping.Snap(player.position - transform.position, new Vector2(1f, 1f));
        GameObject bullet = GetComponent<AIController>().Bullet;
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletInstance.GetComponent<Bullet>().SetValues(returnApproximation(relativedir), gameObject, (int)WeaponEnums.AK5000laser);
    }

    private Vector2 returnApproximation(Vector2 number)
    {
        if(number.x >= 0)
        {
            if(number.x == 0)
            {
                if(number.y >= 0)
                {
                    if(number.y == 0)
                    {
                        return Vector2.up; //user is on top of enemy
                    }
                    else
                    {
                        return Vector2.up; //(0, 1)
                    }
                }
                else
                {
                    return Vector2.down; //(0, -1)
                }
            }
            else
            {
                if (number.y >= 0)
                {
                    if (number.y == 0)
                    {
                        return Vector2.right; //(1, 0)
                    }
                    else
                    {
                        return new Vector2(1f, 1f); //(1, 1)
                    }
                }
                else
                {
                    return new Vector2(1f, -1f); //(1, -1)
                }
            }
        }
        else
        {
            if (number.y >= 0)
            {
                if (number.y == 0)
                {
                    return Vector2.left; //(-1, 0)
                }
                else
                {
                    return new Vector2(-1f, 1f); //(-1, 1)
                }
            }
            else
            {
                return new Vector2(-1f, -1f); //(-1, -1)
            }
        }
    }

    public void Scale(int level)
    {
        GetComponent<AIController>().Health += level * 50 / 2;
        GetComponent<AIController>().Damage += level * 10 / 2;
    }
}

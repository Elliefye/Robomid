using UnityEngine;
/// <summary>
/// Handles bullet objects and on collision calls damage methods
/// </summary>
public class Bullet : MonoBehaviour
{
    private GameObject Shooter;
    [SerializeField]
    private Sprite[] Weapons; //change self sprite
    private int Speed = 1;
    private Vector2 Direction;
    private int weaponType = 0;

    public void SetValues(Vector2 direction, GameObject shooter, int weaponType = 0)
    {
        this.Shooter = shooter;
        this.Speed = getSpeed(weaponType);
        this.weaponType = weaponType;
        Transform sprite = transform.GetChild(0);
        sprite.GetComponent<SpriteRenderer>().sprite = Weapons[weaponType];
        this.Direction = direction;
        //this.Direction = Vector2.right;
        if (direction == Vector2.left)
            sprite.transform.Rotate(new Vector3(0, 0, 180));
        else if (direction == Vector2.up)
            sprite.transform.Rotate(new Vector3(0, 0, 90));
        else if (direction == Vector2.down)
            sprite.transform.Rotate(new Vector3(0, 0, -90));
        else if (direction == new Vector2(-1f, -1f)) //downleft
            sprite.transform.Rotate(new Vector3(0, 0, -135));
        else if (direction == new Vector2(1f, -1f)) //downright
            sprite.transform.Rotate(new Vector3(0, 0, -45));
        else if (direction == new Vector2(1f, 1f)) //upleft
            sprite.transform.Rotate(new Vector3(0, 0, 45));
        else if (direction == new Vector2(-1f, 1f)) //upright
            sprite.transform.Rotate(new Vector3(0, 0, 135));
    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (Direction != null)
            transform.Translate(Direction * Speed / 10);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Shooter.tag != "Player")
        {
            collision.gameObject.GetComponent<PlayerState>().Damage(Shooter);
        }
        else if (collision.gameObject.CompareTag("Enemy") && Shooter.tag != "Enemy")
        {
            collision.gameObject.GetComponent<AIController>().Damage(weaponType + 1);
        }

        Destroy(gameObject);
    }

    private int getSpeed(int weaponType)
    {
        return weaponType + 1;
    }
}

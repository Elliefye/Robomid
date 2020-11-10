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
        GetComponent<SpriteRenderer>().sprite = Weapons[weaponType];
        this.Direction = Vector2.right;
        if (direction == Vector2.left)
            transform.Rotate(new Vector3(0, 0, 180));
        else if (direction == Vector2.up)
            transform.Rotate(new Vector3(0, 0, 90));
        else if (direction == Vector2.down)
            transform.Rotate(new Vector3(0, 0, -90));
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerState>().Damage(Shooter);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
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

using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject bullet;
    private Animator Animator;

    // Use this for initialization
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Animator.SetBool("IsMoving", true);
        }
        else
        {
            Animator.SetBool("IsMoving", false);
        }

        Flip(horizontal);

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * Speed, vertical * Speed);
    }

    public void Update()
    {
        if (Input.GetButtonDown("FireUp"))
        {
            Shoot(Vector2.up);
        }
        if (Input.GetButtonDown("FireDown"))
        {
            Shoot(Vector2.down);
        }
        if (Input.GetButtonDown("FireLeft"))
        {
            Shoot(Vector2.left);
        }
        if (Input.GetButtonDown("FireRight"))
        {
            Shoot(Vector2.right);
        }
    }

    private void Flip(float movement)
    {
        transform.localRotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    private void Shoot(Vector2 direction)
    {
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletInstance.GetComponent<Bullet>().SetValues(direction, gameObject);
    }
}
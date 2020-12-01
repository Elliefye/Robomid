using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Handles all Player input and animations
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject Bullet;
    private Animator Animator;
    private Rigidbody2D Rigidbody;

    public bool IsDamaged = false;
    private bool CanAttack = true;
    public bool IsDead { get; private set; }
    private bool CanMove = true;
    public bool Recoil = false;

    public WeaponEnums weaponType;

    // Use this for initialization
    void Start()
    {
        Animator = GetComponent<Animator>();
        weaponType = GetComponent<PlayerState>().LocalPlayerData.currentWeapon;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Speed = GetComponent<PlayerState>().LocalPlayerData.Speed;
        if (CanMove)
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
    }

    public void Update()
    {
        if (CanAttack)
        {
            if (Input.GetButtonDown("FireUp"))
            {
                Shoot(Vector2.up);
            }
            else if (Input.GetButtonDown("FireDown"))
            {
                Shoot(Vector2.down);

            }
            else if (Input.GetButtonDown("FireLeft"))
            {
                Shoot(Vector2.left);
            }
            else if (Input.GetButtonDown("FireRight"))
            {
                Shoot(Vector2.right);
            }

        }
        if (IsDamaged && !IsDead)
        {
            Animator.Play("Player_hurt");
            IsDamaged = false;
        }
    }

    private void Flip(float movement)
    {
        transform.localRotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    public void Die()
    {
        IsDead = true;
        StartCoroutine(DeathAnimation());
    }

    private void Shoot(Vector2 direction)
    {
        var weaponType = GetComponent<PlayerState>().LocalPlayerData.currentWeapon;
        var bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletInstance.GetComponent<Bullet>().SetValues(direction, gameObject, (int)weaponType);
        Animator.Play("Base Layer.Player_cast");

        if (Recoil)
        {
            Rigidbody.AddForce(Rigidbody.position - direction * Rigidbody.mass * 500);
        }
    }

    private IEnumerator DeathAnimation()
    {
        CanAttack = false;
        CanMove = false;
        Animator.Play("Player_death");
        //laukt kol baigsis death animation
        yield return new WaitForSeconds(0.45f);
        Destroy(gameObject);
        SceneManager.LoadScene(3);
    }
}
﻿using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject Bullet;
    private Animator Animator;

    public bool IsDamaged = false;
    private bool CanAttack = true;
    public bool IsDead = false;
    private bool CanMove = true;

    // Use this for initialization
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        if (IsDamaged)
        {
            Animator.Play("Player_hurt");
            IsDamaged = false;
        }
        if (IsDead)
        {
            StartCoroutine(DeathAnimation());
            IsDead = false;
        }
    }

    private void Flip(float movement)
    {
        transform.localRotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    private void Shoot(Vector2 direction)
    {
        GameObject bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletInstance.GetComponent<Bullet>().SetValues(direction, gameObject);
        Animator.Play("Base Layer.Player_cast");
    }

    private IEnumerator DeathAnimation()
    {
        CanAttack = false;
        CanMove = false;
        Animator.SetTrigger("IsDead");
        //laukt kol baigsis IsDead animation
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
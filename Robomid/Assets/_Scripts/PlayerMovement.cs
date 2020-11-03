﻿using System.Collections;
using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject bullet;
    private Animator Animator;
    public bool damaged = false;
    private bool canAttack = true;
    public bool death = false;
    private bool canMove = true;

    // Use this for initialization
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove)
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
        if(canAttack)
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
        if(damaged)
        {
            StartCoroutine(DamageAnimation());
            damaged = false;
        }
        if(death)
        {
            StartCoroutine(DeathAnimation());
            death = false;
        }
    }

    private void Flip(float movement)
    {
        transform.localRotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    private void Shoot(Vector2 direction)
    {
        canAttack = false;
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 1)));
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bulletInstance.GetComponent<Bullet>().SetValues(direction, gameObject);
        Animator.SetBool("IsAttacking", true);
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator DamageAnimation()
    {
        Debug.Log("damage animation");
        GetComponent<PlayerState>().invincible = true;
        Animator.SetBool("IsHurt", true);
        Animator.Play("Base Layer.Player_hurt");

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("Player_hurt"))
        {
            for(int i = 0; i < 3; i++)
                yield return null;
        }

        Animator.Play("Base Layer.Player_idle");
        Animator.SetBool("IsHurt", false);
        GetComponent<PlayerState>().invincible = false;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        Animator.SetBool("IsAttacking", false);
        canAttack = true;
    }

    private IEnumerator DeathAnimation()
    {
        canAttack = false;
        canMove = false;
        Animator.SetTrigger("Death");
        //laukt kol baigsis death animation
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
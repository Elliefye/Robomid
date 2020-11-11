using System.Collections;
using UnityEngine;
/// <summary>
/// AiController handles global ai controlls for all enemies
/// </summary>
public class AIController : MonoBehaviour
{
    private IAiLogic _aiLogic;
    public AiEnums AiEnum;
    public int Health = 10;

    public int MoveSpeed = 4;

    public int SightRange = 3;
    public float FollowRange = 0;
    public float AttackRange = 0;
    public int AttackCooldown = 2;  //InSeconds
    public bool IsRotationEnabled = false;

    private bool IsAttacking = false;

    private GameObject Player;
    private Animator Animator;
    public GameObject Bullet;

    private bool IsAbleToAttack = true;
    public bool dead = false;
    public GameObject[] itemDrops;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _aiLogic = AiResolver.GetLogic(gameObject, AiEnum);
        Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(!dead)
        {
            var distanceFromPlayer = Vector2.Distance(transform.position, Player.transform.position);

            if (distanceFromPlayer <= SightRange)
            {
                Flip(transform.position.x - Player.transform.position.x);

                // Enemy rotation to follow player
                // going to leave for now but it sometimes looks weird
                if (IsRotationEnabled)
                {
                    Rotate();
                }

                if (distanceFromPlayer >= FollowRange)
                {
                    Animator.SetBool("IsMoving", true);
                    transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);
                }

                if (distanceFromPlayer <= AttackRange + 0.01f && !IsAttacking)
                {
                    Animator.SetBool("IsAttacking", true);
                    if (IsAbleToAttack)
                    {
                        _aiLogic.Attack();
                        IsAbleToAttack = false;
                        StartCoroutine(attackCooldown());
                    }
                    Invoke("StopAttackAnimation", AttackCooldown);
                }
            }

            else
            {
                Animator.SetBool("IsMoving", false);
            }
        }
    }

    private void Rotate()
    {
        var relativePos = Player.transform.position - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    private void Flip(float movement)
    {
        transform.localRotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }


    private void StopAttackAnimation()
    {
        Animator.SetBool("IsAttacking", false);
        IsAttacking = false;
    }
    
    private IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        IsAbleToAttack = true;
    }

    public void Damage(int amount)
    {
        Health -= amount;
        if (Health > 0)
        {
            Animator.Play("hurt");
        }
        else StartCoroutine(die());
    }

    private IEnumerator die()
    {
        Animator.Play("death");
        yield return new WaitForSeconds(0.5f);
        DropItem();
        Destroy(gameObject);
    }
    
    /*
    private void OnDestroy()
    {
        if (itemDrops.Length >= 1)
        {
            //Need to change randomization logic
            if (Random.Range(0, 101) > 50)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                GameObject item = Instantiate(itemDrops[Random.Range(0, itemDrops.Length)], transform.position, transform.rotation) as GameObject;
            }
        }
    }
    */
    private void DropItem()
    {
        if (itemDrops.Length >= 1)
        {
            //Need to change randomization logic
            if (Random.Range(0, 101) > 50)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                GameObject item = Instantiate(itemDrops[Random.Range(0, itemDrops.Length)], transform.position, transform.rotation) as GameObject;
            }
        }
    }

}
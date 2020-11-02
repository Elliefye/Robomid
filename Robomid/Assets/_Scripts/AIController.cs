using UnityEngine;
public class AIController : MonoBehaviour
{
    private IAiLogic _aiLogic;
    public AiEnums AiEnum;

    public int MoveSpeed = 4;

    public int SightRange = 3;
    public float FollowRange = 0;
    public float AttackRange = 0;
    public int AttackCooldown = 2;  //InSeconds

    private bool IsAttacking = false;

    private GameObject Player;
    private Animator Animator;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _aiLogic = AiResolver.GetLogic(gameObject, AiEnum);
        Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        var distanceFromPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (distanceFromPlayer >= FollowRange && distanceFromPlayer <= SightRange)
        {
            Animator.SetBool("IsMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);

            Flip(transform.position.x - Player.transform.position.x);
        }

        if (distanceFromPlayer <= AttackRange + 0.01f && !IsAttacking)
        {
            Animator.SetBool("IsAttacking", true);
            Flip(transform.position.x - Player.transform.position.x);
            _aiLogic.Attack();
            Invoke("StopAttackAnimation", AttackCooldown);
        }

        else
        {
            Animator.SetBool("IsMoving", false);
        }
    }

    void Flip(float movement)
    {
        transform.localRotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }


    void StopAttackAnimation()
    {
        Animator.SetBool("IsAttacking", false);
        IsAttacking = false;
    }
}
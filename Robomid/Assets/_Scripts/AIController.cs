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

        if(distanceFromPlayer <= SightRange)
        {
            Flip(transform.position.x - Player.transform.position.x);

            // Enemy rotation to follow player
            // going to leave for now but it sometimes looks weird
            transform.LookAt(Player.transform.position);
            transform.Rotate(new Vector2(0, -90), Space.Self);

            if (distanceFromPlayer >= FollowRange)
            {
                Animator.SetBool("IsMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime);

            }

            if (distanceFromPlayer <= AttackRange + 0.01f && !IsAttacking)
            {
                Animator.SetBool("IsAttacking", true);
                _aiLogic.Attack();
                Invoke("StopAttackAnimation", AttackCooldown);
            }
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
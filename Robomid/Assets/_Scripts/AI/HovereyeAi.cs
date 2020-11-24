using UnityEngine;

public class HovereyeAi : MonoBehaviour, IAiLogic
{
    private bool IsAlarmed = false;
    public void Attack()
    {
        if (!IsAlarmed)
        {
            IsAlarmed = true;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<AIController>().SightRange *= 2;
            }
        }
    }

    public void Scale(int level)
    {
        GetComponent<AIController>().MoveSpeed += level;
        GetComponent<AIController>().SightRange += level / 2;
        GetComponent<AIController>().Health += level * 20/2;
    }

    void Start()
    {
        GetComponent<AIController>().Health = 20;
    }

    void Update()
    {

    }

    void OnDestroy()
    {
        if (IsAlarmed)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<AIController>().SightRange /= 2;
            }
        }
    }
}

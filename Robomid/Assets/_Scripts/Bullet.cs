using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string shooterTag;
    [SerializeField]
    private Sprite[] weapons; //change self sprite
    private int speed = 1;
    private Vector2 direction;

    public void SetValues(Vector2 direction, GameObject shooter, int speed = 1)
    {
        shooterTag = shooter.tag;
        this.speed = speed;
        this.direction = direction;
    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if(direction != null)
            transform.Translate(direction * (float)speed/10);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(shooterTag)) 
        {
            Debug.Log("return");
            return;
        }
        else if(collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("Enemy")))
        {
            //reduce enemy/player health
        }
        Destroy(gameObject);
    }
}

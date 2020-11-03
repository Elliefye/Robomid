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
        this.direction = Vector2.right;
        if (direction == Vector2.left)
            transform.Rotate(new Vector3(0, 0, 180));
        else if(direction == Vector2.up)
            transform.Rotate(new Vector3(0, 0, 90));
        else if (direction == Vector2.down)
            transform.Rotate(new Vector3(0, 0, -90));
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
            //this should never occur but im leaving it just in case
            return;
        }
        else if(collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("Enemy")))
        {
            //reduce enemy/player health
        }
        Destroy(gameObject);
    }
}

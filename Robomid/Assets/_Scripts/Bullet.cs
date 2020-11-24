using System;
using UnityEngine;
/// <summary>
/// Handles bullet objects and on collision calls damage methods
/// </summary>
public class Bullet : MonoBehaviour
{
    private GameObject Shooter;
    [SerializeField]
    private Sprite[] Weapon_sprites; //change self sprite
    private int Speed = 1;
    private Vector2 Direction;
    private int weaponType = 0;

    public void SetValues(Vector2 direction, GameObject shooter, int weaponType = (int)WeaponEnums.TaserPhaser)
    {
        Shooter = shooter;
        Speed = GetSpeed((WeaponEnums)weaponType);
        this.weaponType = weaponType;
        Transform sprite = transform.GetChild(0);
        sprite.GetComponent<SpriteRenderer>().sprite = Weapon_sprites[weaponType];
        Direction = direction;
        if (direction == Vector2.left)
            sprite.transform.Rotate(new Vector3(0, 0, 180));
        else if (direction == Vector2.up)
            sprite.transform.Rotate(new Vector3(0, 0, 90));
        else if (direction == Vector2.down)
            sprite.transform.Rotate(new Vector3(0, 0, -90));
        else if (direction == new Vector2(-1f, -1f)) //downleft
            sprite.transform.Rotate(new Vector3(0, 0, -135));
        else if (direction == new Vector2(1f, -1f)) //downright
            sprite.transform.Rotate(new Vector3(0, 0, -45));
        else if (direction == new Vector2(1f, 1f)) //upleft
            sprite.transform.Rotate(new Vector3(0, 0, 45));
        else if (direction == new Vector2(-1f, 1f)) //upright
            sprite.transform.Rotate(new Vector3(0, 0, 135));
    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (Direction != null)
            transform.Translate(Direction * Speed / 10);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Shooter.tag != "Player")
        {
            collision.gameObject.GetComponent<PlayerState>().ReceiveDamage(Shooter.GetComponent<AIController>().Damage);
        }
        else if (collision.gameObject.CompareTag("Enemy") && Shooter.tag != "Enemy")
        {
            collision.gameObject.GetComponent<AIController>().Damaged(GetDamage((WeaponEnums)weaponType));
        }

        Destroy(gameObject);
    }

    //TODO kodėl čia du vienodos paskirties metodai ? xd GetSpeed ir GetDamage atlieka ta pati
    private int GetSpeed(WeaponEnums weaponType)
    {
        switch (weaponType)
        {
            case WeaponEnums.PlasmaShooter:
                return 1;
            case WeaponEnums.LaserPointer9000:
                return 2;
            case WeaponEnums.SemiManualGifle:
                return 1;
            case WeaponEnums.Boomzooka:
                return 3;
            case WeaponEnums.TaserPhaser:
                return 1;
            case WeaponEnums.AK5000laser:
                return 1;
            default:
                throw new IndexOutOfRangeException("Weapon out of range, got " + weaponType);
        }
    }

    private int GetDamage(WeaponEnums weaponType)
    {
        switch (weaponType)
        {
            case WeaponEnums.PlasmaShooter:
                return 1;
            case WeaponEnums.LaserPointer9000:
                return 2;
            case WeaponEnums.SemiManualGifle:
                return 1;
            case WeaponEnums.Boomzooka:
                return 3;
            case WeaponEnums.TaserPhaser:
                return 1;
            case WeaponEnums.AK5000laser:
                return 1;
            default:
                throw new IndexOutOfRangeException("Weapon out of range, got " + weaponType);
        }
    }
}

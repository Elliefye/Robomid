/// <summary>
/// Stores global Player data
/// </summary>
public class PlayerStatistics
{
    public int BaseHP = 100; //item prideda 10
    private int _HP = 100;
    public int Defence = 1; //prideda 0.1
    public int Speed = 1; //prideda 0.1
    public int Damage = 1; //prideda 0.1
    public int Money = 1000; //prideda 10
    public int Keys = 0;
    public string DirectionFrom;
    public WeaponEnums currentWeapon = WeaponEnums.PlasmaShooter; //default is 0

    public int HP
    {
        get
        {
            return _HP;
        }
        set
        {
            if(value <= BaseHP)
            {
                _HP = value;
            }
        }
    }
}

/// <summary>
/// Stores global player data
/// </summary>
public class PlayerStatistics
{
    public int BaseHP = 100; //item prideda 10
    private int _HP = 100;
    public int Defence = 1; //prideda 0.1
    public int Speed = 1; //prideda 0.1
    public int Damage = 1; //prideda 0.1
    public int Money = 0; //prideda 10
    public string DirectionFrom;
    public int currentWeapon = 2; //default is 0

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

using System.Collections.Generic;
/// <summary>
/// Stores global Player data
/// </summary>
[System.Serializable]
public class PlayerStatistics
{
    public int BaseHP = 100; //item prideda 10
    private int _HP = 100;
    public int Defence = 1; //prideda 0.1
    public int BaseSpeed = 5; //prideda 0.1
    public int Speed = 5; //prideda 0.1
    public int Damage = 5; //prideda 0.1
    public int Money = 1000; //prideda 10
    public int Keys = 0;
    public string DirectionFrom;
    public WeaponEnums currentWeapon = WeaponEnums.PlasmaShooter; //default is 0

    public int CompletedFloors = 0;
    public List<FloorEffectEnums> FloorEffects = new List<FloorEffectEnums>();

    public float Volume = 1;

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

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int UnitsT1;
    public int UnitsT2;
    public int UnitsT3;

    public int unitT1Power = 1;
    public int unitT2Power = 5;
    public int unitT3Power = 10;

    public int CalculatePlayerPower()
    {
        return (UnitsT1 * unitT1Power) + (UnitsT2 * unitT2Power) + (UnitsT3 * unitT3Power);
    }
}
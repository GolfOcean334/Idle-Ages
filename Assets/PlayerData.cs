using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int UnitsT1;
    public int UnitsT2;
    public int UnitsT3;
    public List<string> UnitT1Queue;
    public List<string> UnitT2Queue;
    public List<string> UnitT3Queue;
    public int resource1;
    public int resource2;
    public int resource3;
    public int resource4;
    public int resources1PerSecond;
    public int resources2PerSecond;
    public int resources3PerSecond;
    public int resources4PerSecond;
    public int ResearchPoint;
    public string LastSaveTime;
}

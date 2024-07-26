using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int UnitsT1;
    public int UnitsT2;
    public int UnitsT3;

    private readonly float MainMultiplicator = 1f;
    private readonly float SecondaryMultiplicator = 0.5f;

    private readonly float PvUnit1 = 70f;
    private readonly float DefUnit1 = 7f;
    private readonly float AttUnit1 = 30f;
    private readonly float BtechDefUnit1 = 0f;
    private readonly float BtechAttUnit1 = 0f;
    private readonly float EqPvUnit1 = 0f;
    private readonly float EqDefUnit1 = 0f;
    private readonly float EqAttUnit1 = 0f;

    private readonly float PvUnit2 = 150f;
    private readonly float DefUnit2 = 25f;
    private readonly float AttUnit2 = 15f;
    private readonly float BtechAttUnit2 = 0f;
    private readonly float BtechDefUnit2 = 0f;
    private readonly float EqPvUnit2 = 0f;
    private readonly float EqDefUnit2 = 0f;
    private readonly float EqAttUnit2 = 0f;

    private readonly float PvUnit3 = 100f;
    private readonly float DefUnit3 = 15f;
    private readonly float AttUnit3 = 20f;
    private readonly float BtechAttUnit3 = 0f;
    private readonly float BtechDefUnit3 = 0f;
    private readonly float EqPvUnit3 = 0f;
    private readonly float EqDefUnit3 = 0f;
    private readonly float EqAttUnit3 = 0f;

    public int CalculatePlayerPower()
    {
        float power = UnitsT1 * ((AttUnit1 * MainMultiplicator + BtechAttUnit1 + EqAttUnit1) + (DefUnit1 * SecondaryMultiplicator + BtechDefUnit1 + EqDefUnit1) + (PvUnit1 * SecondaryMultiplicator + EqPvUnit1))
                    + UnitsT2 * ((AttUnit2 * MainMultiplicator + BtechAttUnit2 + EqAttUnit2) + (DefUnit2 * SecondaryMultiplicator + BtechDefUnit2 + EqDefUnit2) + (PvUnit2 * SecondaryMultiplicator + EqPvUnit2))
                    + UnitsT3 * ((AttUnit3 * MainMultiplicator + BtechAttUnit3 + EqAttUnit3) + (DefUnit3 * SecondaryMultiplicator + BtechDefUnit3 + EqDefUnit3) + (PvUnit3 * SecondaryMultiplicator + EqPvUnit3));

        return Mathf.RoundToInt(power);
    }

    public void SaveAllUnits()
    {
        PlayerPrefs.SetInt("Units1", UnitsT1);
        PlayerPrefs.SetInt("Units2", UnitsT2);
        PlayerPrefs.SetInt("Units3", UnitsT3);
    }

    public void LoadSaveUnits()
    {
        UnitsT1 = PlayerPrefs.GetInt("Units1", 0);
        UnitsT2 = PlayerPrefs.GetInt("Units2", 0);
        UnitsT3 = PlayerPrefs.GetInt("Units3", 0);
    }

    public void ResetUnits()
    {
        UnitsT1 = 0;
        UnitsT2 = 0;
        UnitsT3 = 0;

        SaveAllUnits();
        PlayerPrefs.Save();
    }
}


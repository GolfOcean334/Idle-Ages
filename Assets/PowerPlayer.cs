using UnityEngine;
using TMPro;

public class PowerPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PowerPlayerText;
    [SerializeField] private PlayerStats playerStats;

    void Update()
    {
        int power = playerStats.CalculatePlayerPower();
        PowerPlayerText.text = "Player Power: " + FormatPower(power);
    }

    string FormatPower(int power)
    {
        if (power >= 1000000)
        {
            return (power / 1000000f).ToString("F1") + "M";
        }
        else if (power >= 1000)
        {
            return (power / 1000f).ToString("F1") + "k";
        }
        else
        {
            return power.ToString();
        }
    }
}

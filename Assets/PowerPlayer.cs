using UnityEngine;
using TMPro;

public class PowerPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PowerPlayerText;
    [SerializeField] private PlayerStats playerStats;

    void Update()
    {
        int power = playerStats.CalculatePlayerPower();
        PowerPlayerText.text = "Player Power: " + power;
    }
}

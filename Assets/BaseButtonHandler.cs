using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseButtonHandler : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private ResourceType resource;
    [SerializeField] private int resourceAmount;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI powerEnnemiesText;
    [SerializeField] private TextMeshProUGUI resourceEnemiesText;
    private static GameObject currentInfoPanel;
    private static TextMeshProUGUI currentPowerEnnemiesText;
    private static TextMeshProUGUI currentResourceEnemiesText;
    private static BaseButtonHandler currentBase;

    void Start()
    {
        if (currentInfoPanel == null)
        {
            currentInfoPanel = infoPanel;
            currentPowerEnnemiesText = powerEnnemiesText;
            currentResourceEnemiesText = resourceEnemiesText;
        }

        // Assigner la méthode OnClick à l'événement du bouton
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Initialize(int power, ResourceType resource, int resourceAmount, GameObject infoPanel, TextMeshProUGUI powerEnnemiesText, TextMeshProUGUI resourceEnemiesText)
    {
        this.power = power;
        this.resource = resource;
        this.resourceAmount = resourceAmount;
        this.infoPanel = infoPanel;
        this.powerEnnemiesText = powerEnnemiesText;
        this.resourceEnemiesText = resourceEnemiesText;
    }

    void OnClick()
    {
        if (currentBase == this)
        {
            // Hide the panel if the same base is clicked again
            currentInfoPanel.SetActive(false);
            currentBase = null;
        }
        else
        {
            // Show the panel with the power and resource information
            currentInfoPanel.SetActive(true);
            currentPowerEnnemiesText.text = "Puissance: " + FormatPower(power);
            currentResourceEnemiesText.text = "Resource: " + resource.ToString() + "\nQuantité: " + resourceAmount;
            currentBase = this;
        }
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

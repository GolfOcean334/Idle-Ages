using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseButtonHandler : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private ResourceType resource;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI powerEnnemiesText;
    [SerializeField] private TextMeshProUGUI resourceEnnemiesText;
    private static GameObject currentInfoPanel;
    private static TextMeshProUGUI currentpowerEnnemiesText; 
    private static TextMeshProUGUI currentresourceEnnemiesText;
    private static BaseButtonHandler currentBase;

    void Start()
    {
        if (currentInfoPanel == null)
        {
            currentInfoPanel = infoPanel;
            currentpowerEnnemiesText = powerEnnemiesText;
            currentresourceEnnemiesText = resourceEnnemiesText;
        }

        // Assigner la méthode OnClick à l'événement du bouton
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Initialize(int power, ResourceType resource, GameObject infoPanel, TextMeshProUGUI powerEnnemiesText, TextMeshProUGUI resourceEnnemiesText)
    {
        this.power = power;
        this.resource = resource;
        this.infoPanel = infoPanel;
        this.powerEnnemiesText = powerEnnemiesText;
        this.resourceEnnemiesText = resourceEnnemiesText;
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
            currentpowerEnnemiesText.text = "Puissance: " + FormatPower(power);
            currentresourceEnnemiesText.text = "Resource: " + resource.ToString();
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

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;

public class BaseButtonHandler : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private ResourceType resource;
    [SerializeField] private int resourceAmount;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI powerEnemiesText;
    [SerializeField] private TextMeshProUGUI resourceEnemiesText;
    [SerializeField] private Button fightButton;
    [SerializeField] private TextMeshProUGUI fightButtonText;
    [SerializeField] private Image fightButtonImage;

    private static GameObject currentInfoPanel;
    private static TextMeshProUGUI currentPowerEnemiesText;
    private static TextMeshProUGUI currentResourceEnemiesText;
    private static Button currentFightButton;
    private static TextMeshProUGUI currentFightButtonText;
    private static Image currentFightButtonImage;
    private static BaseButtonHandler currentBase;

    void Start()
    {
        if (currentInfoPanel == null)
        {
            currentInfoPanel = infoPanel;
            currentPowerEnemiesText = powerEnemiesText;
            currentResourceEnemiesText = resourceEnemiesText;
            currentFightButton = fightButton;
            currentFightButtonText = fightButtonText;
            currentFightButtonImage = fightButtonImage;
        }

        // Assigner la méthode OnClick à l'événement du bouton
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Initialize(int power, ResourceType resource, int resourceAmount, GameObject infoPanel, TextMeshProUGUI powerEnemiesText, TextMeshProUGUI resourceEnemiesText, Button fightButton, TextMeshProUGUI fightButtonText, Image fightButtonImage)
    {
        this.power = power;
        this.resource = resource;
        this.resourceAmount = resourceAmount;
        this.infoPanel = infoPanel;
        this.powerEnemiesText = powerEnemiesText;
        this.resourceEnemiesText = resourceEnemiesText;
        this.fightButton = fightButton;
        this.fightButtonText = fightButtonText;
        this.fightButtonImage = fightButtonImage;
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
            currentPowerEnemiesText.text = "Puissance: " + FormatPower(power);
            currentResourceEnemiesText.text = "Ressource: " + resource.ToString() + "\nQuantité: " + resourceAmount;
            currentBase = this;

            // Calculer le prix pour lancer le combat
            int fightCost = Mathf.RoundToInt(power * 0.75f);
            currentFightButtonText.text = fightCost.ToString();

            // Mettre à jour l'image de la ressource sur le bouton de combat
            currentFightButtonImage.sprite = GetResourceSprite(resource);

            currentFightButton.onClick.RemoveAllListeners();
            currentFightButton.onClick.AddListener(() => LaunchFight(fightCost));
        }
    }

    void LaunchFight(int fightCost)
    {
        // Logique pour lancer le combat
        Debug.Log("Combat lancé contre une base avec un coût de " + fightCost + " " + resource);
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

    Sprite GetResourceSprite(ResourceType resource)
    {
        return resource switch
        {
            ResourceType.Wood => Resources.Load<Sprite>("Icons/Wood Log"),
            ResourceType.Stone => Resources.Load<Sprite>("Icons/Obsidian"),
            ResourceType.Food => Resources.Load<Sprite>("Icons/Meat"),
            _ => null,
        };
    }
}

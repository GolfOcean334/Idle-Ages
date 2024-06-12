using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BaseButtonHandler : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private ResourceType resource;
    [SerializeField] private int resourceAmount;
    [SerializeField] private List<UnitsEnemy> unitsEnemies;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI powerEnemiesText;
    [SerializeField] private TextMeshProUGUI resourceEnemiesText;
    [SerializeField] private Button fightButton;
    [SerializeField] private TextMeshProUGUI fightButtonText;
    [SerializeField] private Image fightButtonImage;
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private TextMeshProUGUI unitsEnemyText;

    private static GameObject currentInfoPanel;
    private static TextMeshProUGUI currentPowerEnemiesText;
    private static TextMeshProUGUI currentResourceEnemiesText;
    private static Button currentFightButton;
    private static TextMeshProUGUI currentFightButtonText;
    private static Image currentFightButtonImage;
    private static BaseButtonHandler currentBase;
    private static TextMeshProUGUI currentUnitsEnemyText;

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
            currentUnitsEnemyText = unitsEnemyText;
        }

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Initialize(int power, ResourceType resource, int resourceAmount, GameObject infoPanel, TextMeshProUGUI powerEnemiesText, TextMeshProUGUI resourceEnemiesText, Button fightButton, TextMeshProUGUI fightButtonText, Image fightButtonImage, ResourcesManager resourcesManager, List<UnitsEnemy> unitsEnemies, TextMeshProUGUI unitsEnemyText)
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
        this.resourcesManager = resourcesManager;
        this.unitsEnemies = unitsEnemies;
        this.unitsEnemyText = unitsEnemyText;
    }

    void OnClick()
    {
        if (currentBase == this)
        {
            currentInfoPanel.SetActive(false);
            currentBase = null;
        }
        else
        {
            currentInfoPanel.SetActive(true);
            currentPowerEnemiesText.text = "Puissance: " + FormatPower(power);
            currentResourceEnemiesText.text = "Ressource: " + resource.ToString() + "\nQuantité: " + resourceAmount; 
            currentUnitsEnemyText.text = string.Join(", ", unitsEnemies);
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
        if (HasEnoughResources(fightCost))
        {
            // Logique pour lancer le combat et soustraire la ressource appropriée
            Debug.Log("Combat lancé contre une base avec un coût de " + fightCost + " " + resource);

            switch (resource)
            {
                case ResourceType.Wood:
                    resourcesManager.resource3 = Mathf.Max(resourcesManager.resource3 - fightCost, 0);
                    break;
                case ResourceType.Stone:
                    resourcesManager.resource2 = Mathf.Max(resourcesManager.resource2 - fightCost, 0);
                    break;
                case ResourceType.Food:
                    resourcesManager.resource1 = Mathf.Max(resourcesManager.resource1 - fightCost, 0);
                    break;
            }
            resourcesManager.UpdateResourceTexts();
        }
        else
        {
            Debug.Log("Pas assez de ressources pour lancer le combat.");
        }
    }

    bool HasEnoughResources(int fightCost)
    {
        return resource switch
        {
            ResourceType.Wood => resourcesManager.resource3 >= fightCost,
            ResourceType.Stone => resourcesManager.resource2 >= fightCost,
            ResourceType.Food => resourcesManager.resource1 >= fightCost,
            _ => false,
        };
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

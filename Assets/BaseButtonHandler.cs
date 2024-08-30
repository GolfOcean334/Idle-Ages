using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BaseButtonHandler : MonoBehaviour
{
    [SerializeField] private int power;
    [SerializeField] private ResourceType resource;
    [SerializeField] private int resourceAmount;
    [SerializeField] private int resourcesPerSecond;
    [SerializeField] private List<UnitsEnemy> unitsEnemies;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI powerEnemiesText;
    [SerializeField] private TextMeshProUGUI resourceEnemiesText;
    [SerializeField] private Button openFightPanelButton;
    [SerializeField] private Button fightButton;
    [SerializeField] private TextMeshProUGUI fightButtonText;
    [SerializeField] private Image fightButtonImage;
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private TextMeshProUGUI unitsEnemyText;
    [SerializeField] private TextMeshProUGUI resourcesPerSecondText;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private UnitsManager unitsManager;
    [SerializeField] private TextMeshProUGUI chanceOfVictoryText;

    [SerializeField] private GameObject battlePanel;
    [SerializeField] private Slider unitT1Slider;
    [SerializeField] private Slider unitT2Slider;
    [SerializeField] private Slider unitT3Slider;
    [SerializeField] private TextMeshProUGUI unitT1CountText;
    [SerializeField] private TextMeshProUGUI unitT2CountText;
    [SerializeField] private TextMeshProUGUI unitT3CountText;
    private bool isBattlePanelActive = false;

    private static GameObject currentInfoPanel; 
    private static TextMeshProUGUI currentPowerEnemiesText;
    private static TextMeshProUGUI currentResourceEnemiesText;
    private static Button currentOpenFightPanelButton;
    private static Button currentFightButton;
    private static TextMeshProUGUI currentFightButtonText;
    private static Image currentFightButtonImage;
    private static BaseButtonHandler currentBase;
    private static TextMeshProUGUI currentUnitsEnemyText;
    private static TextMeshProUGUI currentresourcesPerSecondText;
    private static TextMeshProUGUI currentChanceOfVictoryText;
    private CaptureBaseHandler captureBaseHandler;
    private static GameObject currentBattlePanel;

    void Start()
    {
        if (currentInfoPanel == null)
        {
            currentInfoPanel = infoPanel;
            currentPowerEnemiesText = powerEnemiesText;
            currentResourceEnemiesText = resourceEnemiesText;
            currentOpenFightPanelButton = openFightPanelButton;
            currentFightButton = fightButton;
            currentFightButtonText = fightButtonText;
            currentFightButtonImage = fightButtonImage;
            currentUnitsEnemyText = unitsEnemyText;
            currentresourcesPerSecondText = resourcesPerSecondText;
            currentChanceOfVictoryText = chanceOfVictoryText;
            currentBattlePanel = battlePanel;
        }

        battlePanel.SetActive(false);
        openFightPanelButton.onClick.AddListener(ToggleBattlePanel);

        GetComponent<Button>().onClick.AddListener(OnClick);

        captureBaseHandler = FindObjectOfType<CaptureBaseHandler>();
    }

    public void Initialize(int power, ResourceType resource, int resourceAmount, int resourcesPerSecond, GameObject infoPanel, TextMeshProUGUI powerEnemiesText, TextMeshProUGUI resourceEnemiesText, Button openFightPanelButton, Button fightButton, TextMeshProUGUI fightButtonText, Image fightButtonImage, ResourcesManager resourcesManager, List<UnitsEnemy> unitsEnemies, TextMeshProUGUI unitsEnemyText, TextMeshProUGUI resourcesPerSecondText, PlayerStats playerStats, TextMeshProUGUI chanceOfVictoryText, GameObject battlePanel, Slider unitT1Slider, Slider unitT2Slider, Slider unitT3Slider)
    {
        this.power = power;
        this.resource = resource;
        this.resourceAmount = resourceAmount;
        this.resourcesPerSecond = resourcesPerSecond;
        this.infoPanel = infoPanel;
        this.powerEnemiesText = powerEnemiesText;
        this.resourceEnemiesText = resourceEnemiesText;
        this.openFightPanelButton = openFightPanelButton;
        this.fightButton = fightButton;
        this.fightButtonText = fightButtonText;
        this.fightButtonImage = fightButtonImage;
        this.resourcesManager = resourcesManager;
        this.unitsEnemies = unitsEnemies;
        this.unitsEnemyText = unitsEnemyText;
        this.resourcesPerSecondText = resourcesPerSecondText;
        this.playerStats = playerStats;
        this.chanceOfVictoryText = chanceOfVictoryText;
        this.battlePanel = battlePanel;
        this.unitT1Slider = unitT1Slider;
        this.unitT2Slider = unitT2Slider;
        this.unitT3Slider = unitT3Slider;
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
            currentresourcesPerSecondText.text = "Ressource par seconde: " + resourcesPerSecond.ToString();
            currentBase = this;

            // Calculer le prix pour lancer le combat
            int fightCost = Mathf.RoundToInt(power * 0.75f);
            currentFightButtonText.text = fightCost.ToString();

            // Mettre à jour l'image de la ressource sur le bouton de combat
            currentFightButtonImage.sprite = GetResourceSprite(resource);

            currentOpenFightPanelButton.onClick.RemoveAllListeners();
            currentOpenFightPanelButton.onClick.AddListener(ToggleBattlePanel); // Ouvre le panel

            currentFightButton.onClick.RemoveAllListeners();
            currentFightButton.onClick.AddListener(() => LaunchFight(fightCost, CalculateChanceOfVictory(playerStats.CalculatePlayerPower(), power))); // Lance le combat
        }
    }
    void Update()
    {
        if (isBattlePanelActive)
        {
            UpdateChanceOfVictoryDisplay();
        }
    }

    void UpdateChanceOfVictoryDisplay()
    {
        // Récupérer les unités sélectionnées des sliders
        int selectedUnitsT1 = (int)unitT1Slider.value;
        int selectedUnitsT2 = (int)unitT2Slider.value;
        int selectedUnitsT3 = (int)unitT3Slider.value;

        // Calculer la puissance du joueur pour le combat avec les unités sélectionnées
        float playerPower = playerStats.CalculatePlayerPowerWithSelectedUnits(selectedUnitsT1, selectedUnitsT2, selectedUnitsT3);

        // Calculer la chance de victoire
        float chanceOfVictory = CalculateChanceOfVictory(playerPower, power);

        // Assurez-vous que le texte est bien affiché et mis à jour
        if (currentChanceOfVictoryText != null)
        {
            currentChanceOfVictoryText.gameObject.SetActive(true);
            currentChanceOfVictoryText.text = "Chance de victoire: " + (chanceOfVictory * 100).ToString("F1") + "%";
        }
        else
        {
            Debug.LogWarning("currentChanceOfVictoryText n'est pas assigné ou n'est pas trouvé.");
        }
    }



    void LaunchFight(int fightCost, float chanceOfVictory)
    {
        if (HasEnoughResources(fightCost))
        {
            Debug.Log("Combat lancé contre une base avec un coût de " + fightCost + " " + resource);

            // Récupérer les unités sélectionnées des sliders
            int selectedUnitsT1 = (int)unitT1Slider.value;
            int selectedUnitsT2 = (int)unitT2Slider.value;
            int selectedUnitsT3 = (int)unitT3Slider.value;

            // Retirer les unités sélectionnées pour le combat
            playerStats.RemoveUnits(selectedUnitsT1, selectedUnitsT2, selectedUnitsT3);
            UpdateSliders();

            if (Random.value <= chanceOfVictory)
            {
                captureBaseHandler.CaptureBase(gameObject, resource, resourceAmount, resourcesPerSecond);
                Debug.Log("Le joueur a réussi à capturer la base.");
            }
            else
            {
                Debug.Log("Le joueur n'a pas réussi à capturer la base.");
            }
        }
        else
        {
            Debug.Log("Pas assez de ressources pour lancer le combat.");
        }
    }



    void ToggleBattlePanel()
    {
        isBattlePanelActive = !isBattlePanelActive;
        currentBattlePanel.SetActive(isBattlePanelActive);

        if (isBattlePanelActive)
        {
            UpdateSliders();
        }
    }

    void UpdateSliders()
    {
        // Synchroniser les sliders avec le nombre d'unités disponibles
        unitT1Slider.maxValue = playerStats.UnitsT1;
        unitT2Slider.maxValue = playerStats.UnitsT2;
        unitT3Slider.maxValue = playerStats.UnitsT3;
    }

    public void HideInfoPanel()
    {
        currentInfoPanel.SetActive(true);
        currentBase = null;
    }

    bool HasEnoughResources(int fightCost)
    {
        return resource switch
        {
            ResourceType.Wood => playerStats.resource3 >= fightCost,
            ResourceType.Stone => playerStats.resource2 >= fightCost,
            ResourceType.Food => playerStats.resource1 >= fightCost,
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

    float CalculateChanceOfVictory(float playerPower, float basePower)
    {
        float difference = playerPower / basePower;
        float chanceOfVictory = 1 / (1 + Mathf.Exp(-10 *(difference - 1.025f)));

        return chanceOfVictory;
    }
}

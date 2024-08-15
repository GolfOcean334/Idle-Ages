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
    private static Button currentFightButton;
    private static TextMeshProUGUI currentFightButtonText;
    private static Image currentFightButtonImage;
    private static BaseButtonHandler currentBase;
    private static TextMeshProUGUI currentUnitsEnemyText;
    private static TextMeshProUGUI currentresourcesPerSecondText;
    private static TextMeshProUGUI currentChanceOfVictoryText;
    private CaptureBaseHandler captureBaseHandler;
    private static GameObject currentBattlePanel;
    private Slider currentUnitT1Slider;
    private Slider currentUnitT2Slider;
    private Slider currentUnitT3Slider;
    private TextMeshProUGUI currentUnitT1CountText;
    private TextMeshProUGUI currentUnitT2Counttext;
    private TextMeshProUGUI currentUnitT3Counttext;

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
            currentresourcesPerSecondText = resourcesPerSecondText;
            currentChanceOfVictoryText = chanceOfVictoryText;
            currentBattlePanel = battlePanel;
            currentUnitT1Slider = unitT1Slider;
            currentUnitT2Slider = unitT2Slider;
            currentUnitT3Slider = unitT3Slider;
            currentUnitT1CountText = unitT1CountText;
            currentUnitT2Counttext = unitT2CountText;
            currentUnitT3Counttext = unitT3CountText;
        }

        battlePanel.SetActive(false);
        fightButton.onClick.AddListener(ToggleBattlePanel);
        InitializeSliders();

        GetComponent<Button>().onClick.AddListener(OnClick);

        captureBaseHandler = FindObjectOfType<CaptureBaseHandler>();
    }

    public void Initialize(int power, ResourceType resource, int resourceAmount, int resourcesPerSecond, GameObject infoPanel, TextMeshProUGUI powerEnemiesText, TextMeshProUGUI resourceEnemiesText, Button fightButton, TextMeshProUGUI fightButtonText, Image fightButtonImage, ResourcesManager resourcesManager, List<UnitsEnemy> unitsEnemies, TextMeshProUGUI unitsEnemyText, TextMeshProUGUI resourcesPerSecondText, PlayerStats playerStats, TextMeshProUGUI chanceOfVictoryText, GameObject battlePanel, Slider unitT1Slider, Slider unitT2Slider, Slider unitT3Slider, TextMeshProUGUI unitT1CountText, TextMeshProUGUI unitT2CountText, TextMeshProUGUI unitT3CountText)
    {
        this.power = power;
        this.resource = resource;
        this.resourceAmount = resourceAmount;
        this.resourcesPerSecond = resourcesPerSecond;
        this.infoPanel = infoPanel;
        this.powerEnemiesText = powerEnemiesText;
        this.resourceEnemiesText = resourceEnemiesText;
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
        this.unitT1CountText = unitT1CountText;
        this.unitT2CountText = unitT2CountText;
        this.unitT3CountText = unitT3CountText;
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

            // Calculer la chance de victoire
            float chanceOfVictory = CalculateChanceOfVictory(playerStats.CalculatePlayerPower(), power);
            currentChanceOfVictoryText.text = "Chance de victoire: " + (chanceOfVictory * 100).ToString("F1") + "%";

            currentFightButton.onClick.RemoveAllListeners();
            currentFightButton.onClick.AddListener(ToggleBattlePanel);
            currentFightButton.onClick.AddListener(() => LaunchFight(fightCost, chanceOfVictory));
        }
    }

    void LaunchFight(int fightCost, float chanceOfVictory)
    {
        if (HasEnoughResources(fightCost))
        {
            Debug.Log("Combat lancé contre une base avec un coût de " + fightCost + " " + resource);

            // Sauvegarde les unités actuelles avant le combat
            playerStats.SaveAllUnits();

            // Obtenir le nombre de Units à envoyer du joueur selon les sliders
            int unitsT1ToSend = Mathf.RoundToInt(unitT1Slider.value);
            int unitsT2ToSend = Mathf.RoundToInt(unitT2Slider.value);
            int unitsT3ToSend = Mathf.RoundToInt(unitT3Slider.value);

            // Réduire les unités envoyées du pool du joueur
            playerStats.RemoveUnits(unitsT1ToSend, unitsT2ToSend, unitsT3ToSend);

            // Calculer la chance de victoire avec les unités sélectionnées
            float playerPower = playerStats.CalculatePlayerPowerWithSelectedUnits(unitsT1ToSend, unitsT2ToSend, unitsT3ToSend);
            float chanceOfVictory = CalculateChanceOfVictory(playerPower, power);

            if (Random.value <= chanceOfVictory)
            {
                captureBaseHandler.CaptureBase(gameObject, resourcesManager, resource, resourceAmount, resourcesPerSecond);
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

    void InitializeSliders()
    {
        unitT1Slider.onValueChanged.AddListener(delegate { UpdateUnitCountText("T1"); });
        unitT2Slider.onValueChanged.AddListener(delegate { UpdateUnitCountText("T2"); });
        unitT3Slider.onValueChanged.AddListener(delegate { UpdateUnitCountText("T3"); });
    }

    void UpdateSliders()
    {
        unitT1Slider.maxValue = playerStats.UnitsT1;
        unitT2Slider.maxValue = playerStats.UnitsT2;
        unitT3Slider.maxValue = playerStats.UnitsT3;

        unitT1Slider.value = 0;
        unitT2Slider.value = 0;
        unitT3Slider.value = 0;

        UpdateUnitCountText("T1");
        UpdateUnitCountText("T2");
        UpdateUnitCountText("T3");
    }

    public float CalculatePlayerPowerWithSelectedUnits(int unitsT1, int unitsT2, int unitsT3)
    {
        float totalPower = unitsT1 * PowerPerUnitT1 + unitsT2 * PowerPerUnitT2 + unitsT3 * PowerPerUnitT3;
        return totalPower;
    }

    public void RemoveUnits(int unitsT1, int unitsT2, int unitsT3)
    {
        playerStats.UnitsT1 -= unitsT1;
        playerStats.UnitsT2 -= unitsT2;
        playerStats.UnitsT3 -= unitsT3;

        // Assurez-vous que les valeurs ne deviennent pas négatives
        playerStats.UnitsT1 = Mathf.Max(playerStats.UnitsT1, 0);
        playerStats.UnitsT2 = Mathf.Max(playerStats.UnitsT2, 0);
        playerStats.UnitsT3 = Mathf.Max(playerStats.UnitsT3, 0);
    }


    void UpdateUnitCountText(string unitType)
    {
        int count = 0;
        if (unitType == "T1")
        {
            count = Mathf.RoundToInt(unitT1Slider.value);
            unitT1CountText.text = "Units T1: " + count;
        }
        else if (unitType == "T2")
        {
            count = Mathf.RoundToInt(unitT2Slider.value);
            unitT2CountText.text = "Units T2: " + count;
        }
        else if (unitType == "T3")
        {
            count = Mathf.RoundToInt(unitT3Slider.value);
            unitT3CountText.text = "Units T3: " + count;
        }
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

    float CalculateChanceOfVictory(float playerPower, float basePower)
    {
        float difference = playerPower / basePower;
        float chanceOfVictory = 1 / (1 + Mathf.Exp(-10 *(difference - 1.025f)));

        return chanceOfVictory;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class UnitsManager : MonoBehaviour
{
    [SerializeField] private RectTransform loadingBarT1;
    [SerializeField] private RectTransform loadingBarT2;
    [SerializeField] private RectTransform loadingBarT3;
    [SerializeField] private Button buttonT1;
    [SerializeField] private Button buttonT2;
    [SerializeField] private Button buttonT3;
    [SerializeField] private Button multiplicatorButton;
    [SerializeField] private TextMeshProUGUI UnitsT1Text;
    [SerializeField] private TextMeshProUGUI UnitsT2Text;
    [SerializeField] private TextMeshProUGUI UnitsT3Text;
    [SerializeField] private TextMeshProUGUI buttonT1Text;
    [SerializeField] private TextMeshProUGUI buttonT2Text;
    [SerializeField] private TextMeshProUGUI buttonT3Text;
    [SerializeField] private TextMeshProUGUI UnitsT1CooldownText;
    [SerializeField] private TextMeshProUGUI UnitsT2CooldownText;
    [SerializeField] private TextMeshProUGUI UnitsT3CooldownText;
    [SerializeField] private PlayerStats playerStats;

    private readonly int[] multiplicators = { 1, 5, 10, 50 };
    private int currentMultiplicatorIndex = 0;

    public static UnitsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerStats.Load();
        playerStats.Initialize();

        buttonT1.onClick.AddListener(() => OnStartButtonClick("T1"));
        buttonT2.onClick.AddListener(() => OnStartButtonClick("T2"));
        buttonT3.onClick.AddListener(() => OnStartButtonClick("T3"));
        multiplicatorButton.onClick.AddListener(ChangeMultiplicator);

        UpdateUnitsT1Text();
        UpdateUnitsT2Text();
        UpdateUnitsT3Text();

        multiplicatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "x" + multiplicators[currentMultiplicatorIndex];

        UpdateButtonCosts();
    }

    private void Update()
    {
        UpdateUnitsT1Text();
        UpdateUnitsT2Text();
        UpdateUnitsT3Text();
    }

    void ChangeMultiplicator()
    {
        currentMultiplicatorIndex = (currentMultiplicatorIndex + 1) % multiplicators.Length;
        multiplicatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "x" + multiplicators[currentMultiplicatorIndex];
        UpdateButtonCosts();
    }

    void UpdateButtonCosts()
    {
        int resourceCost = 2 * multiplicators[currentMultiplicatorIndex];
        buttonT1Text.text = "Buy T1\nCost: " + resourceCost + " Res1";
        buttonT2Text.text = "Buy T2\nCost: " + resourceCost + " Res2";
        buttonT3Text.text = "Buy T3\nCost: " + resourceCost + " Res3";
    }

    void OnStartButtonClick(string unitType)
    {
        int multiplier = multiplicators[currentMultiplicatorIndex];
        int resourceCost = 2 * multiplicators[currentMultiplicatorIndex];

        if (unitType == "T1" && playerStats.resource1 >= resourceCost)
        {
            playerStats.resource1 -= resourceCost;
            playerStats.EnqueueUnits("T1", multiplier);
        }
        else if (unitType == "T2" && playerStats.resource2 >= resourceCost)
        {
            playerStats.resource2 -= resourceCost;
            playerStats.EnqueueUnits("T2", multiplier);
        }
        else if (unitType == "T3" && playerStats.resource3 >= resourceCost)
        {
            playerStats.resource3 -= resourceCost;
            playerStats.EnqueueUnits("T3", multiplier);
        }
        playerStats.Save();
    }

    void UpdateUnitsT1Text()
    {
        UnitsT1Text.text = "" + playerStats.UnitsT1;
        Debug.Log("Troupe généré: \n Troupe1 : " + playerStats.UnitsT1);
    }

    void UpdateUnitsT2Text()
    {
        UnitsT2Text.text = "" + playerStats.UnitsT2;
    }

    void UpdateUnitsT3Text()
    {
        UnitsT3Text.text = "" + playerStats.UnitsT3;
    }

    public void UpdateLoadingBar(string unitType, float progress)
    {
        RectTransform loadingBar = null;
        TextMeshProUGUI cooldownText = null;
        float totalTimeInSeconds = 0f;

        switch (unitType)
        {
            case "T1":
                loadingBar = loadingBarT1;
                cooldownText = UnitsT1CooldownText;
                totalTimeInSeconds = Mathf.CeilToInt((1f - progress) * playerStats.productionInterval + playerStats.productionInterval * (playerStats.unitT1Queue.Count - 1));
                break;
            case "T2":
                loadingBar = loadingBarT2;
                cooldownText = UnitsT2CooldownText;
                totalTimeInSeconds = Mathf.CeilToInt((1f - progress) * playerStats.productionInterval + playerStats.productionInterval * (playerStats.unitT2Queue.Count - 1));
                break;
            case "T3":
                loadingBar = loadingBarT3;
                cooldownText = UnitsT3CooldownText;
                totalTimeInSeconds = Mathf.CeilToInt((1f - progress) * playerStats.productionInterval + playerStats.productionInterval * (playerStats.unitT3Queue.Count - 1));
                break;
        }

        if (cooldownText != null)
        {
            // Formater le temps restant
            string formattedTime = FormatTime(totalTimeInSeconds);
            if (totalTimeInSeconds > 0)
            {
                cooldownText.text = formattedTime;
            }
            else
            {
                cooldownText.text = string.Empty;
            }
            
        }

        if (loadingBar != null)
        {
            loadingBar.localScale = new Vector3(progress, 1f, 1f);
        }
    }

    // Fonction de formatage du temps
    private string FormatTime(float seconds)
    {
        if (seconds >= 3600)
        {
            int hours = Mathf.FloorToInt(seconds / 3600);
            int minutes = Mathf.FloorToInt((seconds % 3600) / 60);
            int secs = Mathf.FloorToInt(seconds % 60);
            return $"{hours}h {minutes}m {secs}s";
        }
        else if (seconds >= 60)
        {
            int minutes = Mathf.FloorToInt(seconds / 60);
            int secs = Mathf.FloorToInt(seconds % 60);
            return $"{minutes}m {secs}s";
        }
        else
        {
            return $"{Mathf.FloorToInt(seconds)}s";
        }
    }


}

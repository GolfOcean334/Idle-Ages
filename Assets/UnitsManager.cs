using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

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
    [SerializeField] private ResourcesManager resourcesManager;

    private int UnitsT1 = 0;
    private int UnitsT2 = 0;
    private int UnitsT3 = 0;
    private bool isLoadingT1 = false;
    private bool isLoadingT2 = false;
    private bool isLoadingT3 = false;
    private readonly float loadingTime = 5f;
    private readonly float maxBarWidth = 300f;

    private readonly Queue<string> unitT1Queue = new();
    private readonly Queue<string> unitT2Queue = new();
    private readonly Queue<string> unitT3Queue = new();

    private readonly int[] multiplicators = { 1, 5, 10, 50 };
    private int currentMultiplicatorIndex = 0;

    void Start()
    {
        buttonT1.onClick.AddListener(() => OnStartButtonClick("T1"));
        buttonT2.onClick.AddListener(() => OnStartButtonClick("T2"));
        buttonT3.onClick.AddListener(() => OnStartButtonClick("T3"));
        multiplicatorButton.onClick.AddListener(ChangeMultiplicator);

        UpdateUnitsT1Text();
        UpdateUnitsT2Text();
        UpdateUnitsT3Text();

        multiplicatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "x" + multiplicators[currentMultiplicatorIndex];

        UpdateButtonCosts();
        UnitsT1CooldownText.text = "";
        UnitsT2CooldownText.text = "";
        UnitsT3CooldownText.text = "";
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

        if (unitType == "T1" && resourcesManager.resource1 >= resourceCost)
        {
            resourcesManager.resource1 -= resourceCost;
            for (int i = 0; i < multiplier; i++)
            {
                unitT1Queue.Enqueue(unitType);
                if (!isLoadingT1)
                {
                    StartCoroutine(ProcessQueue("T1"));
                }
            }
        }
        else if (unitType == "T2" && resourcesManager.resource2 >= resourceCost)
        {
            resourcesManager.resource2 -= resourceCost;
            for (int i = 0; i < multiplier; i++)
            {
                unitT2Queue.Enqueue(unitType);
                if (!isLoadingT2)
                {
                    StartCoroutine(ProcessQueue("T2"));
                }
            }
        }
        else if (unitType == "T3" && resourcesManager.resource3 >= resourceCost)
        {
            resourcesManager.resource3 -= resourceCost;
            for (int i = 0; i < multiplier; i++)
            {
                unitT3Queue.Enqueue(unitType);
                if (!isLoadingT3)
                {
                    StartCoroutine(ProcessQueue("T3"));
                }
            }
        }

        UpdateCooldownTexts();
    }

    IEnumerator ProcessQueue(string unitType)
    {
        if (unitType == "T1")
        {
            while (unitT1Queue.Count > 0)
            {
                isLoadingT1 = true;
                yield return StartCoroutine(LoadOverTime(loadingBarT1, unitType));
                unitT1Queue.Dequeue();
                IncreaseUnitsT1();
                UpdateCooldownTexts();
            }
            isLoadingT1 = false;
        }
        else if (unitType == "T2")
        {
            while (unitT2Queue.Count > 0)
            {
                isLoadingT2 = true;
                yield return StartCoroutine(LoadOverTime(loadingBarT2, unitType));
                unitT2Queue.Dequeue();
                IncreaseUnitsT2();
                UpdateCooldownTexts();
            }
            isLoadingT2 = false;
        }
        else if (unitType == "T3")
        {
            while (unitT3Queue.Count > 0)
            {
                isLoadingT3 = true;
                yield return StartCoroutine(LoadOverTime(loadingBarT3, unitType));
                unitT3Queue.Dequeue();
                IncreaseUnitsT3();
                UpdateCooldownTexts();
            }
            isLoadingT3 = false;
        }
    }

    IEnumerator LoadOverTime(RectTransform loadingBar, string unitType)
    {
        float elapsedTime = 0f;
        float initialBarWidth = 0f;

        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;

            float newWidth = Mathf.Lerp(initialBarWidth, maxBarWidth, elapsedTime / loadingTime);
            loadingBar.sizeDelta = new Vector2(newWidth, loadingBar.sizeDelta.y);

            UpdateCooldownText(unitType, loadingTime - elapsedTime);

            yield return null;
        }

        loadingBar.sizeDelta = new Vector2(maxBarWidth, loadingBar.sizeDelta.y);
    }

    void UpdateCooldownText(string unitType, float currentLoadingTime)
    {
        if (unitType == "T1")
        {
            UnitsT1CooldownText.text = CalculateTotalCooldown(unitT1Queue.Count, currentLoadingTime);
        }
        else if (unitType == "T2")
        {
            UnitsT2CooldownText.text = CalculateTotalCooldown(unitT2Queue.Count, currentLoadingTime);
        }
        else if (unitType == "T3")
        {
            UnitsT3CooldownText.text = CalculateTotalCooldown(unitT3Queue.Count, currentLoadingTime);
        }
    }

    void UpdateCooldownTexts()
    {
        UnitsT1CooldownText.text = CalculateTotalCooldown(unitT1Queue.Count, isLoadingT1 ? loadingTime : 0);
        UnitsT2CooldownText.text = CalculateTotalCooldown(unitT2Queue.Count, isLoadingT2 ? loadingTime : 0);
        UnitsT3CooldownText.text = CalculateTotalCooldown(unitT3Queue.Count, isLoadingT3 ? loadingTime : 0);
    }

    string CalculateTotalCooldown(int queueCount, float currentLoadingTime)
    {
        float totalCooldown = queueCount * loadingTime;
        if (queueCount > 0)
        {
            totalCooldown -= (loadingTime - currentLoadingTime);
        }
        else 
        { 
            totalCooldown = 0;
        }
        return totalCooldown > 0 ? "Temps formation: " + totalCooldown.ToString("F2") + "s" : "";
    }

    void IncreaseUnitsT1()
    {
        UnitsT1 += 1;
        UpdateUnitsT1Text();
        ResetLoadingBar(loadingBarT1);
    }

    void UpdateUnitsT1Text()
    {
        UnitsT1Text.text = "UnitsT1: " + UnitsT1;
    }

    void IncreaseUnitsT2()
    {
        UnitsT2 += 1;
        UpdateUnitsT2Text();
        ResetLoadingBar(loadingBarT2);
    }

    void UpdateUnitsT2Text()
    {
        UnitsT2Text.text = "UnitsT2: " + UnitsT2;
    }

    void IncreaseUnitsT3()
    {
        UnitsT3 += 1;
        UpdateUnitsT3Text();
        ResetLoadingBar(loadingBarT3);
    }

    void UpdateUnitsT3Text()
    {
        UnitsT3Text.text = "UnitsT3: " + UnitsT3;
    }

    void ResetLoadingBar(RectTransform loadingBar)
    {
        loadingBar.sizeDelta = new Vector2(0f, loadingBar.sizeDelta.y);
    }
}

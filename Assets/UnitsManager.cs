using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Resources;

public class UnitsManager : MonoBehaviour
{
    // Références aux éléments UI pour les barres de chargement et les boutons
    [SerializeField] private RectTransform loadingBarT1;
    [SerializeField] private RectTransform loadingBarT2;
    [SerializeField] private RectTransform loadingBarT3;
    [SerializeField] private Button buttonT1;
    [SerializeField] private Button buttonT2;
    [SerializeField] private Button buttonT3;
    [SerializeField] private Button multiplicatorButton; // Bouton pour le multiplicateur
    [SerializeField] private TextMeshProUGUI UnitsT1Text;
    [SerializeField] private TextMeshProUGUI UnitsT2Text;
    [SerializeField] private TextMeshProUGUI UnitsT3Text;

    // Références aux textes des boutons pour afficher le coût en ressources
    [SerializeField] private TextMeshProUGUI buttonT1Text;
    [SerializeField] private TextMeshProUGUI buttonT2Text;
    [SerializeField] private TextMeshProUGUI buttonT3Text;

    // Référence aux textes utilisés pour afficher le cooldown de fin de formation de chaque unité
    [SerializeField] private TextMeshProUGUI UnitsT1CooldownText;
    [SerializeField] private TextMeshProUGUI UnitsT2CooldownText;
    [SerializeField] private TextMeshProUGUI UnitsT3CooldownText;

    // Référence au ResourcesManager
    [SerializeField] private ResourcesManager resourcesManager;

    // Compteurs pour les unités
    private int UnitsT1 = 0;
    private int UnitsT2 = 0;
    private int UnitsT3 = 0;

    // Indicateurs pour savoir si une barre de chargement est en cours
    private bool isLoadingT1 = false;
    private bool isLoadingT2 = false;
    private bool isLoadingT3 = false;

    // Constantes pour le temps de chargement et la largeur maximale de la barre
    private readonly float loadingTime = 5f;
    private readonly float maxBarWidth = 300f;

    // Files d'attente pour gérer les améliorations en attente
    private readonly Queue<string> unitT1Queue = new();
    private readonly Queue<string> unitT2Queue = new();
    private readonly Queue<string> unitT3Queue = new();

    // Variable pour le multiplicateur
    private readonly int[] multiplicators = { 1, 5, 10, 50 };
    private int currentMultiplicatorIndex = 0;

    void Start()
    {
        // Ajouter des listeners pour les boutons
        buttonT1.onClick.AddListener(() => OnStartButtonClick("T1"));
        buttonT2.onClick.AddListener(() => OnStartButtonClick("T2"));
        buttonT3.onClick.AddListener(() => OnStartButtonClick("T3"));
        multiplicatorButton.onClick.AddListener(ChangeMultiplicator); // Ajouter un listener pour le bouton multiplicateur

        // Initialiser l'affichage des unités
        UpdateUnitsT1Text();
        UpdateUnitsT2Text();
        UpdateUnitsT3Text();

        // Initialiser le texte du bouton multiplicateur
        multiplicatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "x" + multiplicators[currentMultiplicatorIndex];

        // Initialiser les textes des boutons d'achat avec le coût
        UpdateButtonCosts();

        // Initialiser les textes des cooldowns à 0
        UnitsT1CooldownText.text = "";
        UnitsT2CooldownText.text = "";
        UnitsT3CooldownText.text = "";
    }

    // Méthode pour changer le multiplicateur
    void ChangeMultiplicator()
    {
        currentMultiplicatorIndex = (currentMultiplicatorIndex + 1) % multiplicators.Length;
        multiplicatorButton.GetComponentInChildren<TextMeshProUGUI>().text = "x" + multiplicators[currentMultiplicatorIndex];
        UpdateButtonCosts(); // Mettre à jour les coûts sur les boutons
    }

    // Méthode pour mettre à jour les coûts affichés sur les boutons d'achat
    void UpdateButtonCosts()
    {
        int resourceCost = 2 * multiplicators[currentMultiplicatorIndex];
        buttonT1Text.text = "Buy T1\nCost: " + resourceCost + " Res1";
        buttonT2Text.text = "Buy T2\nCost: " + resourceCost + " Res2";
        buttonT3Text.text = "Buy T3\nCost: " + resourceCost + " Res3";
    }

    // Méthode appelée lorsque l'un des boutons est cliqué
    void OnStartButtonClick(string unitType)
    {
        int multiplier = multiplicators[currentMultiplicatorIndex];
        int resourceCost = 2 * multiplicators[currentMultiplicatorIndex];

        if (unitType == "T1" && resourcesManager.resource1 >= resourceCost)
        {
            resourcesManager.resource1 -= resourceCost;
            for (int i = 0; i < multiplier; i++)
            {
                unitT1Queue.Enqueue(unitType); // Ajouter une amélioration à la file d'attente pour T1
                // Démarrer la coroutine si aucune amélioration n'est en cours
                if (!isLoadingT1)
                {
                    StartCoroutine(ProcessQueue(unitType));
                }
            }
        }
        else if (unitType == "T2" && resourcesManager.resource2 >= resourceCost)
        {
            resourcesManager.resource2 -= resourceCost;
            for (int i = 0; i < multiplier; i++)
            {
                unitT2Queue.Enqueue(unitType); // Ajouter une amélioration à la file d'attente pour T2
                // Démarrer la coroutine si aucune amélioration n'est en cours
                if (!isLoadingT2)
                {
                    StartCoroutine(ProcessQueue(unitType));
                }
            }
        }
        else if (unitType == "T3" && resourcesManager.resource3 >= resourceCost)
        {
            resourcesManager.resource3 -= resourceCost;
            for (int i = 0; i < multiplier; i++)
            {
                unitT3Queue.Enqueue(unitType); // Ajouter une amélioration à la file d'attente pour T3
                // Démarrer la coroutine si aucune amélioration n'est en cours
                if (!isLoadingT3)
                {
                    StartCoroutine(ProcessQueue(unitType));
                }
            }
        }

        UpdateCooldownTexts(); // Mettre à jour les textes des cooldowns
    }

    // Coroutine pour traiter la file d'attente des améliorations
    IEnumerator ProcessQueue(string unitType)
    {
        if (unitType == "T1")
        {
            while (unitT1Queue.Count > 0)
            {
                isLoadingT1 = true;
                unitT1Queue.Dequeue(); // Retirer une amélioration de la file d'attente
                yield return StartCoroutine(LoadOverTime(loadingBarT1, UnitsT1CooldownText)); // Effectuer l'animation de la barre de chargement
                IncreaseUnitsT1();
            }
            isLoadingT1 = false;
        }
        else if (unitType == "T2")
        {
            while (unitT2Queue.Count > 0)
            {
                isLoadingT2 = true;
                unitT2Queue.Dequeue(); // Retirer une amélioration de la file d'attente
                yield return StartCoroutine(LoadOverTime(loadingBarT2, UnitsT2CooldownText)); // Effectuer l'animation de la barre de chargement
                IncreaseUnitsT2();
            }
            isLoadingT2 = false;
        }
        else if (unitType == "T3")
        {
            while (unitT3Queue.Count > 0)
            {
                isLoadingT3 = true;
                unitT3Queue.Dequeue(); // Retirer une amélioration de la file d'attente
                yield return StartCoroutine(LoadOverTime(loadingBarT3, UnitsT3CooldownText)); // Effectuer l'animation de la barre de chargement
                IncreaseUnitsT3();
            }
            isLoadingT3 = false;
        }

        UpdateCooldownTexts(); // Mettre à jour les textes des cooldowns après chaque amélioration
    }

    // Coroutine pour animer la barre de chargement et mettre à jour le cooldown
    IEnumerator LoadOverTime(RectTransform loadingBar, TextMeshProUGUI cooldownText)
    {
        float elapsedTime = 0f;
        float initialBarWidth = 0f;

        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;

            // Calculer la nouvelle largeur de la barre
            float newWidth = Mathf.Lerp(initialBarWidth, maxBarWidth, elapsedTime / loadingTime);
            loadingBar.sizeDelta = new Vector2(newWidth, loadingBar.sizeDelta.y);

            // Mettre à jour le texte du cooldown
            cooldownText.text = "Cooldown: " + (loadingTime - elapsedTime).ToString("F2") + "s";
            yield return null; // Attendre la frame suivante
        }

        // Assurer que la barre atteint la largeur maximale
        loadingBar.sizeDelta = new Vector2(maxBarWidth, loadingBar.sizeDelta.y);

        // Réinitialiser le texte du cooldown
        cooldownText.text = "";
    }

    // Mettre à jour les textes des cooldowns pour refléter le temps total restant pour chaque type d'unité
    void UpdateCooldownTexts()
    {
        UnitsT1CooldownText.text = CalculateTotalCooldown(unitT1Queue.Count, isLoadingT1);
        UnitsT2CooldownText.text = CalculateTotalCooldown(unitT2Queue.Count, isLoadingT2);
        UnitsT3CooldownText.text = CalculateTotalCooldown(unitT3Queue.Count, isLoadingT3);
    }

    // Calculer le temps total restant en fonction de la file d'attente et de l'état de chargement
    string CalculateTotalCooldown(int queueCount, bool isLoading)
    {
        float totalCooldown = queueCount * loadingTime;
        if (isLoading)
        {
            totalCooldown += loadingTime; // Ajouter le temps restant pour l'unité en cours de chargement
        }
        return totalCooldown > 0 ? "Total Cooldown: " + totalCooldown.ToString("F2") + "s" : "";
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

    // Réinitialiser la largeur de la barre de chargement
    void ResetLoadingBar(RectTransform loadingBar)
    {
        loadingBar.sizeDelta = new Vector2(0f, loadingBar.sizeDelta.y);
    }
}

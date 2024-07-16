using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class EnemyBaseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerBase;
    [SerializeField] private GameObject redEnemyBasePrefab;
    [SerializeField] private GameObject whiteEnemyBasePrefab;
    [SerializeField] private GameObject allBasesParent;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI powerEnnemiesText;
    [SerializeField] private TextMeshProUGUI resourceEnemiesText;
    [SerializeField] private TextMeshProUGUI unitsEnemyText;
    [SerializeField] private Button fightButton;
    [SerializeField] private TextMeshProUGUI fightButtonText;
    [SerializeField] private Image fightButtonImage;
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private TextMeshProUGUI resourcesPerSecondText;
    [SerializeField] private int numberOfRedBases = 100;
    [SerializeField] private int numberOfWhiteBases = 25;
    [SerializeField] private float minDistanceBetweenBases = 200f;
    [SerializeField] private float minXDistance = 500f;
    [SerializeField] private float maxXDistance = 10000f;
    [SerializeField] private float minYDistance = 500f;
    [SerializeField] private float maxYDistance = 10000f;

    private readonly List<Vector3> basePositions = new();
    private readonly List<ResourceType> resourcePool = new();
    private readonly Dictionary<ResourceType, float> totalResources = new()
    {
        { ResourceType.Wood, 20000f },
        { ResourceType.Stone, 15000f },
        { ResourceType.Food, 25000f }
    };
    private Dictionary<ResourceType, float> remainingResources;

    void Start()
    {
        infoPanel.SetActive(false);
        remainingResources = new Dictionary<ResourceType, float>(totalResources);

        // Pr�parer la liste des ressources
        PrepareResourcePool();

        SpawnEnemyBases(numberOfRedBases, redEnemyBasePrefab, false);
        SpawnEnemyBases(numberOfWhiteBases, whiteEnemyBasePrefab, true);
    }

    void PrepareResourcePool()
    {
        int totalBases = numberOfRedBases + numberOfWhiteBases;
        int resourcesPerType = totalBases / 3;

        for (int i = 0; i < resourcesPerType; i++)
        {
            resourcePool.Add(ResourceType.Wood);
            resourcePool.Add(ResourceType.Stone);
            resourcePool.Add(ResourceType.Food);
        }

        // Si le total des bases n'est pas divisible par 3, ajouter des ressources suppl�mentaires
        while (resourcePool.Count < totalBases)
        {
            resourcePool.Add((ResourceType)Random.Range(0, 3));
        }

        // M�langer la liste pour une distribution al�atoire
        Shuffle(resourcePool);
    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    void SpawnEnemyBases(int numberOfBases, GameObject basePrefab, bool isWhite)
    {
        int spawnedBases = 0;
        int maxAttempts = 50000; // augmenter le nombre de tentatives
        int attempts = 0;

        while (spawnedBases < numberOfBases && attempts < maxAttempts)
        {
            attempts++;
            Vector3 randomPosition = GenerateRandomPosition();

            if (IsPositionValid(randomPosition))
            {
                GameObject newBase = Instantiate(basePrefab, randomPosition, Quaternion.identity, allBasesParent.transform);
                int power = CalculatePower(randomPosition, isWhite);
                ResourceType resource = resourcePool[0];
                resourcePool.RemoveAt(0);
                int resourceAmount = CalculateResourceAmount(resource, randomPosition, isWhite);
                int resourcesPerSecond = Mathf.CeilToInt(power / 100.0f); // Calcul de la quantit� de ressource par seconde
                BaseButtonHandler baseButtonHandler = newBase.AddComponent<BaseButtonHandler>();
                List<UnitsEnemy> baseUnitsEnemies = GenerateRandomUnitsEnemy(isWhite);

                baseButtonHandler.Initialize(power, resource, resourceAmount, resourcesPerSecond, infoPanel, powerEnnemiesText, resourceEnemiesText, fightButton, fightButtonText, fightButtonImage, resourcesManager, baseUnitsEnemies, unitsEnemyText, resourcesPerSecondText);
                basePositions.Add(randomPosition);
                spawnedBases++;
            }
        }

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Nombre maximum de tentatives atteint. Certaines bases peuvent ne pas avoir �t� cr��es.");
        }
    }


    Vector3 GenerateRandomPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomXDistance = Random.Range(minXDistance, maxXDistance);
        float randomYDistance = Random.Range(minYDistance, maxYDistance);
        Vector3 randomPosition = playerBase.transform.position + new Vector3(randomDirection.x * randomXDistance, randomDirection.y * randomYDistance, 0);
        return randomPosition;
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 basePosition in basePositions)
        {
            if (Vector3.Distance(position, basePosition) < minDistanceBetweenBases)
            {
                return false;
            }
        }
        return true;
    }

    int CalculatePower(Vector3 position, bool isWhite)
    {
        float distance = Vector3.Distance(new Vector3(position.x, position.y, 0), new Vector3(playerBase.transform.position.x, playerBase.transform.position.y, 0));
        int power = Mathf.RoundToInt(500 * Mathf.Exp((distance - 500) / 2000f)); // Calcule de la puissance de mani�re exponentielle
        if (isWhite)
        {
            power *= 2;
        }
        return power;
    }

    int CalculateResourceAmount(ResourceType resource, Vector3 position, bool isWhite)
    {
        float distance = Vector3.Distance(new Vector3(position.x, position.y, 0), new Vector3(playerBase.transform.position.x, playerBase.transform.position.y, 0));
        int resourceAmount = Mathf.RoundToInt(500 * Mathf.Exp((distance - 500) / 2500f)); // Calcule des ressources de mani�re exponentielles
        remainingResources[resource] -= resourceAmount;
        if (isWhite)
        {
            resourceAmount *= 2;
        }
        return resourceAmount;
    }

    List<UnitsEnemy> GenerateRandomUnitsEnemy(bool isWhite)
    {
        List<UnitsEnemy> units = new();
        UnitsEnemy[] unitsEnemies = (UnitsEnemy[])System.Enum.GetValues(typeof(UnitsEnemy));

        float threshold = isWhite ? 0.4f : 0.8f;

        if (Random.value <= threshold)
        {
            units.Add(unitsEnemies[Random.Range(0, unitsEnemies.Length)]);
        }
        else
        {
            while (units.Count < 2)
            {
                UnitsEnemy randomUnit = unitsEnemies[Random.Range(0, unitsEnemies.Length)];
                if (!units.Contains(randomUnit))
                {
                    units.Add(randomUnit);
                }
            }
        }

        return units;
    }
}

public enum ResourceType
{
    Wood,
    Stone,
    Food
}

public enum UnitsEnemy
{
    Unit1,
    Unit2,
    Unit3
}

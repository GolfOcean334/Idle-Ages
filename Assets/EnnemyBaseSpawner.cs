using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class EnemyBaseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerBase;
    [SerializeField] private GameObject redEnemyBasePrefab;
    [SerializeField] private GameObject whiteEnemyBasePrefab;
    [SerializeField] private GameObject allBasesParent;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI powerEnnemiesText;
    [SerializeField] private TextMeshProUGUI resourceEnemiesText;
    [SerializeField] private int numberOfRedBases = 10;
    [SerializeField] private int numberOfWhiteBases = 5;
    [SerializeField] private float minDistanceBetweenBases = 250f;
    [SerializeField] private float minXDistance = 500f;
    [SerializeField] private float maxXDistance = 2000f;
    [SerializeField] private float minYDistance = 500f;
    [SerializeField] private float maxYDistance = 2000f;

    private readonly List<Vector3> basePositions = new();
    private readonly List<ResourceType> resourcePool = new();

    void Start()
    {
        infoPanel.SetActive(false);

        // Préparer la liste des ressources
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

        // Si le total des bases n'est pas divisible par 3, ajouter des ressources supplémentaires
        while (resourcePool.Count < totalBases)
        {
            resourcePool.Add((ResourceType)Random.Range(0, 3));
        }

        // Mélanger la liste pour une distribution aléatoire
        Shuffle(resourcePool);
    }

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void SpawnEnemyBases(int numberOfBases, GameObject basePrefab, bool isWhite)
    {
        int spawnedBases = 0;
        int maxAttempts = 10000; // pour éviter les boucles infinies
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
                BaseButtonHandler baseButtonHandler = newBase.AddComponent<BaseButtonHandler>();
                baseButtonHandler.Initialize(power, resource, infoPanel, powerEnnemiesText, resourceEnemiesText);
                basePositions.Add(randomPosition);
                spawnedBases++;
            }
        }

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Nombre maximum de tentatives atteint. Certaines bases peuvent ne pas avoir été créées.");
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
        // Calculer la distance euclidienne en 2D entre la base du joueur et la base ennemie
        float distance = Vector3.Distance(new Vector3(position.x, position.y, 0), new Vector3(playerBase.transform.position.x, playerBase.transform.position.y, 0));
        int power = Mathf.RoundToInt(distance);
        if (isWhite)
        {
            power *= 2;
        }
        return power;
    }
}

public enum ResourceType
{
    Wood,
    Stone,
    Food
}

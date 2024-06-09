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
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private int numberOfRedBases = 10;
    [SerializeField] private int numberOfWhiteBases = 5;
    [SerializeField] private float minDistanceBetweenBases = 250f;
    [SerializeField] private float minXDistance = 500f;
    [SerializeField] private float maxXDistance = 2000f;
    [SerializeField] private float minYDistance = 500f;
    [SerializeField] private float maxYDistance = 2000f;

    private readonly List<Vector3> basePositions = new();

    void Start()
    {
        infoPanel.SetActive(false);

        SpawnEnemyBases(numberOfRedBases, redEnemyBasePrefab, false);
        SpawnEnemyBases(numberOfWhiteBases, whiteEnemyBasePrefab, true);
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
                BaseButtonHandler baseButtonHandler = newBase.AddComponent<BaseButtonHandler>();
                baseButtonHandler.Initialize(power, infoPanel, infoText);
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
        int power = Mathf.RoundToInt(distance) * 2;
        if (isWhite)
            power *= 2;
        return power;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class EnemyBaseSpawner : MonoBehaviour
{
    public GameObject playerBase;
    public GameObject redEnemyBasePrefab;
    public GameObject whiteEnemyBasePrefab;
    public GameObject allBasesParent; // Référence au GameObject qui gère toutes les bases
    public int numberOfRedBases = 10;
    public int numberOfWhiteBases = 5;
    public float minDistanceBetweenBases = 250f;
    public float minXDistance = 500f;
    public float maxXDistance = 2000f;
    public float minYDistance = 500f;
    public float maxYDistance = 2000f;

    private readonly List<Vector3> basePositions = new();

    void Start()
    {
        SpawnEnemyBases(numberOfRedBases, redEnemyBasePrefab);
        SpawnEnemyBases(numberOfWhiteBases, whiteEnemyBasePrefab);
    }

    void SpawnEnemyBases(int numberOfBases, GameObject basePrefab)
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
}

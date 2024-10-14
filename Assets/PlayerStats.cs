using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public static PlayerStats playerStats;
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, "playerSave.json");

    public int UnitsT1;
    public int UnitsT2;
    public int UnitsT3;


    public int resource1 = 500;
    public int resource2 = 500;
    public int resource3 = 500;
    public int resource4;

    public int resources1PerSecond = 0;
    public int resources2PerSecond = 0;
    public int resources3PerSecond = 0;
    public int resources4PerSecond = 0;

    private readonly float MainMultiplicator = 1f;
    private readonly float SecondaryMultiplicator = 0.5f;

    private readonly float PvUnit1 = 70f;
    private readonly float DefUnit1 = 7f;
    private readonly float AttUnit1 = 30f;

    private readonly float PvUnit2 = 150f;
    private readonly float DefUnit2 = 25f;
    private readonly float AttUnit2 = 15f;

    private readonly float PvUnit3 = 100f;
    private readonly float DefUnit3 = 15f;
    private readonly float AttUnit3 = 20f;

    public float productionInterval = 5f;
    private Coroutine productionCoroutineT1;
    private Coroutine productionCoroutineT2;
    private Coroutine productionCoroutineT3;

    public readonly Queue<string> unitT1Queue = new();
    public readonly Queue<string> unitT2Queue = new();
    public readonly Queue<string> unitT3Queue = new();

    public List<Item> EquippedItems = new List<Item>();

    public void EquipItem(Item item)
    {
        if (EquippedItems.Contains(item))
        {
            UnequipItem(item);
            return;
        }

        if (EquippedItems.Count >= 3)
        {
            UnityEngine.Debug.Log("Cannot equip more than 3 items.");
            return;
        }

        EquippedItems.Add(item);
        UpdateStats(item, true);
    }

    public void UnequipItem(Item item)
    {
        if (EquippedItems.Contains(item))
        {
            EquippedItems.Remove(item);
            UpdateStats(item, false);
        }
    }

    private void UpdateStats(Item item, bool isEquipping)
    {
        int modifier = isEquipping ? 1 : -1;

        switch (item.TypeStat1)
        {
            case 1:
                UnitsT1 += item.Stat1 * modifier;
                break;
            case 2:
                UnitsT1 += (int)(UnitsT1 * item.StatPerc * modifier);
                break;
            case 3:
                UnitsT2 += item.Stat1 * modifier;
                break;
            case 4:
                UnitsT2 += (int)(UnitsT2 * item.StatPerc * modifier);
                break;
            case 5:
                UnitsT3 += item.Stat1 * modifier;
                break;
            case 6:
                UnitsT3 += (int)(UnitsT3 * item.StatPerc * modifier);
                break;
        }

        switch (item.TypeStat2)
        {
            case 1:
                UnitsT1 += item.Stat2 * modifier;
                break;
            case 2:
                UnitsT1 += (int)(UnitsT1 * item.StatPerc * modifier);
                break;
            case 3:
                UnitsT2 += item.Stat2 * modifier;
                break;
            case 4:
                UnitsT2 += (int)(UnitsT2 * item.StatPerc * modifier);
                break;
            case 5:
                UnitsT3 += item.Stat2 * modifier;
                break;
            case 6:
                UnitsT3 += (int)(UnitsT3 * item.StatPerc * modifier);
                break;
        }
    }

    public int CalculatePlayerPower()
    {
        float power = UnitsT1 * ((AttUnit1 * MainMultiplicator) + (DefUnit1 * SecondaryMultiplicator) + (PvUnit1 * SecondaryMultiplicator))
                    + UnitsT2 * ((AttUnit2 * MainMultiplicator) + (DefUnit2 * SecondaryMultiplicator) + (PvUnit2 * SecondaryMultiplicator))
                    + UnitsT3 * ((AttUnit3 * MainMultiplicator) + (DefUnit3 * SecondaryMultiplicator) + (PvUnit3 * SecondaryMultiplicator));
        foreach (var item in EquippedItems)
        {
            switch (item.TypeStat1)
            {
                case 1:
                    power += item.Stat1;
                    break;
                case 2:
                    power += (int)(power * item.StatPerc);
                    break;
                case 3:
                    power += item.Stat1;
                    break;
                case 4:
                    power += (int)(power * item.StatPerc);
                    break;
                case 5:
                    power += item.Stat1;
                    break;
                case 6:
                    power += (int)(power * item.StatPerc);
                    break;
            }

            switch (item.TypeStat2)
            {
                case 1:
                    power += item.Stat2;
                    break;
                case 2:
                    power += (int)(power * item.StatPerc);
                    break;
                case 3:
                    power += item.Stat2;
                    break;
                case 4:
                    power += (int)(power * item.StatPerc);
                    break;
                case 5:
                    power += item.Stat2;
                    break;
                case 6:
                    power += (int)(power * item.StatPerc);
                    break;
            }
        }
        return Mathf.RoundToInt(power);
    }

    public void RemoveUnits(int unitsT1, int unitsT2, int unitsT3)
    {
        UnitsT1 = Mathf.Max(UnitsT1 - unitsT1, 0);
        UnitsT2 = Mathf.Max(UnitsT2 - unitsT2, 0);
        UnitsT3 = Mathf.Max(UnitsT3 - unitsT3, 0);
    }


    // Méthode pour calculer la puissance du joueur avec un nombre sélectionné d'unités
    public int CalculatePlayerPowerWithSelectedUnits(int selectedUnitsT1, int selectedUnitsT2, int selectedUnitsT3)
    {
        float power = selectedUnitsT1 * ((AttUnit1 * MainMultiplicator) + (DefUnit1 * SecondaryMultiplicator) + (PvUnit1 * SecondaryMultiplicator))
                    + selectedUnitsT2 * ((AttUnit2 * MainMultiplicator) + (DefUnit2 * SecondaryMultiplicator) + (PvUnit2 * SecondaryMultiplicator))
                    + selectedUnitsT3 * ((AttUnit3 * MainMultiplicator) + (DefUnit3 * SecondaryMultiplicator) + (PvUnit3 * SecondaryMultiplicator));

        return Mathf.RoundToInt(power);
    }

    public void Initialize()
    {
        StartProduction();
    }

    public void StartProduction()
    {
        productionCoroutineT1 ??= MonoBehaviourSingleton.Instance.StartCoroutine(ProduceUnits("T1", unitT1Queue));
        productionCoroutineT2 ??= MonoBehaviourSingleton.Instance.StartCoroutine(ProduceUnits("T2", unitT2Queue));
        productionCoroutineT3 ??= MonoBehaviourSingleton.Instance.StartCoroutine(ProduceUnits("T3", unitT3Queue));
    }

    public void StopProduction()
    {
        if (productionCoroutineT1 != null)
        {
            MonoBehaviourSingleton.Instance.StopCoroutine(productionCoroutineT1);
            productionCoroutineT1 = null;
        }
        if (productionCoroutineT2 != null)
        {
            MonoBehaviourSingleton.Instance.StopCoroutine(productionCoroutineT2);
            productionCoroutineT2 = null;
        }
        if (productionCoroutineT3 != null)
        {
            MonoBehaviourSingleton.Instance.StopCoroutine(productionCoroutineT3);
            productionCoroutineT3 = null;
        }
    }

    private IEnumerator ProduceUnits(string unitType, Queue<string> unitQueue)
    {
        while (true)
        {
            if (unitQueue.Count > 0)
            {
                yield return ProduceUnitFromQueue(unitQueue, unitType);
            }
            yield return null;
        }
    }

    private IEnumerator ProduceUnitFromQueue(Queue<string> unitQueue, string unitType)
    {
        if (unitQueue.Count > 0)
        {
            float elapsedTime = 0f;
            while (elapsedTime < productionInterval)
            {
                elapsedTime += Time.deltaTime;
                UpdateLoadingBar(unitType, elapsedTime / productionInterval);
                yield return null;
            }
            unitQueue.Dequeue();

            if (unitType == "T1")
            {
                UnitsT1++;
            }
            else if (unitType == "T2")
            {
                UnitsT2++;
            }
            else if (unitType == "T3")
            {
                UnitsT3++;
            }
        }
    }

    private void UpdateLoadingBar(string unitType, float progress)
    {
        UnitsManager.Instance.UpdateLoadingBar(unitType, progress);
    }

    public void EnqueueUnits(string unitType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (unitType == "T1")
            {
                unitT1Queue.Enqueue(unitType);
            }
            else if (unitType == "T2")
            {
                unitT2Queue.Enqueue(unitType);
            }
            else if (unitType == "T3")
            {
                unitT3Queue.Enqueue(unitType);
            }
        }
    }

    public void Save()
    {
        // Crée une instance de PlayerData avec les données actuelles
        PlayerData data = new PlayerData
        {
            UnitsT1 = UnitsT1,
            UnitsT2 = UnitsT2,
            UnitsT3 = UnitsT3,
            UnitT1Queue = new List<string>(unitT1Queue),
            UnitT2Queue = new List<string>(unitT2Queue),
            UnitT3Queue = new List<string>(unitT3Queue),
            resource1 = resource1,
            resource2 = resource2,
            resource3 = resource3,
            resource4 = resource4,
            resources1PerSecond = resources1PerSecond,
            resources2PerSecond = resources2PerSecond,
            resources3PerSecond = resources3PerSecond,
            resources4PerSecond = resources4PerSecond,
            LastSaveTime = DateTime.Now.ToBinary().ToString()
        };

        // Sérialiser les données en JSON
        string jsonData = JsonUtility.ToJson(data, true);

        // Écrire les données JSON dans un fichier
        File.WriteAllText(SaveFilePath, jsonData);

        //Debug.Log("Données sauvegardées dans " + SaveFilePath);
    }

    public void Load()
    {
        if (File.Exists(SaveFilePath))
        {
            // Lire les données JSON du fichier
            string jsonData = File.ReadAllText(SaveFilePath);

            // Désérialiser les données JSON en une instance de PlayerData
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);

            // Charger les données dans les variables du script
            UnitsT1 = data.UnitsT1;
            UnitsT2 = data.UnitsT2;
            UnitsT3 = data.UnitsT3;


            unitT1Queue.Clear();
            unitT2Queue.Clear();
            unitT3Queue.Clear();

            foreach (string unit in data.UnitT1Queue)
            {
                unitT1Queue.Enqueue(unit);
            }
            foreach (string unit in data.UnitT2Queue)
            {
                unitT2Queue.Enqueue(unit);
            }
            foreach (string unit in data.UnitT3Queue)
            {
                unitT3Queue.Enqueue(unit);
            }

            resource1 = data.resource1;
            resource2 = data.resource2;
            resource3 = data.resource3;
            resource4 = data.resource4;

            resources1PerSecond = data.resources1PerSecond;
            resources2PerSecond = data.resources2PerSecond;
            resources3PerSecond = data.resources3PerSecond;
            resources4PerSecond = data.resources4PerSecond;

            if (long.TryParse(data.LastSaveTime, out long lastSaveTimeBinary))
            {
                DateTime lastSaveTime = DateTime.FromBinary(lastSaveTimeBinary);
                ProcessOfflineProduction(lastSaveTime);
            }

            Debug.Log("Données chargées depuis " + SaveFilePath);
        }
        else
        {
            Debug.LogWarning("Aucun fichier de sauvegarde trouvé à " + SaveFilePath);
        }
    }

    private void ProcessOfflineProduction(DateTime lastSaveTime)
    {
        TimeSpan offlineTime = DateTime.Now - lastSaveTime;
        int secondsOffline = (int)offlineTime.TotalSeconds;

        ProduceOfflineUnits(unitT1Queue, ref UnitsT1, secondsOffline);
        ProduceOfflineUnits(unitT2Queue, ref UnitsT2, secondsOffline);
        ProduceOfflineUnits(unitT3Queue, ref UnitsT3, secondsOffline);
    }

    private void ProduceOfflineUnits(Queue<string> unitQueue, ref int unitCount, int secondsOffline)
    {
        int producedUnits = secondsOffline / (int)productionInterval;
        int unitsToProduce = Math.Min(producedUnits, unitQueue.Count);

        for (int i = 0; i < unitsToProduce; i++)
        {
            unitQueue.Dequeue();
            unitCount++;
        }
    }

    public void ResetUnits()
    {
        UnitsT1 = 0;
        UnitsT2 = 0;
        UnitsT3 = 0;
    }
}

public class InventoryContext
{
    public PlayerStats playerStats;

    public InventoryContext(PlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }
}

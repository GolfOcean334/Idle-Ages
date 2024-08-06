using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int UnitsT1;
    public int UnitsT2;
    public int UnitsT3;

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

    public int CalculatePlayerPower()
    {
        float power = UnitsT1 * ((AttUnit1 * MainMultiplicator) + (DefUnit1 * SecondaryMultiplicator) + (PvUnit1 * SecondaryMultiplicator))
                    + UnitsT2 * ((AttUnit2 * MainMultiplicator) + (DefUnit2 * SecondaryMultiplicator) + (PvUnit2 * SecondaryMultiplicator))
                    + UnitsT3 * ((AttUnit3 * MainMultiplicator) + (DefUnit3 * SecondaryMultiplicator) + (PvUnit3 * SecondaryMultiplicator));

        return Mathf.RoundToInt(power);
    }

    public void Initialize()
    {
        LoadSaveUnits();
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

            SaveAllUnits();
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

    public void SaveAllUnits()
    {
        PlayerPrefs.SetInt("UnitsT1", UnitsT1);
        PlayerPrefs.SetInt("UnitsT2", UnitsT2);
        PlayerPrefs.SetInt("UnitsT3", UnitsT3);
        PlayerPrefs.SetString("UnitT1Queue", string.Join(",", unitT1Queue.ToArray()));
        PlayerPrefs.SetString("UnitT2Queue", string.Join(",", unitT2Queue.ToArray()));
        PlayerPrefs.SetString("UnitT3Queue", string.Join(",", unitT3Queue.ToArray()));
        PlayerPrefs.SetString("LastSaveTime", DateTime.Now.ToBinary().ToString());
    }

    public void LoadSaveUnits()
    {
        UnitsT1 = PlayerPrefs.GetInt("UnitsT1", 0);
        UnitsT2 = PlayerPrefs.GetInt("UnitsT2", 0);
        UnitsT3 = PlayerPrefs.GetInt("UnitsT3", 0);

        string savedUnitT1Queue = PlayerPrefs.GetString("UnitT1Queue", "");
        string savedUnitT2Queue = PlayerPrefs.GetString("UnitT2Queue", "");
        string savedUnitT3Queue = PlayerPrefs.GetString("UnitT3Queue", "");

        unitT1Queue.Clear();
        unitT2Queue.Clear();
        unitT3Queue.Clear();

        if (!string.IsNullOrEmpty(savedUnitT1Queue))
        {
            foreach (string unit in savedUnitT1Queue.Split(','))
            {
                unitT1Queue.Enqueue(unit);
            }
        }
        if (!string.IsNullOrEmpty(savedUnitT2Queue))
        {
            foreach (string unit in savedUnitT2Queue.Split(','))
            {
                unitT2Queue.Enqueue(unit);
            }
        }
        if (!string.IsNullOrEmpty(savedUnitT3Queue))
        {
            foreach (string unit in savedUnitT3Queue.Split(','))
            {
                unitT3Queue.Enqueue(unit);
            }
        }

        if (long.TryParse(PlayerPrefs.GetString("LastSaveTime", ""), out long lastSaveTimeBinary))
        {
            DateTime lastSaveTime = DateTime.FromBinary(lastSaveTimeBinary);
            ProcessOfflineProduction(lastSaveTime);
        }
    }

    private void ProcessOfflineProduction(DateTime lastSaveTime)
    {
        TimeSpan offlineTime = DateTime.Now - lastSaveTime;
        int secondsOffline = (int)offlineTime.TotalSeconds;

        ProduceOfflineUnits(unitT1Queue, ref UnitsT1, secondsOffline);
        ProduceOfflineUnits(unitT2Queue, ref UnitsT2, secondsOffline);
        ProduceOfflineUnits(unitT3Queue, ref UnitsT3, secondsOffline);

        SaveAllUnits();
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
        SaveAllUnits();
        PlayerPrefs.Save();
    }
}

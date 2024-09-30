using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; // Ajout de l'espace de noms System.IO
using Debug = UnityEngine.Debug; // Alias pour UnityEngine.Debug
using Console = System.Diagnostics.Debug; // Alias pour System.Diagnostics.Debug

public class InventoryManager : MonoBehaviour
{
    private string saveFilePath;
    private InventoryData currentInventory;

    private void Awake()
    {
        saveFilePath = Path.Combine(UnityEngine.Application.persistentDataPath, "inventory.json");
        //LoadInventory();
    }

    private void OnApplicationQuit()
    {
        //SaveInventory();
    }

    public void SaveInventory()
    {
        try
        {
            string json = JsonUtility.ToJson(currentInventory);
            File.WriteAllText(saveFilePath, json);
            UnityEngine.Debug.Log("Inventory saved successfully.");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Failed to save inventory: {ex.Message}");
        }
    }

    public void LoadInventory()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                currentInventory = JsonUtility.FromJson<InventoryData>(json);
                DisplayInventory();
                UnityEngine.Debug.Log("Inventory loaded successfully.");
            }
            else
            {
                currentInventory = new InventoryData(10); // Initialiser avec un inventaire vide
                UnityEngine.Debug.Log("No inventory file found, starting with an empty inventory.");
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Failed to load inventory: {ex.Message}");
        }
    }

    public void AddBoughtItem(Item newItem)
    {
        currentInventory.AddItem(ref newItem);
        SaveInventory();
    }

    public InventoryData GetCurrentInventory()
    {
        return currentInventory;
    }

    private void DisplayInventory()
    {
        // Ajoutez ici la logique pour afficher l'inventaire
        foreach (var item in currentInventory.items)
        {
            UnityEngine.Debug.Log($"Item: {item.Title}, Count: {item.Count}");
        }
    }
}


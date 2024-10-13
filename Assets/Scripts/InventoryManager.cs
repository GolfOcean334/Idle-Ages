using System;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(UnityEngine.Application.persistentDataPath, "inventory.json");
        LoadInventory();
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    public void SaveInventory()
    {
        try
        {
            InventoryData inventoryData = new InventoryData(10); // Remplacez par votre méthode pour obtenir l'inventaire actuel
            string json = JsonUtility.ToJson(inventoryData);
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
                InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);
                // Remplacez par votre méthode pour afficher l'inventaire
                UnityEngine.Debug.Log("Inventory loaded successfully.");
            }
            else
            {
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
        // Ajoutez l'item à l'inventaire
        InventoryData inventoryData = new InventoryData(10); // Remplacez par votre méthode pour obtenir l'inventaire actuel
        inventoryData.AddItem(ref newItem);
        SaveInventory();
    }
}

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
    private Inventory inventory;

    private void Awake()
    {
        saveFilePath = Path.Combine(UnityEngine.Application.persistentDataPath, "inventory.json");
        LoadInventory();
    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    private void Start()
    {
        inventory = Inventory.Instance;
        LoadInventory();
    }

    public void SaveInventory()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory.items);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();

            inventory.items = new List<Item>(data.items); // Convertir le tableau en liste
            inventory.display.UpdateDisplay(inventory.items);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
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

    public void DisplayInventory()
    {
        // Ajoutez ici la logique pour afficher l'inventaire
        foreach (var item in currentInventory.items)
        {
            UnityEngine.Debug.Log($"Item: {item.Title}, Count: {item.Count}");
        }
    }
}
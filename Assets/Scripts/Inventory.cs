using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; // Ajout de l'espace de noms System.IO
using Debug = UnityEngine.Debug; // Alias pour UnityEngine.Debug
using Console = System.Diagnostics.Debug; // Alias pour System.Diagnostics.Debug

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private InventoryDisplay display;
    [SerializeField] private InventoryData data;
    public List<Item> items = new List<Item>();
    private InventoryContext context;

    private void Awake()
    {
        int _slotCount = display.Initialize(this);

        data = new InventoryData(_slotCount);

        display.UpdateDisplay(data.items);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Assurez-vous que l'objet persiste entre les scènes
            Debug.Log("Inventory persiste entre les scènes.");


        }
        else
        {
            Destroy(gameObject);
        }

    }



    public Item AddItem(Item _item)
    {
        if (!data.SlotAvailable(_item)) return _item;

        data.AddItem(ref _item);

        display.UpdateDisplay(data.items);

        return _item;
    }

    public Item PickItem(int _slotID)
    {
        Item _result = data.Pick(_slotID);

        display.UpdateDisplay(data.items);

        return _result;
    }

    public void SwapSlots(int _slotA, int _slotB)
    {
        data.Swap(_slotA, _slotB);

        display.UpdateDisplay(data.items);
    }

    public Item[] Data => data.items;
    public InventoryContext Context => context;
}

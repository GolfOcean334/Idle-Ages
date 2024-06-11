using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Item itemToPush, pickedItem;

    private Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        Add();
    }

    [ContextMenu("Test Push")]
    public void Add()
    {
        itemToPush = inventory.AddItem(itemToPush);
    }

    [ContextMenu("Test Pick")]
    public void Pick()
    {
        pickedItem = inventory.PickItem(1);
    }
}
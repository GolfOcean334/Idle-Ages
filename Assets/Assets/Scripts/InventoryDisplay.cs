using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    private int draggedSlotIndex;

    [SerializeField] private InventoryContextMenu contextMenu;
    private InventorySlot[] slots;
    private Inventory inventory;

    /// <summary>Return slot count.</summary>
    public int Initialize(Inventory _inventory)
    {
        slots = GetComponentsInChildren<InventorySlot>();
        inventory = _inventory;

        contextMenu.Init(inventory);

        for (var i = 0; i < slots.Length; i++)
        {
            slots[i].Initialize(this, i);
        }

        return slots.Length;
    }

    public void UpdateDisplay(Item[] _items)
    {
        for (var i = 0; i < _items.Length; i++)
        {
            slots[i].UpdateDisplay(_items[i]);
        }
    }

    #region Inputs

    public void ClickSlot(int _index)
    {
        Debug.Log($"Clic on slot {_index}");
        contextMenu.Select(_index, slots[_index]);
    }

    public void DragSlot(int _index) => draggedSlotIndex = _index;

    public void DropOnSlot(int _index)
    {
        print("Dropped " + draggedSlotIndex + " on " + _index);
        inventory.SwapSlots(_index, draggedSlotIndex);
    }

    #endregion
}
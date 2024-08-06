using UnityEngine;
using UnityEngine.UI;

public class InventoryContextMenu : MonoBehaviour
{
    private int targetSlotID;

    [SerializeField] private Button dropButton, consumeButton, useButton, equipButton, closeButton;
    private Inventory inventory;

    private void Awake()
    {
        dropButton.onClick.AddListener(Drop);
        consumeButton.onClick.AddListener(Consume);
        useButton.onClick.AddListener(Use);
        equipButton.onClick.AddListener(Equip);
        closeButton.onClick.AddListener(Close);
    }

    public void Init(Inventory _inventory)
    {
        inventory = _inventory;
    }

    public void Select(int _slotID, InventorySlot _slot)
    {
        Item _slotItem = inventory.Data[_slotID];
        ItemData _itemData = _slotItem.Data;

        if (_slotItem.Empty)
        {
            Close();
            return;
        }

        gameObject.SetActive(true);
        transform.position = _slot.transform.position;
        targetSlotID = _slotID;

        consumeButton.gameObject.SetActive(_itemData is IConsumable);
        useButton.gameObject.SetActive(_itemData is IUsable);
        equipButton.gameObject.SetActive(_itemData is IEquipable);
    }

    #region Inputs

    private void Drop()
    {
        inventory.PickItem(targetSlotID);
        Close();
    }

    private void Consume()
    {
        IConsumable _item = inventory.Data[targetSlotID].Data as IConsumable;
        _item.OnConsumed(inventory.Context);
        Close();
    }

    private void Use()
    {
        IUsable _item = inventory.Data[targetSlotID].Data as IUsable;
        _item.OnUsed(inventory.Context);
        Close();
    }

    public void Equip()
    {
        IEquipable _item = inventory.Data[targetSlotID].Data as IEquipable;
        _item.OnEquiped(inventory.Context);
        Close();
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
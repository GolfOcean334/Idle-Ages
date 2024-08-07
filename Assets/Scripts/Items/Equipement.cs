using UnityEngine;

[CreateAssetMenu(menuName = "Items/Tool Item Data", fileName = "Item")]
public class Equipement : ItemData, IDurable, IEquipable
{
    [SerializeField] private int durability;

    void IDurable.OnBreak(InventoryContext _ctx)
    {
        Debug.Log("Break");
    }

    void IEquipable.OnEquiped(InventoryContext _ctx)
    {
        Debug.Log("Equip");
    }

    void IDurable.OnRepair(InventoryContext _ctx)
    {
        Debug.Log("Repaired");
    }

    int IDurable.MaxDurability => durability;
}

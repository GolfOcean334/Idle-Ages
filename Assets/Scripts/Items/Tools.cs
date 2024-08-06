using UnityEngine;

[CreateAssetMenu(menuName = "Items/Tool Item Data", fileName = "Item")]
public class Tools : ItemData, IDurable, IEquipable, IUsable
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

    void IUsable.OnUsed(InventoryContext _ctx)
    {
        Debug.Log("Attack");
    }

    int IDurable.MaxDurability => durability;
}

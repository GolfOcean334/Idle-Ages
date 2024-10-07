using System;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipement Item Data", fileName = "Item")]
public class Equipement : ItemData, IDurable, IEquipable
{
    public int TypeStat1;
    public int TypeStat2;
    public float Stat1;
    public float Stat2;

    void IDurable.OnBreak(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Break");
    }

    void IEquipable.OnEquiped(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Equip");
        ApplyStats(_ctx);
    }

    void IDurable.OnRepair(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Repaired");
    }

    int IDurable.MaxDurability => durability;

    public void GenerateRandomValues()
    {
        durability = UnityEngine.Random.Range(50, 200);
        Stat1 = UnityEngine.Random.Range(5, 50);
        Stat2 = UnityEngine.Random.Range(5, 50);
        TypeStat1 = UnityEngine.Random.Range(1, 6); // Par exemple, 1 à 4 pour différents types
        TypeStat2 = UnityEngine.Random.Range(1, 6);
        StatPerc = UnityEngine.Random.Range(0.01f, 0.1f);
        title = "Equipement_" + UnityEngine.Random.Range(1, 1000);
        description = "Description_" + UnityEngine.Random.Range(1, 1000);
        price = UnityEngine.Random.Range(100, 1000);
        priceType = UnityEngine.Random.Range(1, 4); // Par exemple, 1 à 3 pour différents types
    }

    private void ApplyStats(InventoryContext _ctx)
    {
        // Appliquer les stats de l'équipement aux unités
        ApplyStat(_ctx, TypeStat1, Stat1);
        ApplyStat(_ctx, TypeStat2, Stat2);
    }

    private void ApplyStat(InventoryContext _ctx, int statType, float value)
    {
        switch (statType)
        {
            case 1: // AttackFlat
                _ctx.AttUnit1 += value;
                _ctx.AttUnit2 += value;
                _ctx.AttUnit3 += value;
                break;
            case 2: // AttackPercent
                _ctx.AttUnit1 *= 1 + value / 100f;
                _ctx.AttUnit2 *= 1 + value / 100f;
                _ctx.AttUnit3 *= 1 + value / 100f;
                break;
            case 3: // DefenseFlat
                _ctx.DefUnit1 += value;
                _ctx.DefUnit2 += value;
                _ctx.DefUnit3 += value;
                break;
            case 4: // DefensePercent
                _ctx.DefUnit1 *= 1 + value / 100f;
                _ctx.DefUnit2 *= 1 + value / 100f;
                _ctx.DefUnit3 *= 1 + value / 100f;
                break;
            case 5: // HealthFlat
                _ctx.PvUnit1 += value;
                _ctx.PvUnit2 += value;
                _ctx.PvUnit3 += value;
                break;
            case 6: // HealthPercent
                _ctx.PvUnit1 *= 1 + value / 100f;
                _ctx.PvUnit2 *= 1 + value / 100f;
                _ctx.PvUnit3 *= 1 + value / 100f;
                break;
        }
    }
}

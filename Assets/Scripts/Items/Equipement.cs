using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipement Item Data", fileName = "Item")]
public class Equipement : ItemData, IDurable, IEquipable
{
    public PlayerStats playerStats;

    void IDurable.OnBreak(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Break");
    }

    void IEquipable.OnEquiped(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Equip");

        // Créez une instance de l'item à équiper
        Item itemToEquip = new Item
        {
            Title = title,
            Description = description,
            icon = icon,
            Durability = durability,
            Stat1 = Stat1,
            Stat2 = Stat2,
            TypeStat1 = TypeStat1,
            TypeStat2 = TypeStat2,
            StatPerc = StatPerc,
            Price = price,
            PriceType = priceType
        };

        // Équipez ou déséquipez l'item
        playerStats.EquipItem(itemToEquip);
    }

    void IDurable.OnRepair(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Repaired");
    }

    int IDurable.MaxDurability => durability;

    // Méthode pour générer des valeurs aléatoires
    public void GenerateRandomValues()
    {
        durability = Random.Range(50, 200);
        Stat1 = Random.Range(5, 50);
        Stat2 = Random.Range(5, 50);
        TypeStat1 = Random.Range(1, 6); // Par exemple, 1 à 6 pour différents types
        TypeStat2 = Random.Range(1, 6);
        StatPerc = Random.Range(0.01f, 0.1f);
        title = "Equipement_" + Random.Range(1, 1000);
        description = "Description_" + Random.Range(1, 1000);
        price = Random.Range(100, 1000);
        priceType = Random.Range(1, 4); // Par exemple, 1 à 4 pour différents types
    }
}


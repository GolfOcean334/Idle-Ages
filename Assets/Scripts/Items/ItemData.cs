using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item Data", fileName = "Item")]
[Serializable] public class ItemData : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public int stackMaxCount = 1;
    [SerializeField] public Sprite icon;

    [SerializeField] public int durability;
    [SerializeField] public int Stat1;
    [SerializeField] public int Stat2;
    [SerializeField] public int TypeStat1;
    [SerializeField] public int TypeStat2; // Correction ici
    [SerializeField] public float StatPerc;

    // Pour le shop
    [SerializeField] public string title;
    [SerializeField] public string description;
    [SerializeField] public int price; // Garder ce champ privé
    [SerializeField] public int priceType; // Garder ce champ privé

    public int Price => price; // Propriété publique pour accéder à price
    public int PriceType => priceType;
    public string Title => title;
    public string Description => description;
    public int Durability => durability;
    public int Stat1Value => Stat1;
    public int Stat2Value => Stat2;
    public int Stat1Type => TypeStat1;
    public int Stat2Type => TypeStat2;
    public float StatPercentage => StatPerc;
}

public interface IConsumable
{
    void OnConsumed(InventoryContext _ctx);
}

public interface IUsable
{
    void OnUsed(InventoryContext _ctx);
}

public interface IDurable
{
    int MaxDurability { get; }

    void OnBreak(InventoryContext _ctx);
    void OnRepair(InventoryContext _ctx);
}

public interface IEquipable
{
    void OnEquiped(InventoryContext _ctx);
}

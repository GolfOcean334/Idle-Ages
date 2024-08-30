using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment Item Data", fileName = "Item")]
public class Equipement : ItemData, IDurable, IEquipable
{
    [SerializeField] private int durability;
    [SerializeField] private int Stat1;
    [SerializeField] private int Stat2;
    [SerializeField] private int TypeStat1;
    [SerializeField] private int TypeStat2;
    [SerializeField] private float StatPerc;


    // Pour le shop
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private int price; // Garder ce champ priv�
    [SerializeField] private int priceType; // Garder ce champ priv�


    public int Price => price; // Propri�t� publique pour acc�der � price
    public int PriceType => priceType;
    public string Title => title;
    public string Description => description;
    public int Durability => durability;
    public int Stat1Value => Stat1;
    public int Stat2Value => Stat2;
    public int Stat1Type => TypeStat1;
    public int Stat2Type => TypeStat2;
    public float StatPercentage => StatPerc;



    void IDurable.OnBreak(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Break");
    }

    void IEquipable.OnEquiped(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Equip");
    }

    void IDurable.OnRepair(InventoryContext _ctx)
    {
        UnityEngine.Debug.Log("Repaired");
    }

    int IDurable.MaxDurability => durability;

    // M�thode pour g�n�rer des valeurs al�atoires
    public void GenerateRandomValues()
    {
        durability = Random.Range(50, 200);
        Stat1 = Random.Range(5, 50);
        Stat2 = Random.Range(5, 50);
        TypeStat1 = Random.Range(1, 6); // Par exemple, 1 � 4 pour diff�rents types
        TypeStat2 = Random.Range(1, 6);
        StatPerc = Random.Range(0.01f, 0.1f);
        title = "Equipement_" + Random.Range(1, 1000);
        description = "Description_" + Random.Range(1, 1000);
        price = Random.Range(100, 1000);
        priceType = Random.Range(1, 3); // Par exemple, 1 � 3 pour diff�rents types
    }
}

public class EquipementManager : MonoBehaviour
{
    public Equipement equipement;

    void Start()
    {
        if (equipement != null)
        {
            UnityEngine.Debug.Log("Title: " + equipement.Title); // Utiliser la propri�t� publique
            UnityEngine.Debug.Log("Description: " + equipement.Description); // Utiliser la propri�t� publique
            UnityEngine.Debug.Log("Durability: " + equipement.Durability);
            UnityEngine.Debug.Log("Stat1: " + equipement.Stat1Value);
            UnityEngine.Debug.Log("Stat2: " + equipement.Stat2Value);
            UnityEngine.Debug.Log("TypeStat1: " + equipement.Stat1Type);
            UnityEngine.Debug.Log("TypeStat2: " + equipement.Stat2Type);
            UnityEngine.Debug.Log("StatPerc: " + equipement.StatPercentage);
            UnityEngine.Debug.Log("Price: " + equipement.Price); // Utiliser la propri�t� publique
            UnityEngine.Debug.Log("PriceType: " + equipement.PriceType); // Utiliser la propri�t� publique
        }
    }
}

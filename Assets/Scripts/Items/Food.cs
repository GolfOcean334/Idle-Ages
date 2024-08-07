using UnityEngine;

[CreateAssetMenu(menuName = "Items/Food Item Data", fileName = "Item")]
public class Food : ItemData, IConsumable
{
    [SerializeField] private int energieRecup = 1;

    void IConsumable.OnConsumed(InventoryContext _ctx)
    {
        Debug.Log("Energie récuperer = " + energieRecup);
    }
}

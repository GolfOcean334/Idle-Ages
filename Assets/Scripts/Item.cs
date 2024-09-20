using UnityEngine;

[System.Serializable]
public struct Item
{
    [SerializeField] private int count;
    [SerializeField] private ItemData data;
    [SerializeField] private int slotID;

    public string Title { get; set; }
    public string Description { get; set; }
    public Sprite icon { get; set; }
    public int Durability { get; set; }
    public int Stat1 { get; set; }
    public int Stat2 { get; set; }
    public int TypeStat1 { get; set; }
    public int TypeStat2 { get; set; }
    public float StatPerc { get; set; }
    public int Price { get; set; }
    public int PriceType { get; set; }

    public void Merge(ref Item _other)
    {
        if (Full) return;

        if (Empty) data = _other.Data;

        if (_other.data != data) throw new System.Exception("Try to merge different item types.");

        int _total = _other.count + count;

        if (_total <= data.stackMaxCount)
        {
            count = _total;
            _other.count = 0;
            return;
        }

        count = data.stackMaxCount;
        _other.count = _total - count;
    }

    public bool AvailableFor(Item _other) => Empty || (Data == _other.Data && !Full);

    public ItemData Data => data;
    public bool Full => data && count >= data.stackMaxCount;
    public bool Empty => count == 0 || data == null;
    public int Count => count;
    public int SlotID { get => slotID; set => slotID = value; }
}

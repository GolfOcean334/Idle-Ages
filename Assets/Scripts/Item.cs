using UnityEngine;

/// <summary>Un stack d'item "réel", qui peut etre stocké dans l'inventaire</summary>
[System.Serializable]
public struct Item
{
    [SerializeField] private int count;
    [SerializeField] private ItemData data;

    /// <summary>Essaye de fusionner les stacks.</summary>
    public void Merge(ref Item _other)
    {
        if(Full) return;

        if(Empty) data = _other.Data;

        if (_other.data != data) throw new System.Exception("Try to merge differents item types.");

        int _total = _other.count + count;

        //Peut directement merge
        if (_total <= data.stackMaxCount)
        {
            count = _total;
            _other.count = 0;
            return;
        }

        count = data.stackMaxCount;
        _other.count = _total - count;
    }

    /// <summary>Retourne si l'item peut se merge avec un autre item.</summary>
    public bool AvailableFor(Item _other) => Empty || (Data == _other.Data && !Full);

    public ItemData Data => data;
    public bool Full => data && count >= data.stackMaxCount;
    public bool Empty => count == 0 || data == null;
    public int Count => count;
}
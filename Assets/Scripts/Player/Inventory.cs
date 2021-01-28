using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<CollectableType, uint> items;

    void Start()
    {
        items = new Dictionary<CollectableType, uint>();
    }

    public bool Contains(CollectableType itemType)
    {
        if (items.TryGetValue(itemType, out uint count))
            return count > 0;
        return false;
    }

    public void AddItem(CollectableType itemType)
    {
        if (items.ContainsKey(itemType))
            items[itemType] += 1;
        else
            items[itemType] = 1;
    }

    public void RemoveItem(CollectableType itemType)
    {
        if (!items.ContainsKey(itemType) || items[itemType] == 0)
        {
            Debug.LogError("Trying to remove an item not contained in the inventory.");
            return;
        }

        items[itemType] -= 1;
    }
}

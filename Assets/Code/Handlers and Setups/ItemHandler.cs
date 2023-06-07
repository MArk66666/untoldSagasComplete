using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private ItemsVisualizer itemsVisualizer;
    private List<Item> _acquiredItems = new List<Item>();

    public void AddItem(Item item)
    {
        if (_acquiredItems.Count >= itemsVisualizer.GetMaxIconsAmount())
        {
            Item firstItem = _acquiredItems[0];
            RemoveItem(firstItem);
        }

        _acquiredItems.Add(item);
        itemsVisualizer.UpdateItemsGrid(_acquiredItems);
    }

    public void RemoveItem(Item item)
    {
        if (!_acquiredItems.Contains(item)) return;
        _acquiredItems.Remove(item);
        itemsVisualizer.UpdateItemsGrid(_acquiredItems);
    }

    public bool CheckForItem(Item requiredItem)
    {
        foreach (Item item in _acquiredItems)
        {
            if (requiredItem == item) return true;
        }
        return false;
    }
}

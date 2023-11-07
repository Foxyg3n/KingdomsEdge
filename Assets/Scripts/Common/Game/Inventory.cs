using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public int size { get; private set; }
    public int count => items.Count;
    public int left => size - count;
    public bool isFull => items.Count == size;

    private List<Drop> items = new();

    public Inventory(int size) {
        this.size = size;
    }

    public void AddItems(List<Drop> itemList) {
        items.AddRange(itemList);
    }

    public void AddItem(Drop item) {
        items.Add(item);
    }

    public void RemoveItem(Drop item) {
        items.Remove(item);
    }

    public void PassAll(Inventory inventory) {
        foreach(Drop item in items.GetRange(0, Mathf.Min(inventory.left, items.Count))) {
            RemoveItem(item);
            inventory.AddItem(item);
        }
    }

}
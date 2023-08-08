using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    List<ShopItem> items = new List<ShopItem>();

    public void AddItem(ShopItem item)
    {
        items.Add(item);
    }

    public void UseAllItems()
    {
        foreach (ShopItem item in items)
        {
            item.Use();
        }
        items = new List<ShopItem>();
    }
}

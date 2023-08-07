using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] float supplyInterval = 5f;
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] GameManager gameManager;
    public Action OnOpen;
    public Action OnClose;
    public Action<ShopItem> OnBuy;
    public Action OnBuyFailure;

    public List<ShopItem> ShopItems = new List<ShopItem>();

    ShopItem[] shopItemCandidates = new ShopItem[]
    {
        new Bomb(),
        new AttackUp()
    };

    float supplyTime;

    private void Update()
    {
        if (scheduleManager.IsPlaying)
        {
            supplyTime += Time.deltaTime;
            if (supplyTime > supplyInterval)
            {
                supplyTime = 0;
                ShopItems.Add(GetRandomItem());
            }
        }
    }

    public void Open()
    {
        OnOpen();
    }
    public void Close()
    {
        OnClose();
    }

    ShopItem GetRandomItem()
    {
        List<ShopItem> candidates = new List<ShopItem>();
        int sumWeight = 0;
        for (int i = 0; i < shopItemCandidates.Length; i++)
        {
            ShopItem item = shopItemCandidates[i];
            if (item.EntryDay <= scheduleManager.Day)
            {
                sumWeight += item.Weight;
                candidates.Add(item);
            }
        }

        int random = UnityEngine.Random.Range(1, sumWeight + 1);
        ShopItem result = null;
        int w = 0;
        foreach (ShopItem shopItem in candidates)
        {
            w += shopItem.Weight;
            if (random <= w)
            {
                result = shopItem;
                break;
            }
        }
        return result;
    }

    public void Buy(ShopItem item)
    {
        if (item.Price <= gameManager.Money)
        {
            gameManager.UseMoney(item.Price);
            ShopItems.Remove(item);
            OnBuy(item);
        }
        else
        {
            OnBuyFailure();
        }
    }
}

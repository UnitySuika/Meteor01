using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopViewer : MonoBehaviour
{
    [SerializeField] Shop shop;
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform itemRoot;
    [SerializeField] ItemObject itemPrefab;
    [SerializeField] CheckPanel checkPanel;
    [SerializeField] GameObject buyFailureMessagePrefab;

    bool isCheckPanelOpen;
    bool isShopOpen;

    List<ItemObject> itemObjects = new List<ItemObject>();

    private void Start()
    {
        shop.OnOpen += () =>
        {
            isShopOpen = true;
            panel.SetActive(true);

            foreach (ShopItem item in shop.ShopItems)
            {
                ItemObject obj = Instantiate(itemPrefab, itemRoot);
                obj.ShopItem = item;
                obj.NameText.text = item.Name;
                obj.PriceText.text = item.Price.ToString();
                obj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (!isCheckPanelOpen)
                    {
                        OnItemButton(obj.ShopItem);
                    }
                });
                itemObjects.Add(obj);
            }
        };
        shop.OnClose += () =>
        {
            isShopOpen = false;
            panel.SetActive(false);

            foreach (ItemObject obj in itemObjects)
            {
                Destroy(obj.gameObject);
            }
            itemObjects = new List<ItemObject>();
        };
        shop.OnBuy += item =>
        {
            isCheckPanelOpen = false;
            checkPanel.gameObject.SetActive(false);

            ItemObject target = null;
            foreach (ItemObject obj in itemObjects)
            {
                if (obj.ShopItem == item)
                {
                    target = obj;
                }
            }
            if (target)
            {
                Destroy(target.gameObject);
                itemObjects.Remove(target);
            }
        };
        shop.OnBuyFailure += () =>
        {
            Instantiate(buyFailureMessagePrefab, checkPanel.transform);
        };
    }

    private void Update()
    {
        if (isShopOpen)
        {
            moneyText.text = gameManager.Money.ToString();
        }
    }

    void OnItemButton(ShopItem item)
    {
        isCheckPanelOpen = true;
        checkPanel.gameObject.SetActive(true);
        checkPanel.Init(item, item =>
        {
            shop.Buy(item);
        }, () =>
        {
            isCheckPanelOpen = false;
            checkPanel.gameObject.SetActive(false);
        });
    }

    public void OnCloseButton()
    {
        shop.Close();
    }
}

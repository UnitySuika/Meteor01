using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckPanel : MonoBehaviour
{
    public TextMeshProUGUI NameText => _NameText;
    [SerializeField] TextMeshProUGUI _NameText;
    public TextMeshProUGUI PriceText => _PriceText;
    [SerializeField] TextMeshProUGUI _PriceText;
    public TextMeshProUGUI DescriptionText => _DescriptionText;
    [SerializeField] TextMeshProUGUI _DescriptionText;

    [SerializeField] Button buyButton;
    [SerializeField] Button cancelButton;

    public Action<ShopItem> OnBuyButton;
    public Action OnCancelButton;

    ShopItem shopItem;

    private void Start()
    {
        buyButton.onClick.AddListener(() => OnBuyButton(shopItem));
        cancelButton.onClick.AddListener(() => OnCancelButton());
    }

    public void Init(ShopItem item, Action<ShopItem> onBuyButton, Action onCancelButton)
    {
        this.shopItem = item;
        NameText.text = item.Name;
        PriceText.text = item.Price.ToString();
        DescriptionText.text = item.Description;
        OnBuyButton = onBuyButton;
        OnCancelButton = onCancelButton;
    }
}

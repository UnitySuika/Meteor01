using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ShopItem ShopItem;
    public TextMeshProUGUI NameText => _NameText;
    [SerializeField] TextMeshProUGUI _NameText;
    public TextMeshProUGUI PriceText => _PriceText;
    [SerializeField] TextMeshProUGUI _PriceText;
}

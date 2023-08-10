using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DayItemViewer : MonoBehaviour
{
    [SerializeField] ItemButton itemPrefab;
    Item[] items = new Item[]
    {
        new Clean(), new DoubleBeam(), new CureTown()
    };

    Item clickedItem;

    public void ShowItems()
    {
        clickedItem = null;
        for (int i = 0; i < items.Length; i++)
        {
            ItemButton itemButton = Instantiate(itemPrefab, transform);
            // TODO アイテムの見た目を設定
            itemButton.Init(items[i], item => clickedItem = item);
        }
    }

    public void HideItems()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    public async UniTask<Item> WaitItemButtonClick()
    {
        await UniTask.WaitUntil(() => clickedItem != null);
        Debug.Log(clickedItem);
        return clickedItem;
    }
}

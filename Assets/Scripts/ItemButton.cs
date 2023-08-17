using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

public class ItemButton : MonoBehaviour
{
    public void Init(Item item, Action<Item> buttonClicked)
    {
        SetImage(item).Forget();
        GetComponent<Button>().onClick.AddListener(() => buttonClicked(item));
    }

    async UniTask<Item> SetImage(Item item)
    {
        GetComponent<Image>().enabled = false;
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(item.ImagePath);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite sprite = handle.Result;
            Debug.Log(sprite.name);
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = sprite;
            return item;
        }
        else
        {
            Debug.Log("Spriteのロードに失敗");
            return null;
        }
    }
}

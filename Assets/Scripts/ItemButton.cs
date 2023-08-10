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

    async UniTask SetImage(Item item)
    {
        Debug.Log("あ");
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(item.ImagePath);
        await handle.Task;
        Debug.Log("は");

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("い");
            Sprite sprite = handle.Result;
            Debug.Log(sprite.name);
            GetComponent<Image>().sprite = sprite;
        }
        else
        {
            Debug.Log("う");
            Debug.Log("Spriteのロードに失敗");
        }
    }
}

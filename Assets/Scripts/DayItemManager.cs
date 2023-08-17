using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class DayItemManager : MonoBehaviour
{
    [SerializeField] Button itemUseButton;
    [SerializeField] MeteorSpawner meteorSpawner;
    [SerializeField] Turret turret;
    [SerializeField] Ground ground;
    [SerializeField] ScheduleManager scheduleManager;

    private void Start()
    {
        scheduleManager.DayEnd += () => itemUseButton.gameObject.SetActive(false);
    }

    public void AddItem(Item item)
    {
        item.Init(meteorSpawner, turret, ground);
        if (item.IsImmediatelyUse)
        {
            scheduleManager.DayStart += () => item.Use();
        }
        else
        {
            itemUseButton.onClick.AddListener(() => item.Use());
            ShowItemButton(item).Forget();
        }
    }

    async UniTask ShowItemButton(Item item)
    {
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(item.ImagePath);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            itemUseButton.gameObject.SetActive(true);
            itemUseButton.GetComponent<Image>().sprite = handle.Result;
        }
        else
        {
            Debug.Log("スプライト読み込みに失敗");
        }
    }
}

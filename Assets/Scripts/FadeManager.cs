using Cysharp.Threading.Tasks;
using SuikaDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] FadeCanvas fadeCanvas;

    bool isFading = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public async UniTask FadeAndLoad(float fadeOutTime, string loadSceneName, float fadeInTime)
    {
        if (isFading) return;
        isFading = true;
        fadeCanvas.gameObject.SetActive(true);
        Image fadeImage = fadeCanvas.FadeImage;
        fadeImage = fadeCanvas.transform.GetComponentInChildren<Image>();

        float t = 0;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            Color c = fadeImage.color;
            c.a = Mathf.Lerp(0, 1, t / fadeOutTime);
            fadeImage.color = c;
            await UniTask.Yield();
        }
        await SceneManager.LoadSceneAsync(loadSceneName);
        t = 0;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            Color c = fadeImage.color;
            c.a = Mathf.Lerp(1, 0, t / fadeOutTime);
            fadeImage.color = c;
            await UniTask.Yield();
        }

        fadeCanvas.gameObject.SetActive(false);
        isFading = false;
    }
}

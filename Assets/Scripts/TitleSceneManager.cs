using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] Image fadeImage;
    [SerializeField] GameObject tapText;

    private void Awake()
    {
        // フレームレートを60に
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        bool isTouch = false;
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTouch = true;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                isTouch = true;
            }
        }
        if (isTouch)
        {
            AudioManager.Instance.PlaySE("System1");
            FadeManager.Instance.FadeAndLoad(fadeOutTime, "GameScene", 0f).Forget();
        }
    }
}

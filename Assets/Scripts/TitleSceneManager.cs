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
    [SerializeField] Button startButton;
    [SerializeField] Button tutorialButton;
    [SerializeField] TutorialWindow tutorialWindow;

    private void Awake()
    {
        // フレームレートを60に
        Application.targetFrameRate = 60;
        startButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySE("System1");
            FadeManager.Instance.FadeAndLoad(fadeOutTime, "GameScene", 0f).Forget();
        });
        tutorialButton.onClick.AddListener(() =>
        {
            tutorialWindow.gameObject.SetActive(true);
        });
    }
}

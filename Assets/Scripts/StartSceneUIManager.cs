using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUIManager : MonoBehaviour
{
    [SerializeField] string[] scenes;
    [SerializeField] Slider slider;
    public void OnStartButton()
    {
        SceneManager.LoadScene(scenes[(int)slider.value]);
    }
}

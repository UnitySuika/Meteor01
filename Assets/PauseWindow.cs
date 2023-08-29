using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public void OnResumeButton()
    {
        StartCoroutine(Resume());
    }

    IEnumerator Resume()
    {
        yield return null;
        gameManager.ResumeGame();
        gameObject.SetActive(false);
    }

    public void OnToTitleButton()
    {
        gameManager.ToTitle();
    }
}

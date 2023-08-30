using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWindow : MonoBehaviour
{
    [SerializeField] int tutorialPanelCount;
    [SerializeField] RectTransform panelRoot;
    [SerializeField] Button arrowRightButton;
    [SerializeField] Button arrowLeftButton;
    [SerializeField] Button closeButton;

    bool isMoving = false;
    int tutorialPanelIndex;

    private void Start()
    {
        tutorialPanelIndex = 0;
        arrowRightButton.onClick.AddListener(() => Move(true));
        arrowLeftButton.onClick.AddListener(() => Move(false));
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        PanelPositionUpdate();
        ArrayActiveUpdate();
    }

    void Move(bool isRight)
    {
        if (isMoving) return;
        isMoving = true;
        if (isRight && tutorialPanelIndex < tutorialPanelCount - 1)
        {
            tutorialPanelIndex++;
        }
        else if (!isRight && tutorialPanelIndex > 0)
        {
            tutorialPanelIndex--;
        }
        PanelPositionUpdate();
        ArrayActiveUpdate();
        isMoving = false;
    }

    void PanelPositionUpdate()
    {
        panelRoot.anchoredPosition = Vector2.left * 1290f * tutorialPanelIndex;
    }

    void ArrayActiveUpdate()
    {
        if (tutorialPanelIndex == 0)
        {
            arrowLeftButton.gameObject.SetActive(false);
            arrowRightButton.gameObject.SetActive(true);
        }
        else if (tutorialPanelIndex == tutorialPanelCount - 1)
        {
            arrowLeftButton.gameObject.SetActive(true);
            arrowRightButton.gameObject.SetActive(false);
        }
        else
        {
            arrowLeftButton.gameObject.SetActive(true);
            arrowRightButton.gameObject.SetActive(true);
        }
    }
}

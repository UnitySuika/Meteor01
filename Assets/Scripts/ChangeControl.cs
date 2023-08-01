using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControl : MonoBehaviour
{
    [SerializeField] TurretInputByButton buttonInput;
    [SerializeField] TurretInputByDrag dragInput;
    [SerializeField] Button changeButton;
    [SerializeField] GameObject inputButtons;
    [SerializeField] Turret turret;

    bool isButtonInput;

    private void Start()
    {
        turret.currentInput = 1;
        isButtonInput = false;
        inputButtons.SetActive(false);
        buttonInput.enabled = false;
        dragInput.enabled = true;

        changeButton.onClick.AddListener(() =>
        {
            isButtonInput = !isButtonInput;
            if (isButtonInput)
            {
                turret.currentInput = 0;
                inputButtons.SetActive(true);
                buttonInput.enabled = true;
                dragInput.enabled = false;
            }
            else
            {
                turret.currentInput = 1;
                inputButtons.SetActive(false);
                buttonInput.enabled = false;
                dragInput.enabled = true;
            }
        });
    }
}

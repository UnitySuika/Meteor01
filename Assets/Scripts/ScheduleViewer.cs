using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScheduleViewer : MonoBehaviour
{
    [SerializeField] ScheduleManager scheduleManager;
    [SerializeField] TextMeshProUGUI dayText;

    private void Update()
    {
        dayText.text = "DAY "+ scheduleManager.Day.ToString();
    }
}

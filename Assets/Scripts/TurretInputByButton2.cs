using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TurretInputByButton2 : MonoBehaviour, TurretInput
{
    [SerializeField] float rotationSpeed;

    [SerializeField] EventTrigger leftRotateButton;
    [SerializeField] EventTrigger rightRotateButton;
    [SerializeField] Button shootButton;

    public Action Shoot { get; set; }

    float angle;

    enum PointerState
    {
        LeftPointerDown,
        PointerUp,
        RightPointerDown
    }

    PointerState currentPointerState;

    public float GetAngle()
    {
        return angle;
    }

    void Start()
    {
        currentPointerState = PointerState.PointerUp;

        AddEventToEventTrigger(leftRotateButton, EventTriggerType.PointerEnter, _ => currentPointerState = PointerState.LeftPointerDown);
        AddEventToEventTrigger(leftRotateButton, EventTriggerType.PointerExit, _ => currentPointerState = PointerState.PointerUp);

        AddEventToEventTrigger(rightRotateButton, EventTriggerType.PointerEnter, _ => currentPointerState = PointerState.RightPointerDown);
        AddEventToEventTrigger(rightRotateButton, EventTriggerType.PointerExit, _ => currentPointerState = PointerState.PointerUp);

        shootButton.onClick.AddListener(() => Shoot());
    }

    void AddEventToEventTrigger(EventTrigger eventTrigger, EventTriggerType type, UnityAction<BaseEventData> call)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(call);
        eventTrigger.triggers.Add(entry);
    }

    void Update()
    {
        if (currentPointerState == PointerState.LeftPointerDown && Input.GetMouseButton(0))
        {
            angle += rotationSpeed * Time.deltaTime;
        }
        else if (currentPointerState == PointerState.RightPointerDown && Input.GetMouseButton(0))
        {
            angle -= rotationSpeed * Time.deltaTime;
        }
    }
}

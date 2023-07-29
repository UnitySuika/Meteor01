using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject gun;
    
    TurretRotationInput turretRotationInput;

    private void Start()
    {
        turretRotationInput = GetComponent<TurretRotationInput>();
    }


    private void Update()
    {
        SetGunRotation(turretRotationInput.GetRotation());
    }

    void SetGunRotation(float angle)
    {
        Vector3 rot = gun.transform.eulerAngles;
        rot.z = angle;
        gun.transform.eulerAngles = rot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface TurretInput
{
    Action Shoot { get; set; }
    float GetAngle();
}

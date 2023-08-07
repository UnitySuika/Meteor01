using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFailureMessage : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}

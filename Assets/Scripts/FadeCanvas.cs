using SuikaDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : Singleton<FadeCanvas>
{
    public Image FadeImage => fadeImage;
    [SerializeField] Image fadeImage;
}

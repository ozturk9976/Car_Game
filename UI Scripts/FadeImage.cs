using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeImage : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] float waitFadedTime;
    [SerializeField] float fadeTime;


    private IEnumerator Fade()
    {
        fadeImage.DOFade(1, fadeTime);
        yield return new WaitForSeconds(waitFadedTime);
        fadeImage.DOFade(0, fadeTime);
    }
}

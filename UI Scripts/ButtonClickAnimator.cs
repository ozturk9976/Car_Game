using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonClickAnimator : MonoBehaviour
{

    [SerializeField] private Image imageToAnimate;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private float animationSpeed;
    [SerializeField] private Vector3 defaultScale;


    public void AnimateButton()
    {
        // imageToAnimate.color = Color.red;
        imageToAnimate.transform.DOScale(targetScale, animationSpeed).SetEase(Ease.InFlash);
    }
    public void ResetAnimateButton()
    {
        // imageToAnimate.color = Color.white;
        imageToAnimate.transform.DOScale(defaultScale, animationSpeed).SetEase(Ease.InFlash);
    }
}

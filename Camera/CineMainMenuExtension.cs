using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CineMainMenuExtension : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook vcam;
    private CinemachineComposer[] rigs = new CinemachineComposer[3];

    public void GoPaintMode()
    {

        for (int i = 0; i < 3; i++)
        {
            rigs[i] = vcam.GetRig(i).GetCinemachineComponent<CinemachineComposer>();
        }

        foreach (var item in rigs)
        {
            DOTween.To(() => item.m_ScreenX, x => item.m_ScreenX = x, 0.5f, 1f).SetEase(Ease.OutQuart);
            DOTween.To(() => item.m_ScreenY, y => item.m_ScreenY = y, 0.5f, 1f).SetEase(Ease.OutQuart);

            DOTween.To(() => item.m_TrackedObjectOffset.y, x => item.m_TrackedObjectOffset.y = x, 0f, 1f).SetEase(Ease.OutQuart);
        }
    }

    public void ExitPaintMode()
    {
        for (int i = 0; i < 3; i++)
        {
            rigs[i] = vcam.GetRig(i).GetCinemachineComponent<CinemachineComposer>();
        }

        foreach (var item in rigs)
        {
            DOTween.To(() => item.m_ScreenX, x => item.m_ScreenX = x, 0.65f, 0.5f).SetEase(Ease.OutQuart);
            DOTween.To(() => item.m_ScreenY, y => item.m_ScreenY = y, 0.65f, 0.5f).SetEase(Ease.OutQuart);

            DOTween.To(() => item.m_TrackedObjectOffset.y, x => item.m_TrackedObjectOffset.y = x, -0.6f, 1f).SetEase(Ease.OutQuart);
        }
    }
}

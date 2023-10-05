using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocker_Button : MonoBehaviour
{
    public void OnPointerDownAnyButton()
    {
        CineExtension.instance.ResetAxisValues();
        CineExtension.instance.isTouchingButton++;
    }
    public void OnPointerUpAnyButton()
    {
        CineExtension.instance.isTouchingButton--;
    }
}

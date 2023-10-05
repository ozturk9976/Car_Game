using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraLocker : MonoBehaviour
{

    [SerializeField] private CineExtension cineExtension;

    private void Start()
    {
        cineExtension.isTouchingButton++;
    }

    public void OnPointerDownUnlockCamera()
    {
        cineExtension.isTouchingButton--;
    }

    public void OnPointerUpLockCamera()
    {
        cineExtension.isTouchingButton++;
    }
}

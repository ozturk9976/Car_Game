using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineInputIncOrDcr : MonoBehaviour
{
    public void IncreaseCinemachineInput()
    {
        CineExtension.instance.isTouchingButton++;
    }
    public void DecreaseCinemachineInput()
    {
        CineExtension.instance.isTouchingButton--;
    }
}

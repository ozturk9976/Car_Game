using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeButtonBehaivour : MonoBehaviour
{
    public void OnPointerDown()
    {
        CarLights.instance.TurnOnBrakeLights();
    }

    public void OnPointerUp()
    {
        CarLights.instance.TurnOffBrakeLights();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarLightController : MonoBehaviour
{
    [SerializeField] List<GameObject> Lights = new List<GameObject>();
    public static CarLightController instance;

    private void Awake()
    {
        instance = this;
    }

    public void TurnOn()
    {
        foreach (GameObject brakeLight in Lights)
        {
            brakeLight.SetActive(true);
        }
    }
    public void TurnOff()
    {
        foreach (GameObject brakeLight in Lights)
        {
            brakeLight.SetActive(false);
        }
    }

}

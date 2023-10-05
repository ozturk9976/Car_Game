using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{
    [SerializeField] private GameObject brakeLights;

    public static CarLights instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (CheckForNight())
        {
            CarLightController.instance.TurnOn();
        }

    }



    public bool CheckForNight()
    {
        return GameObject.Find("Directional Light").GetComponent<Light>().intensity < 0.5f;
    }

    public void TurnOnBrakeLights()
    {
        brakeLights.gameObject.SetActive(true);
    }

    public void TurnOffBrakeLights()
    {
        brakeLights.gameObject.SetActive(false);
    }
}

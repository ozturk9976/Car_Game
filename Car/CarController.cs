using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using DG.Tweening;
using JetBrains.Annotations;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CarCollider))]
[RequireComponent(typeof(CarAudio))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CarPaintController))]



public class CarController : MonoBehaviour
{


    [Header("Scripts")]
    [SerializeField] CarAudio carAudio;
    [SerializeField] SteeringWheelPhysical steeringWheelPhysical;
    [SerializeField] private bool useCustomCenterOfMass;
    [SerializeField] Vector3 customCentreOfMass;

    [SerializeField] Rigidbody rb;
    [Header("Wheel Stuff")]
    [SerializeField] private float brakePower;
    [SerializeField] private float wheelTurn_Degree;
    [SerializeField] private float wheelTurn_Smoothing;


    [Header("Car Settings")]

    [SerializeField] private float maxAcceleration;
    [SerializeField] private float torqueSmoothing;



    private static float _speed;
    private PlayerInput _playerInput;
    public static CarController instance;

    private float _steering;

    float velocity = 0.0f;


    private float _torqueSmoothing;
    float movementVectorY;
    float _maxAcceleration;
    float _time;

    bool isInPlatform = false;
    private void Awake()
    {
        instance = this;

        _playerInput = new PlayerInput();

        _playerInput.Gameplay.Brake.performed += OnStopCar;
        _playerInput.Gameplay.Brake.canceled += OnDontStopCar;
        // _playerInput.Gameplay.Gear.performed += OnGearCar;


        if (useCustomCenterOfMass)
        {
            rb.centerOfMass += customCentreOfMass;
        }
    }

    private void OnEnable()
    {
        _playerInput.Gameplay.Enable();

        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnLevelPass, OnLevelPass);
        EventManager.AddHandler(GameEvent.OnPlatformIn, StopCar);
        EventManager.AddHandler(GameEvent.OnPlatformOut, DontStopCar);
        EventManager.AddHandler(GameEvent.OnPlatformOut, SetParentToNull);
        EventManager.AddHandler(GameEvent.OnPlatformIn, OnPlatformIn);
        EventManager.AddHandler(GameEvent.OnPlatformOut, OnPlatformOut);

        //Brakes
        EventManager.AddHandler(GameEvent.OnBrake, StopCar);
        EventManager.AddHandler(GameEvent.OnBrake_UP, DontStopCar);
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Disable();

        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, OnLevelPass);
        EventManager.RemoveHandler(GameEvent.OnPlatformIn, StopCar);
        EventManager.RemoveHandler(GameEvent.OnPlatformOut, DontStopCar);
        EventManager.RemoveHandler(GameEvent.OnPlatformOut, SetParentToNull);
        EventManager.RemoveHandler(GameEvent.OnPlatformIn, OnPlatformIn);
        EventManager.RemoveHandler(GameEvent.OnPlatformOut, OnPlatformOut);

        //Brakes
        EventManager.RemoveHandler(GameEvent.OnBrake, StopCar);
        EventManager.RemoveHandler(GameEvent.OnBrake_UP, DontStopCar);


        PlayerPrefs.SetString("LastPlayedCar", this.gameObject.name);
    }



    private void Update()
    {
        Wheels.instance.TORQUE = Mathf.SmoothDamp(Wheels.instance.TORQUE
        , maxAcceleration * InputManager.API._movementVector.y
        , ref velocity,/* _*/ torqueSmoothing /** (Get_Speed + 0.1f)*/);

    }

    public float Steering
    {
        get { return _steering; }
        set { Wheels.instance.STEERING = value; }
    }
    public float Get_Speed
    {
        get
        {
            _speed = rb.velocity.magnitude * 3.6f;
            return _speed;
        }
    }
    // private IEnumerator OptimizedUpdate(int waitFrameCount)
    // {
    //     while (true)
    //     {
    //         for (int i = 0; i < waitFrameCount; i++)
    //         {
    //             yield return new WaitForEndOfFrame();
    //         }
    //     }
    // }
    public void OnPlatformIn()
    {
        isInPlatform = true;
    }

    public void OnPlatformOut()
    {
        isInPlatform = false;
    }

    #region Input System Callbacks

    // public void OnBrakeCar(InputAction.CallbackContext context)
    // {

    //     foreach (Wheel col in Wheels.instance.WheelList)
    //     {
    //         col.wheelSetting.WC.brakeTorque = brakePower;
    //     }
    // }

    // public void OnNotBrakeCar(InputAction.CallbackContext context)
    // {   
    //     foreach (Wheel col in Wheels.instance.WheelList)
    //     {
    //         col.wheelSetting.WC.brakeTorque = 0;
    //     }
    // }
    public void OnStopCar(InputAction.CallbackContext context)
    {
        foreach (Wheel col in Wheels.instance.WheelList)
        {
            col.wheelSetting.WC.brakeTorque = (brakePower * 3);
        }
    }
    public void OnDontStopCar(InputAction.CallbackContext context)
    {
        if (isInPlatform)
        {
            return;
        }
        else
        {
            foreach (Wheel col in Wheels.instance.WheelList)
            {

                col.wheelSetting.WC.brakeTorque = 0;
            }
        }
    }
    public void OnGearCar(InputAction.CallbackContext context)
    {

    }
    #endregion


    #region Global Event Callbacks
    public void StopCar()
    {
        foreach (Wheel col in Wheels.instance.WheelList)
        {
            col.wheelSetting.WC.brakeTorque = brakePower * 2;
        }
    }

    public void DontStopCar()
    {
        foreach (Wheel col in Wheels.instance.WheelList)
        {
            col.wheelSetting.WC.brakeTorque = 0;
        }
    }
    #endregion


    public void SetParentToNull()
    {
        transform.SetParent(null);
    }

    private void OnGameOver()
    {
        foreach (Wheel col in Wheels.instance.WheelList)
        {
            col.wheelSetting.WC.brakeTorque = brakePower * 10;
        }

        // this.enabled = false;
    }
    private void OnLevelPass()
    {
        foreach (Wheel col in Wheels.instance.WheelList)
        {
            col.wheelSetting.WC.brakeTorque = brakePower * 5;
        }
    }

    public void SetMainMenuBehaivours()
    {
        StopAllCoroutines();
        GetComponent<CarCollider>().enabled = false;
        GetComponent<CarAudio>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        steeringWheelPhysical.enabled = false;
        this.enabled = false;
    }

}

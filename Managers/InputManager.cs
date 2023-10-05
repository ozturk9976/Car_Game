using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{

    public float inputSense;
    public float horizontal;
    public float vertical;
    public bool brake;


    public bool isBackwards;

    [Header("Choosen Control Type")]
    public string currentControls;

    [HideInInspector] public Vector2 _movementVector;
    private PlayerInput _playerInput;
    float _smoothInput;
    float _smoothVector;
    float _clampedValue;
    public static InputManager API;

    private void Awake()
    {
        API = this;
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Gameplay.Enable();

        EventManager.AddHandler(GameEvent.OnGameOver, ResetAllAxis);
        EventManager.AddHandler(GameEvent.OnLevelPass, ResetAllAxis);
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnLevelPass, OnLevelPass);
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Disable();

        EventManager.RemoveHandler(GameEvent.OnGameOver, ResetAllAxis);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, ResetAllAxis);
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, OnLevelPass);
    }


    public void InitializeInputData()
    {
        currentControls = PlayerPrefs.GetString("WhichControls", "CarUI_1");
        CarUIContainer.instance.ManageDeviceSensors(currentControls);
        inputSense = PlayerPrefs.GetFloat("InputSenseValue", inputSense);
    }

    private void Update()
    {

        _movementVector = _playerInput.Gameplay.Move.ReadValue<Vector2>();

        _smoothInput = Mathf.Lerp(_smoothInput, _movementVector.x, inputSense * Time.deltaTime);
        _smoothVector = Mathf.Lerp(_smoothVector, _movementVector.x, (inputSense * 2) * Time.deltaTime);
        if

        (isBackwards)
        {
            _movementVector.y = _movementVector.y * -1;
        }

        switch (currentControls)
        {
            #region Button Controls

            case "CarUI_1":

                CarController.instance.Steering = _smoothVector;
                SteeringWheelPhysical.instance.TurnTheWheel(_movementVector.x * 60);
                break;
            #endregion

            #region Wheel Controls
            case "CarUI_2":
                //CarController.instance.Steering = SimpleInput.GetAxis("Horizontal");
                //SteeringWheelPhysical.instance.TurnTheWheel(SimpleInput.GetAxis("Horizontal") * 60);
                break;
            #endregion

            #region Gyro Controls
            case "CarUI_3":
                float _gyroInput = _playerInput.Gameplay.GyroRotation.ReadValue<Vector3>().x * 5;
                _clampedValue = Mathf.Lerp(Mathf.Clamp(_clampedValue, -1, 1), _gyroInput, inputSense * 2 * Time.deltaTime);
                CarController.instance.Steering = _clampedValue;
                SteeringWheelPhysical.instance.TurnTheWheel(_movementVector.x * 60);
                break;
                #endregion
        }
    }

    public void ResetAllAxis()
    {
        if (horizontal != 0 || vertical != 0 /*|| isTouchingButton != 0*/)
        {
            horizontal = 0;
            vertical = 0;
        }

        brake = true;
    }

    private void OnLevelPass()
    {
    }
    private void OnGameOver()
    {
    }

}

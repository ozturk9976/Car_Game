using UnityEngine;
using System.Collections;

public class SteeringWheelPhysical : MonoBehaviour
{

    [SerializeField] float _defaultRotationX;
    [SerializeField] float turnBackSmoothing;

    private float _targetRotation;

    public static SteeringWheelPhysical instance;

    private void Awake()
    {
        instance = this;
    }
    public void TurnTheWheel(float targetRot)
    {
        _targetRotation = Mathf.Lerp(_targetRotation, targetRot, (InputManager.API.inputSense * 5f) * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(_defaultRotationX, 180, _targetRotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class LaserRotate : MonoBehaviour
{
    [Header("Rotation Degree")]
    [SerializeField] Vector3 rotationVector;

    [Header("Rotation Speed")]
    [SerializeField] float speed;

    [SerializeField] GameObject laserBody;

    private void Start()
    {
        var _startVector = laserBody.transform.localEulerAngles;
        laserBody.transform.localRotation = Quaternion.Euler(_startVector - rotationVector);
        laserBody.transform.DOLocalRotateQuaternion(Quaternion.Euler(_startVector + rotationVector), speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}

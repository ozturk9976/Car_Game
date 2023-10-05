using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Main_Menu_Camera : MonoBehaviour
{
    [Header("Pivots")]
    [SerializeField] Transform garage_Camera_Pivot;
    [SerializeField] Transform main_Pivot;
    [Space(20)]
    private Transform camera_transform;
    public GameObject garageDoor;
    public static Main_Menu_Camera instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        camera_transform = Camera.main.transform;
    }
    public void GoTo_Garage_Pivot()
    {
        camera_transform.transform.DOMove(garage_Camera_Pivot.position, 1f).SetEase(Ease.OutQuad).OnComplete(() => { OpenGarageDoor(); });
        camera_transform.transform.DORotateQuaternion(garage_Camera_Pivot.transform.rotation, 1f).SetEase(Ease.OutQuad);
    }
    public void GoTo_Main_Pivot()
    {
        CloseGarageDoor();
        camera_transform.transform.DOMove(main_Pivot.position, 1f).SetEase(Ease.OutQuad);
        camera_transform.transform.DORotateQuaternion(main_Pivot.transform.rotation, 1f).SetEase(Ease.OutQuad);
    }
    void OpenGarageDoor()
    {
        if (garageDoor != null)
        {
            garageDoor.transform.DOMoveY(3.6f, 0.8f).SetEase(Ease.InSine);
        }
    }
    void CloseGarageDoor()
    {
        if (garageDoor != null)
        {
            garageDoor.transform.DOMoveY(1.2f, 0.8f).SetEase(Ease.InSine);
        }
    }
}

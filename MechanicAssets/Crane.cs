using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crane : MonoBehaviour, IMechanic
{
    [Header("asansör şuan kaçıncı katta duruyor?")]
    public SYS.Floor currentFloor;
    [Header("Kaç derece hareket edilecek?")]
    public SYS.Degree degree;
    [Header("gidilecek konum kaçıncı katta?")]
    public SYS.Floor targetFloor;

    [Header("Hareket edecek obje")]
    public GameObject moveObject;
    [Header("360 dönecek olan parça")]
    public GameObject moveObject2;
    [Header("harekete başlamadan önce kaç saniye bekleyecek")]
    public float waitTime;
    [Header("Hız (değer ne kadar düşükse o kadar hızlıhareket)")]
    public float speed;

    [Header("Asansör Sesi")]
    [SerializeField] private AudioSource craneAudioSFX;

    private float rotateDegree;
    private float floorLevel;



    [SerializeField] private PlatformHelper platformHelper;

    void Start()
    {

        switch (targetFloor)
        {
            case SYS.Floor.plane:
                floorLevel = 2.47f;
                break;
            case SYS.Floor.first_floor:
                floorLevel = 4.9f;
                break;
            case SYS.Floor.second_floor:
                floorLevel = 7.6f;
                break;

        }
        switch (degree)
        {
            case SYS.Degree.Degree_0:
                rotateDegree = 0;
                break;
            case SYS.Degree.Degree_right_45:
                rotateDegree = 45;
                break;
            case SYS.Degree.Degree_right_90:
                rotateDegree = 90;
                break;
            case SYS.Degree.Degree_right_135:
                rotateDegree = 135;
                break;
            case SYS.Degree.Degree_right_180:
                rotateDegree = 180;
                break;
            case SYS.Degree.Degree_left_135:
                rotateDegree = 225;
                break;
            case SYS.Degree.Degree_left_90:
                rotateDegree = 270;
                break;
            case SYS.Degree.Degree_left_45:
                rotateDegree = 315;
                break;
        }
    }


    private void GoUp()
    {
        // varspeed = speed * (rotateDegree / 100);
        if (targetFloor != SYS.Floor.plane)
        {
            craneAudioSFX.Play();
            var _sequence = DOTween.Sequence();
            _sequence.Append(moveObject.transform.DOLocalMoveY(floorLevel, speed));
            _sequence.Append(moveObject2.transform.DOLocalRotate(new Vector3(0, 180, 0), speed));
            _sequence.Append(moveObject.transform.DOLocalRotate(new Vector3(0, rotateDegree, 0), speed * 3).SetEase((Ease.Linear))
            .OnComplete(() =>
                {
                    if (craneAudioSFX.isPlaying)
                    {
                        craneAudioSFX.Stop();
                    }
                    EventManager.Broadcast(GameEvent.OnPlatformOut);
                    platformHelper.CloseThis();
                }));
        }
        else
        {
            craneAudioSFX.Play();
            var _sequence = DOTween.Sequence();
            _sequence.Append(moveObject.transform.DOLocalRotate(new Vector3(0, rotateDegree, 0), speed * 3)).SetEase(Ease.Linear);
            _sequence.Append(moveObject2.transform.DOLocalRotate(new Vector3(0, 180, 0), speed));
            _sequence.Append(moveObject.transform.DOLocalMoveY(floorLevel, speed))
            .OnComplete(() =>
                 {
                     if (craneAudioSFX.isPlaying)
                     {
                         craneAudioSFX.Stop();
                     }
                     EventManager.Broadcast(GameEvent.OnPlatformOut);
                     platformHelper.CloseThis();
                 });


        }


    }

    IEnumerator DoMove()
    {
        yield return new WaitForSeconds(waitTime);
        GoUp();
    }

    public void IDoMove()
    {
        StartCoroutine(DoMove());
    }
    public void ISetCarAsChild(Transform carTransform)
    {
        carTransform.transform.SetParent(moveObject2.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    [SerializeField] private AnimationCurve engineSoundCurve;
    [SerializeField] private AudioSource carEngineSound;
    [SerializeField] private AudioSource carStartupSound;

    [SerializeField] private float minVolume;
    [SerializeField] private float minPitch;

    [SerializeField] private float soundLerpSpeed;
    private float _targetValue;
    private float _targetInputValue;
    private float _value;

    float clipsValue;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);

    }
    private void OnDisable()
    {

        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void Start()
    {
        carEngineSound.PlayDelayed(0.2f);

    }
    private void Update()
    {
        EngineSoundEffect(carEngineSound);
    }
    public void EngineSoundEffect(AudioSource sound)
    {
        _targetInputValue = minPitch + CarController.instance.Get_Speed / 35;
        clipsValue = engineSoundCurve.Evaluate(_targetInputValue);
        sound.pitch = Mathf.Clamp(clipsValue, minPitch, 1.2f);
        sound.volume = Mathf.Clamp(clipsValue, minVolume, 1);
    }

    void OnGameOver()
    {
        // DOTween.To(() => carEngineSound.pitch, x => carEngineSound.pitch = x, 0, 0.7f).SetEase(Ease.OutQuart);
        // DOTween.To(() => carEngineSound.volume, x => carEngineSound.pitch = x, 0, 0.7f).SetEase(Ease.OutQuart);
        // this.enabled = false;
    }
}

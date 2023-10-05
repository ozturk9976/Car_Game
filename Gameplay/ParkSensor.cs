using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class ParkSensor : MonoBehaviour
{
    [SerializeField] private GameObject levelPassParticle;
    [SerializeField] private Transform particleTransform;
    [SerializeField] private UnityEvent levelPassEvent;

    int wheelCollisionCount;
    private bool isGameOver;
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wheel" && !isGameOver)
        {
            //Being sure that only one wheel collider used for passing level
            wheelCollisionCount++;
            if (wheelCollisionCount == 1)
            {
                levelPassEvent.Invoke();
                EventManager.Broadcast(GameEvent.OnLevelPass);
                StarManager.instance.ManageStars();
                DOTween.KillAll();
                this.enabled = false;
            }
        }
    }

    private void OnGameOver()
    {
        isGameOver = true;
    }
}

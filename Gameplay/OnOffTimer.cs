using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffTimer : MonoBehaviour
{
    [SerializeField] GameObject objectToTurnOnOff;
    [SerializeField] float waitTime;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnLevelPass, OnGameOver);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, OnGameOver);
    }
    private void Start()
    {
        StartCoroutine(Lamp());
    }
    private IEnumerator Lamp()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            objectToTurnOnOff.SetActive(false);
            yield return new WaitForSeconds(waitTime);
            objectToTurnOnOff.SetActive(true);
        }
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
        this.enabled = false;
    }
}

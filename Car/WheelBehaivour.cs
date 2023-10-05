using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehaivour : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnLevelPass, OnLevelPass);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, OnLevelPass);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Mine"))
        {
            other.GetComponent<Explosion>().Boom(0.3f);
            this.gameObject.SetActive(false);
            // Destroy(this.gameObject, 0.31f);
            // this.transform.GetChild(0).gameObject.SetActive(false);
            EventManager.Broadcast(GameEvent.OnGameOver);
        }


    }
    private void OnGameOver()
    {
    }

    private void OnLevelPass()
    {
    }
}

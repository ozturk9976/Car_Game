using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInvoker : MonoBehaviour
{
    [SerializeField] private GameEvent[] _eventToInvoke;
    public void InvokeEvents()
    {
        foreach (var item in _eventToInvoke)
        {
            EventManager.Broadcast(item);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventClass
{


}
public enum GameEvent
{
    //Game status
    OnGameOver,
    OnGamePause,
    OffGamePause,
    OnPlayerSpawn,
    OnLevelPass,
    OnThemePass,
    OnCameraCantMove,
    OnCameraCanMove,
    OnCameraZoom,
    OffCameraZoom,

    //Lifting pack etc events
    OnPlatformIn,
    OnPlatformOut,

    //Brakes
    OnBrake,
    OnBrake_UP,

    //UI Events
    OnAdjustMode,
    OnNotAdjustMode
}
public static class EventManager
{
    private static Dictionary<GameEvent, Action> eventTable = new Dictionary<GameEvent, Action>();

    public static void AddHandler(GameEvent gameEvent, Action action)
    {
        if (!eventTable.ContainsKey(gameEvent))
        {
            eventTable[gameEvent] = action;
        }
        else
        {
            eventTable[gameEvent] += action;
        }
    }


    public static void RemoveHandler(GameEvent gameEvent, Action action)
    {
        if (eventTable[gameEvent] != null)
        {
            eventTable[gameEvent] -= action;
        }
        if (eventTable[gameEvent] == null)
        {
            eventTable.Remove(gameEvent);
        }
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if (eventTable[gameEvent] != null)
        {
            eventTable[gameEvent]();
        }
    }
}

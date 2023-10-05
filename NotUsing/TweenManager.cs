using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TweenManager : MonoBehaviour
{
    private Dictionary<string, Sequence> LiveTweens = new Dictionary<String, Sequence>();


    public void AddNewTween(string objectName, Sequence sequence)
    {
        LiveTweens.Add(objectName, sequence);
    }

    public void DestroyTween(string objectName)
    {
        DOTween.Kill(LiveTweens[objectName]);
        LiveTweens.Remove(objectName);
    }

    public void DestroyAllTweens()
    {
        DOTween.KillAll();
    }
}

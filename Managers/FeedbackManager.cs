using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField] private MMFeedbacks crashFeedback;

    [SerializeField] private MMFeedbacks paintFeedback;

    [SerializeField] private MMFeedbacks gameoverFeedback;

    [SerializeField] private MMFeedbacks gameoverExplosionFeedback;

    [SerializeField] private MMFeedbacks levelpassFeedback;

    [SerializeField] private MMFeedbacks pausegameFeedback;

    [SerializeField] private MMFeedbacks resumegameFeedback;






    public static FeedbackManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void Play_crashFeedback()
    {
        crashFeedback?.PlayFeedbacks();
    }

    public void Play_paintFeedback()
    {
        paintFeedback?.PlayFeedbacks();
    }

    public void Play_gameoverFeedback()
    {
        gameoverFeedback?.PlayFeedbacks();
    }

    public void Play_levelpassFeedback()
    {
        levelpassFeedback?.PlayFeedbacks();
    }

    public void Play_gameoverExplosionFeedback()
    {
        gameoverExplosionFeedback?.PlayFeedbacks();
    }

    public void Play_pausegameFeedback()
    {
        pausegameFeedback?.PlayFeedbacks();
    }

    public void Play_resumegameFeedback()
    {
        resumegameFeedback?.PlayFeedbacks();
    }

}

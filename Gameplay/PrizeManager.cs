using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PrizeManager : MonoBehaviour
{

    [SerializeField] private GameObject[] allPrizes;

    public void SetDailyPrize(string nameOfPrize)
    {
        for (int i = 0; i < allPrizes.Length; i++)
        {
            if (allPrizes[i].GetComponent<PrizeButtonBehaivour>().dateTimePrize.name == nameOfPrize)
            {
                allPrizes[i].GetComponent<PrizeButtonBehaivour>().UnlockPrize();
            }
        }
    }
}

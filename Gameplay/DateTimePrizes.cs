using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PrizeData", menuName = "ScriptableObjects/ScriptablePrize", order = 1)]
public class DateTimePrizes : ScriptableObject
{
    public Sprite imageOfPrize;
    public int starCount_Prize;
}

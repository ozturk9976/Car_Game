using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CountTime : MonoBehaviour
{
    public float waitTime;
    private float showWaitTime;
    public Image fillAmountObj;
    public TextMeshProUGUI textTo;


    private void Start()
    {
        this.transform.DOPunchRotation(new Vector3(2f, 2f, 2f), 0.2f);
    }
    private void Awake()
    {
        StartCoroutine(WaitAndDoSomething());
    }
    void Update()
    {
        int showTextInt = (int)(fillAmountObj.fillAmount * waitTime) + 1;
        textTo.text = showTextInt.ToString();
        fillAmountObj.fillAmount -= 1f / waitTime * Time.deltaTime;
    }
    IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(waitTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonBehaivour : MonoBehaviour
{

    [SerializeField] UnityEvent buttonClickEvent;
    private Button _thisButton;

    private void OnEnable()
    {
        _thisButton = this.GetComponent<Button>();
    }

    public void OnClick()
    {
        _thisButton.interactable = false;
        AudioManager.instance.PlayButton1_Sfx();
        var _sequence = DOTween.Sequence();
        _sequence.SetUpdate(true);
        _sequence.Append(GetComponent<Button>().transform.DOScale(1.15f, 0.1f));
        _sequence.Append(GetComponent<Button>().transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f)
        .OnComplete(() => { buttonClickEvent.Invoke(); _thisButton.interactable = true; }));

    }
}

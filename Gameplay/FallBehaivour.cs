using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FallBehaivour : MonoBehaviour
{

    [SerializeField] private float _forcePower;
    [SerializeField] private float _timer;
    [SerializeField] private GameObject _targetTransorm;
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _targetStartPos;
    private Vector3 _targetStartRot;
    private bool canForce;
    private void Start()
    {
        _targetStartPos = _targetTransorm.transform.position;
        _targetStartRot = _targetTransorm.transform.eulerAngles;
        StartCoroutine(StartForce());

    }

    private void Update()
    {
        _targetTransorm.gameObject.transform.position = _targetStartPos;
        _targetTransorm.gameObject.transform.eulerAngles = _targetStartRot;
    }
    private void FixedUpdate()
    {

        _rigidbody.AddForce(
            new Vector3(_targetTransorm.transform.position.x - transform.position.x, 0, _targetTransorm.transform.position.z - transform.position.z).normalized
            * _forcePower);
    }
    private IEnumerator StartForce()
    {
        yield return new WaitForSeconds(_timer);
        // GameManager.instance.ChangeTagOfGameobject(this.gameObject, "Untagged");
        this.enabled = false;
    }
}

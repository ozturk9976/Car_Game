using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> boxPresets = new List<GameObject>();
    void Start()
    {
        int _preset = Random.Range(0, boxPresets.Count);
        GameObject _tempBoxes = Instantiate(boxPresets[_preset], this.transform.position, transform.rotation, transform);
    }
}

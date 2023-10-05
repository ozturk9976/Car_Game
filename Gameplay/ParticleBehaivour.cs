using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaivour : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = Color.red;
    }

}

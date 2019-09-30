using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrail : MonoBehaviour
{
    public GameObject trail;
    void Start()
    {
        print("Instanitated particle");
        Instantiate(trail,gameObject.transform);
    }
}

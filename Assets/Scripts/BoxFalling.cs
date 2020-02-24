using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFalling : MonoBehaviour
{
    private bool Fallen = false;
    AudioSource FallNoise;
    private int Checked = 1;

    void Start()
    {
        FallNoise = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Fallen && Checked ==1)
        {
            Checked = Checked + 1;
            Fallen = false;
            FallNoise.Play();
        }        
    }
    void OnCollisionExit(Collision collision)
    {
        Fallen = true;
    }
}

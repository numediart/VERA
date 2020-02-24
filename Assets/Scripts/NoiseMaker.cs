using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    float Timer = 0f;
    AudioSource fridgeNoise;

    public float turnontime ;
    public float StimuliDuration ;

    void Start()
    {
        fridgeNoise = GetComponent<AudioSource>();
    }
    void Update()
    {
        Timer = Timer + Time.deltaTime;
        if(Timer > turnontime)
        {
            fridgeNoise.Play();
        }

        if(Timer > turnontime + StimuliDuration)
        {
            Timer = 0;
            fridgeNoise.Stop();
        }
        
    }
}

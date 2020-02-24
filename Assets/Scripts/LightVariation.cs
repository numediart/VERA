using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVariation : MonoBehaviour
{
    Light m_light;

    float timer;
    public float vel = 5f;
    void Start()
    {
        m_light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        m_light.intensity = 1.5f + Mathf.Sin(timer * vel);
    }
}

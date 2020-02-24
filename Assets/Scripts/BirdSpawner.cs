using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject Robin;
    public GameObject Cardinal;
    public float Spawning_time = 10f;
    
    private float Timer;
    private float x_center;
    private float y_center;

    void Start()
    {
        x_center = GetComponent<Transform>().position.x;
        y_center = GetComponent<Transform>().position.x;

    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer + Time.deltaTime;
        
        if(Timer > Spawning_time)
        {
            Timer = 0;
            float x_spawn = Random.Range(x_center - 50, x_center + 50);
            float y_spawn = y_center + (Random.Range(0, 2) * 2 - 1) * Mathf.Sqrt(50 * 50 - (x_spawn - x_center) * (x_spawn - x_center));
            float rand = Random.Range(-1f, 1f);

            if(rand > 0)
            {
                Instantiate(Robin, new Vector3(x_spawn, 20f, y_spawn), Quaternion.identity);
            }
            else
            {
                Instantiate(Cardinal, new Vector3(x_spawn, 20f, y_spawn), Quaternion.identity);
            }
        }
    }
}

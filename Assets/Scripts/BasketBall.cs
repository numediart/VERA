using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    private float Timer = 0;
    AudioSource BounceSong;
    Rigidbody BallRgb;

    public float time_moving;
    bool Collide = false;
    
    // Start is called before the first frame update
    void Start()
    {
        BounceSong = GetComponent<AudioSource>();
        BallRgb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Timer = Timer + Time.deltaTime;
        if (Timer>time_moving)
        {
            BallRgb.AddForce(new Vector3(-1f, 0, 0));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Collide = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if (Collide)
        {
            Collide = false;
            BounceSong.Play(0);
        }
    }
}

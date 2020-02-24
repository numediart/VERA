using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlying : MonoBehaviour
{

    public bool isFlying = true;

    Rigidbody balloonRgb;
    // Start is called before the first frame update
    void Start()
    {
        balloonRgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            balloonRgb.AddForce(new Vector3(0.75f, 0.1f, 0));
            isFlying = false;
        }
    }
}

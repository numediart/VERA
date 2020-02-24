using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMvt : MonoBehaviour
{

    public bool Detached;

    Rigidbody ballonRgb;
    // Start is called before the first frame update
    void Start()
    {
        ballonRgb = GetComponent<Rigidbody>();
        ballonRgb.AddForce(0.5f,0.25f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

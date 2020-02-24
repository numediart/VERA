using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMooving : MonoBehaviour
{
    public float velocity = 3f;

    Rigidbody Dog_rgb;
    // Start is called before the first frame update
    void Start()
    {
        Dog_rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Dog_rgb.velocity = new Vector3(velocity, 0, 0);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMvt : MonoBehaviour
{
    public Rigidbody RigidBodyCar; //rigidbody linked to the car to induce the mvt
    public float CarVelocity = 10f; //speed of the car along the road

    public float VMin = 7.5f; //minimum velocity of the car
    public float VMax = 20f; //minimum velocity of the car

    // Start is called before the first frame update
    void Start()
    {
        RigidBodyCar = GetComponent<Rigidbody>(); // get the car rigidbody componenet
        CarVelocity = Random.Range(VMin, VMax); //car velocity between two value 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RigidBodyCar.velocity = new Vector3(0, 0, Mathf.Cos(Mathf.PI * RigidBodyCar.rotation.y)*CarVelocity); //make the car moving at the given velocity
    }
}

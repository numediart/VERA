using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrouselTurning : MonoBehaviour
{
    Rigidbody CarrouselRgb;

    public float TurningVelocity = 10f;
    public AudioSource CarrouselSound;

    private bool isTurning = true;
    void Start()
    {
        CarrouselRgb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (isTurning)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, TurningVelocity, 0) * Time.deltaTime);
            CarrouselRgb.MoveRotation(CarrouselRgb.rotation * deltaRotation);
            //CarrouselSound.Play();
        }
    }
}

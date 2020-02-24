using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : MonoBehaviour
{
    public Rigidbody boxRgb;
    private float PushForce = 150f;
    // Start is called before the first frame update
    void Start()
    {
        boxRgb.AddForce(PushForce, 0, 0);
    }
}

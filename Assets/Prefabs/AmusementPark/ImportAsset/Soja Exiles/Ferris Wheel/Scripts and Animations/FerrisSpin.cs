using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisSpin : MonoBehaviour {
    public float speed = 10f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            transform.Rotate( new Vector3(0, 0, -1f), speed * Time.deltaTime);
    }
}

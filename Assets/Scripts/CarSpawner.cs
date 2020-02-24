using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner: MonoBehaviour
{
    public GameObject[] Cars; //array with all the available cars
    public Transform[] Apparition_Coord; //apparition point of in the two direction


    public float ApparitionFreq = 4f;
    private float Timer; //timer to record the time passing
    private int id = 0;
    private int car_id = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        id = Random.Range(0, Apparition_Coord.Length);
        car_id = Random.Range(0, Cars.Length);

        Timer = Timer + Time.deltaTime;
        if (Timer > ApparitionFreq)
        {
            Timer = 0;
          
            GameObject Car_tmp = Instantiate(Cars[car_id], Apparition_Coord[id].position, Apparition_Coord[id].rotation);
            Car_tmp.name = "car_tmp";
        }
        
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "car_tmp")
        {
            Destroy(other.gameObject);

        }
    }
}

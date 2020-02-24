using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestAnimalsSpawner : MonoBehaviour
{

    private float x_center;
    private float y_center;
    private float x_spawn;
    private float y_spawn;
    private float tan_angle;
    private float height;

    public float squirrelSpawnDist;
    public float ButterSpawnDist;
    public float SqcrossingDist;
    public float BfcrossingDist;

    public GameObject SquirrelGO;

    public GameObject CrowGO;

    public GameObject ButterGO;

    private float Time_counter = 0f;

    public Transform[] flightPoints;

    // Start is called before the first frame update
    void Start()
    {
        x_center = GetComponent<Transform>().position.x;
        y_center = GetComponent<Transform>().position.z;
        height = GetComponent<Transform>().position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Time_counter = Time_counter + Time.deltaTime;

        //Squirrel
        x_spawn = Random.Range(x_center - squirrelSpawnDist, x_center + squirrelSpawnDist);
        y_spawn = y_center + (Random.Range(0, 2) * 2 - 1) * Mathf.Sqrt(squirrelSpawnDist * squirrelSpawnDist - (x_spawn - x_center) * (x_spawn - x_center));

        if (Time_counter > 10)
        {
            //Time_counter = 0;
            //Debug.Log("Spawning Squirrel");
            GameObject Clone = Instantiate(SquirrelGO, new Vector3(x_spawn, 0.5f, y_spawn), Quaternion.identity);
            Destroy(Clone, 15);
        }

        //Crow
        if (Time_counter > 10)
        {
            //Time_counter = 0;
            //Debug.Log("Spawning Crow");
            int id = Random.Range(0, flightPoints.Length);
            GameObject Clone = Instantiate(CrowGO, flightPoints[id].position, Quaternion.Euler(flightPoints[id].localEulerAngles));
            Destroy(Clone, 10);
        }

        //Butter
        x_spawn = Random.Range(x_center - ButterSpawnDist, x_center + ButterSpawnDist);
        y_spawn = y_center + (Random.Range(0, 2) * 2 - 1) * Mathf.Sqrt(ButterSpawnDist * ButterSpawnDist - (x_spawn - x_center) * (x_spawn - x_center));

        if (Time_counter > 10)
        {
            Time_counter = 0;
            //Debug.Log("Spawning Butterfly");
            GameObject Clone = Instantiate(ButterGO, new Vector3(x_spawn, Random.Range(0.85f, 1.95f), y_spawn), Quaternion.identity);
            Destroy(Clone, 15);
        }
    }
}

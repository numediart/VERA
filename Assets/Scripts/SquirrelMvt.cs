using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelMvt : MonoBehaviour
{
    public float velocity = 5f;

    private float x_center;
    private float y_center;
    private float x_spawn;
    private float y_spawn;
    private float tan_angle;
    private Vector2 Squirrel_dir;
    private float orient_squirrel;

    public Rigidbody SquirrelRgb;
    private bool CanMoove = false;

    public float squirrelSpawnDist;
    public float crossingDist;

    public ForestAnimalsSpawner script_agent;
    public GameObject Agent_GameObject;
    // Start is called before the first frame update
    void Start()
    {
        script_agent = Agent_GameObject.GetComponent<ForestAnimalsSpawner>();
        SquirrelRgb =  GetComponent<Rigidbody>();
        x_center = Agent_GameObject.transform.position.x;
        y_center = Agent_GameObject.transform.position.z;
        x_spawn = SquirrelRgb.position.x;
        y_spawn = SquirrelRgb.position.z;
        //        squirrelSpawnDist = script_agent.squirrelSpawnDist;
        squirrelSpawnDist = 40f;
        // crossingDist = script_agent.SqcrossingDist;
        crossingDist = 2f;
        tan_angle = Mathf.Asin(crossingDist / squirrelSpawnDist);
        Squirrel_dir = new Vector2(x_center - x_spawn, y_center - y_spawn); //vector coming from the spawn point to the center 
        //we need to induce a deviation to the squirrel (in order not to rush on the player)
        tan_angle = tan_angle * (Random.Range(0, 2) * 2 - 1);
        Squirrel_dir.x = Mathf.Cos(tan_angle) * Squirrel_dir.x - Mathf.Sin(tan_angle) * Squirrel_dir.y;
        Squirrel_dir.y = Mathf.Sin(tan_angle) * Squirrel_dir.x + Mathf.Cos(tan_angle) * Squirrel_dir.y;
        Squirrel_dir = velocity * (Squirrel_dir / Squirrel_dir.magnitude);
        if(Squirrel_dir.y > 0)
        {
            orient_squirrel = Mathf.Atan(Squirrel_dir.x / Squirrel_dir.y) * Mathf.Rad2Deg;
            }
        else
        {
            orient_squirrel = Mathf.Atan(Squirrel_dir.x / Squirrel_dir.y) * Mathf.Rad2Deg + 180f;
        }
        SquirrelRgb.transform.rotation = Quaternion.Euler(new Vector3(0, orient_squirrel, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMoove)
        {
            SquirrelRgb.velocity = new Vector3(Squirrel_dir.x, 0, Squirrel_dir.y); //make the car movi
        }
        SquirrelRgb.transform.rotation = Quaternion.Euler(new Vector3(0, orient_squirrel, 0));
    }

    void OnCollisionEnter(Collision collision) //if the squirrel touch the ground
    {
        CanMoove = true;        
    }
}

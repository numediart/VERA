using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyMvt : MonoBehaviour
{
    public Rigidbody ButterRgb;
    public float velocity = 1.5f;

    private float x_center;
    private float y_center;
    private float x_spawn;
    private float y_spawn;
    private float tan_angle;
    private Vector2 Butter_dir;
    private float orient_butter;

    public float ButterSpawnDist;
    public float crossingDist;

    public ForestAnimalsSpawner script_agent;
    public GameObject Agent_GameObject;

    // Start is called before the first frame update
    void Start()
    {
        script_agent = Agent_GameObject.GetComponent<ForestAnimalsSpawner>();
        ButterRgb = GetComponent<Rigidbody>();
        x_center = Agent_GameObject.transform.position.x;
        y_center = Agent_GameObject.transform.position.z;
        x_spawn = ButterRgb.position.x;
        y_spawn = ButterRgb.position.z;
        ButterSpawnDist = script_agent.squirrelSpawnDist;
        crossingDist = script_agent.SqcrossingDist;
        tan_angle = Mathf.Asin(crossingDist / ButterSpawnDist);
        Butter_dir = new Vector2(x_center - x_spawn, y_center - y_spawn); //vector coming from the spawn point to the center 
        //we need to induce a deviation to the squirrel (in order not to rush on the player)
        tan_angle = tan_angle * (Random.Range(0, 2) * 2 - 1);
        Butter_dir.x = Mathf.Cos(tan_angle) * Butter_dir.x - Mathf.Sin(tan_angle) * Butter_dir.y;
        Butter_dir.y = Mathf.Sin(tan_angle) * Butter_dir.x + Mathf.Cos(tan_angle) * Butter_dir.y;
        Butter_dir = velocity * (Butter_dir / Butter_dir.magnitude);
        ButterRgb.velocity = new Vector3(Butter_dir.x, 0, Butter_dir.y);
        if (Butter_dir.y > 0)
        {
            orient_butter = Mathf.Atan(Butter_dir.x / Butter_dir.y) * Mathf.Rad2Deg;
        }
        else
        {
            orient_butter = Mathf.Atan(Butter_dir.x / Butter_dir.y) * Mathf.Rad2Deg + 180f;
        }
        ButterRgb.transform.rotation = Quaternion.Euler(new Vector3(0, orient_butter, 0));
    }
}

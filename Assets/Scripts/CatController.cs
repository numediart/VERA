using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    Rigidbody CatRgb;

    public Transform ReachPoint;
    public float ForceIndex = 0.025f;

    private float Target_distance;
    private bool isMooving = true;

    private float x_final;
    private float z_final;

    public BirthdayAgentSpawner script_Agent;
    public GameObject Agent_GameObject;

    void Start()
    {
        CatRgb = GetComponent<Rigidbody>();

        script_Agent = Agent_GameObject.GetComponent<BirthdayAgentSpawner>();

        ReachPoint = script_Agent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Target_distance = Vector3.Distance(ReachPoint.position, CatRgb.position);
        if(isMooving)
        { 
            if (Target_distance > 1f) //if the distance to the target is superior to 1, let's moove
            {
                Vector3 MvtVector = ReachPoint.position - CatRgb.position; 
                CatRgb.transform.position = Vector3.MoveTowards(CatRgb.transform.position, ReachPoint.position, ForceIndex);
                //CatRgb.transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Sign(MvtVector.z) * Mathf.Atan(MvtVector.x / MvtVector.z) * Mathf.Rad2Deg, 0));
                CatRgb.transform.rotation = Quaternion.LookRotation(MvtVector);
            }
            else
            {
                isMooving = false;
                x_final = Random.Range(0f, -160f);
                z_final = Random.Range(0f, -160f);
                Destroy(this.gameObject, 20f);
            }
        }
        else
        {
            Vector3 MvtVector = new Vector3(x_final, CatRgb.position.y,z_final) - CatRgb.position;
            CatRgb.transform.position = Vector3.MoveTowards(CatRgb.transform.position, new Vector3(x_final, CatRgb.position.y, z_final), ForceIndex);
            CatRgb.transform.rotation = Quaternion.LookRotation(MvtVector);
            //CatRgb.transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Sign(MvtVector.x) * Mathf.Atan(MvtVector.x / MvtVector.z) * Mathf.Rad2Deg, 0));
        }

    }
}

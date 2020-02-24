using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    Animator Bird_Animator;
    AudioSource Bird_Song;

    Rigidbody BirdRigidBody;
    public float ForceIndex = 150f;
    public float SingDuration = 10f;

    private float x_center;
    private float y_center;
    private float z_center;
    private float x_spawn;
    private float y_spawn;
    private float z_spawn;
    private Vector3 Bird_dir;


    public BirdSpawner script_agent;
    public GameObject Agent_GameObject;

    private bool OnStationnary = false;
    private bool ToTarget = false;
    private float Timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Bird_Animator = GetComponent<Animator>();
        Bird_Song = GetComponent<AudioSource>();

        script_agent = Agent_GameObject.GetComponent<BirdSpawner>();
        BirdRigidBody = GetComponent<Rigidbody>();
        x_center = Agent_GameObject.transform.position.x;
        y_center = Agent_GameObject.transform.position.y;
        z_center = Agent_GameObject.transform.position.z;

        x_spawn = BirdRigidBody.position.x;
        y_spawn = BirdRigidBody.position.y;
        z_spawn = BirdRigidBody.position.z;

        Bird_dir = new Vector3(x_center - x_spawn, y_center - y_spawn, z_center - z_spawn);
        Bird_dir = ForceIndex * (Bird_dir / Bird_dir.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer + Time.deltaTime;

        BirdRigidBody.velocity = Bird_dir;
        x_spawn = BirdRigidBody.position.x;
        y_spawn = BirdRigidBody.position.y;
        z_spawn = BirdRigidBody.position.z;

        
        if (!OnStationnary ||ToTarget)
        {
            if(!OnStationnary)
            {
                Bird_dir = new Vector3(x_center - x_spawn, y_center - y_spawn, z_center - z_spawn);
                Bird_dir = ForceIndex * (Bird_dir / Bird_dir.magnitude);
            }
            if (ToTarget)
            {
                //Debug.Log(x_spawn + "     " + y_spawn + "      " + z_spawn+"\n"+ x_center + "     " + y_center + "      " + z_center);
                Bird_dir.y = Mathf.Lerp(y_spawn, y_center, 0.75f) - y_spawn;
                Bird_dir.x = 5 * (x_center - x_spawn);
                Bird_dir.z = 10*(z_center - z_spawn);
            }
            BirdRigidBody.AddForce(Bird_dir);
        }

        float distance_target = Mathf.Sqrt(Mathf.Pow(x_center - x_spawn, 2f) + Mathf.Pow(y_center - y_spawn, 2f) + Mathf.Pow(z_center - z_spawn, 2f));
        if (distance_target < 15f)
        {
            ForceIndex = 10 * (distance_target + 1f);
            if(distance_target < 1f && !ToTarget && x_center == Agent_GameObject.transform.position.x)
            {
                OnStationnary = true;
            }
            if(ToTarget && distance_target < 1f)
            {
                Singing();
            }
            if(distance_target < 1f)
            {
                ForceIndex = 0;
            }
        }
        if (OnStationnary)
        {
            GoToRoof();
        }

        float orient_bird;
        if (Bird_dir.z > 0)
        {
            orient_bird = Mathf.Atan(Bird_dir.x / Bird_dir.z) * Mathf.Rad2Deg;
        }
        else
        {
            orient_bird = Mathf.Atan(Bird_dir.x / Bird_dir.z) * Mathf.Rad2Deg + 180f;
        }
        BirdRigidBody.transform.rotation = Quaternion.Euler(new Vector3(0, orient_bird, 0));
    }

    void GoToRoof()
    {
        int Num_Child = Agent_GameObject.transform.childCount;
        float[] x_tmp = new float[Num_Child];
        float[] z_tmp = new float[Num_Child];
        for (int j = 0; j < Num_Child; j++)
        {
            GameObject ChildGameObject = Agent_GameObject.transform.GetChild(j).gameObject;
            x_tmp[j] = ChildGameObject.transform.position.x;
            z_tmp[j] = ChildGameObject.transform.position.z;
            y_center = ChildGameObject.transform.position.y;
        }
        x_center = Random.Range(Mathf.Min(x_tmp),Mathf.Max(x_tmp));
        z_center = Random.Range(Mathf.Min(z_tmp),Mathf.Max(z_tmp));
        ToTarget = true;
        OnStationnary = false;
    }

    void Singing()
    {
        Timer = -SingDuration;
        ForceIndex = 0;
        Bird_Animator.SetBool("Land", true);
        Bird_Song.Play();
        ToTarget = false;
        Destroy(this.gameObject, SingDuration);
    }

    void OnCollisionEnter(Collision collision)
    {
        Singing();
    }
}

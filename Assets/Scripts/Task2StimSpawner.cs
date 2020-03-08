using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class Task2StimSpawner : MonoBehaviour
{
    public GameObject RightPrefab;
    public GameObject WrongPrefab;
    
    public float Mean_spawn_time = 1f;
    private float Spawn_time;

    private float delta_time = 0.25f;
    private float x, y, z;
    private float x_target, y_target, z_target;
    private float Timer;
    private float distance;

    public float pos_tresh_time = 0.075f; //time duration to consider the object as looked or non-looked
    public float neg_tresh_time = 0.025f; //time duration to consider the object as looked or non-looked

    public float x_center;
    public float y_center;
    public float z_center;

    public float inter_x;
    public float inter_y;
    public float inter_z;

    InputSimulator sim = new InputSimulator();

    void Start()
    {
        Spawn_time = Random.Range(Mean_spawn_time - delta_time, Mean_spawn_time + delta_time);
    }
    void FixedUpdate()
    {
        Timer = Timer + Time.deltaTime;
        if(Timer > Spawn_time)
        {
            ScoreComputation.n_sphere = ScoreComputation.n_sphere + 1;
            Spawn_time = Random.Range(Mean_spawn_time - delta_time, Mean_spawn_time + delta_time);
            Timer = 0;

            RandomPosition(x_center, y_center, z_center, inter_x, inter_y, inter_z);
            distance = ComputeDistance(x, y, z, x_center, y_center, z_center);
            
            while (distance < 1.2) // update the target apparition position while it is not correct
            {
                RandomPosition(x_center, y_center, z_center, inter_x, inter_y, inter_z);
                distance = ComputeDistance(x, y, z, x_center, y_center, z_center);
            }

            x_target = x;
            y_target = y;
            z_target = z;

            TargetApparition();
            GameObject Tmp = Instantiate(RightPrefab, new Vector3(x, y, z), Quaternion.identity);
            Tmp.GetComponent<SightAnalysis>().Exp_time = Spawn_time; //time_duration of the object
            Tmp.GetComponent<SightAnalysis>().Go = true; //class assignement to go 
            Destroy(Tmp, Spawn_time-1f);

            RandomPosition(x_center, y_center, z_center, inter_x, inter_y, inter_z);
            distance = ComputeDistance(x, y, z, x_target, y_target, z_target);

            while (distance < 2.5) // update the target apparition position while it is not correct
            {
                RandomPosition(x_center, y_center, z_center, inter_x, inter_y, inter_z);
                distance = ComputeDistance(x, y, z, x_target, y_target, z_target);
            }

            PerturbApparition();
            Tmp = Instantiate(WrongPrefab, new Vector3(x, y, z), Quaternion.identity);
            Tmp.GetComponent<SightAnalysis>().Exp_time = Spawn_time; //time_duration of the object
            Tmp.GetComponent<SightAnalysis>().Go = false; //class assignement to no-go 
            Destroy(Tmp, Spawn_time-1f);
            RawRecorder.AppearTask2 = true;
            RawRecorder.x_target = x_target;
            RawRecorder.y_target = y_target;
            RawRecorder.z_target = z_target;
            RawRecorder.x = x;
            RawRecorder.y = y;
            RawRecorder.z = z;
        }
    }

    void RandomPosition(float x_center, float y_center, float z_center, float inter_x, float inter_y, float inter_z)
    {
        x = Random.Range(x_center - inter_x, x_center + inter_x);
        y = Random.Range(y_center - inter_y, y_center + inter_y);
        z = Random.Range(z_center - inter_z, z_center + inter_z);
    }

    void TargetApparition()
    {
        for (int i = 0; i < 25; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
        }
    }

    float ComputeDistance(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        float distance = 0;
        distance = Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
        return distance;
    }

    void PerturbApparition()
    {
        for (int i = 0; i < 25; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_4);
        }
    }
}

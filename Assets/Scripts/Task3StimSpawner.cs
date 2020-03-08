using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class Task3StimSpawner : MonoBehaviour
{
    public GameObject RightPrefab;

    public float Mean_spawn_time = 2.5f; //Mean Time to spawn between disappearing sphere and appearing new one
    public float Delta_Spawn_time = 1.5f; //Mean Time during which sphere is visible
    public float Tresh_ISI = 1f; //treshold between to stimulus

    public float Mean_Duration_time = 0.5f;
    public float Delta_Duration_time = 0.25f;

    private float Spawn_time;
    private float Duration_time;

    private float x, y, z;
    private float Timer;
    private bool toAppear = true;

    public float x_center;
    public float y_center;
    public float z_center;

    public float inter_x;
    public float inter_y;
    public float inter_z;

    public float pos_tresh_time = 0.3f; //time duration to consider the object as looked or non-looked
    public float neg_tresh_time = 0.1f; //time duration to consider the object as looked or non-looked

    InputSimulator sim = new InputSimulator();

    //public ScoreComputation score_comp;

    void Start()
    {
        Spawn_time = Random.Range(Mean_spawn_time - Delta_Spawn_time, Mean_spawn_time + Delta_Spawn_time);
        Duration_time = Random.Range(Mean_Duration_time - Delta_Duration_time, Mean_Duration_time + Delta_Duration_time);

    }
    void FixedUpdate()
    {
        Timer = Timer + Time.deltaTime;

        if (Spawn_time < Tresh_ISI)
        {
            Spawn_time = Tresh_ISI;
        }

        if (Timer > Spawn_time && toAppear)
        {
            toAppear = false;
            ScoreComputation.n_sphere = ScoreComputation.n_sphere + 1;

            RandomPosition(x_center, y_center, z_center, inter_x, inter_y, inter_z);
            float distance = ComputeDistance(x, y, z, x_center, y_center, z_center);

            while (distance < 1) // update the target apparition position while it is not correct
            {
                RandomPosition(x_center, y_center, z_center, inter_x, inter_y, inter_z);
                distance = ComputeDistance(x, y, z, x_center, y_center, z_center);
            }

            TargetApparition();
            GameObject Tmp = Instantiate(RightPrefab, new Vector3(x, y, z), Quaternion.identity);
            Destroy(Tmp, Duration_time);
            Tmp.GetComponent<SightAnalysis>().Exp_time = Duration_time; //time_duration of the object
            RawRecorder.x = x;
            RawRecorder.y = y;
            RawRecorder.z = z;
            RawRecorder.AppearTask3 = true;
        }
        if (Timer > Spawn_time + Duration_time && !toAppear)
        {
            Spawn_time = Random.Range(Mean_spawn_time - Delta_Spawn_time, Mean_spawn_time + Delta_Spawn_time);
            Duration_time = Random.Range(Mean_Duration_time - Delta_Duration_time, Mean_Duration_time + Delta_Duration_time);
            Timer = 0;
            toAppear = true;
        }
    }

    void TargetApparition()
    {
        for (int i = 0; i < 25; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_3);
        }
    }
    void RandomPosition(float x_center, float y_center, float z_center, float inter_x, float inter_y, float inter_z)
    {
        x = Random.Range(x_center - inter_x, x_center + inter_x);
        y = Random.Range(y_center - inter_y, y_center + inter_y);
        z = Random.Range(z_center - inter_z, z_center + inter_z);
    }
    float ComputeDistance(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        float distance = 0;
        distance = Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
        return distance;
    }
}

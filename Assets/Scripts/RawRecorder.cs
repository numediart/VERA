using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Tobii.XR;
using ViveSR;
using ViveSR.anipal.Eye;

public class RawRecorder : MonoBehaviour
{
    private string DirName = ""; //directory where the raw datas will be stored
    private float init_time; //time at which the scene is initially instancied
    private float timer;
    private int inc;
    private Transform CameraTrsf; //transform from the camera
    private string Physiological_Recorded = "";
    private float[] sight_data;
    private static VerboseData verboseData;
    private static EyeData eyeData;
    StreamWriter sr;
    StreamWriter sr_task2;
    StreamWriter sr_task3;
    public int FrameRecordRate = 20;

    public static bool AppearTask2 = false;
    public static bool AppearTask3 = false;
    public static float x;
    public static float y;
    public static float z;
    public static float x_target;
    public static float y_target;
    public static float z_target;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        inc = inc + 1;
        sight_data = new float[9];
        CameraTrsf = GetComponent<Transform>();
        DirName = "RawSignals__"+System.DateTime.Now.ToString("hh_mm_ss");
        CreateDir(DirName);
        sr = File.CreateText(DirName + "/PhysiologicalSig.txt");
        sr.WriteLine(FrameRecordRate + "\n");
        sr_task2 = File.CreateText(DirName + "/Task2Stim.txt");
        sr_task2.WriteLine("Stimulus Apparition Position Task 2\n");
        sr_task3 = File.CreateText(DirName + "/Task3Stim.txt");
        sr_task3.WriteLine("Stimulus Apparition Position Task 3\n");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SRanipal_Eye_API.GetEyeData(ref eyeData);
        SRanipal_Eye.GetVerboseData(out verboseData);    // pupil diameter    
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World); //acquisition of the data comming from the TobiiXR device
        timer = timer + Time.deltaTime;
        if (eyeTrackingData.IsLeftEyeBlinking)
        {
            sight_data[7] = timer;
        }
        if (eyeTrackingData.IsRightEyeBlinking)
        {
            sight_data[8] = timer;
        }
        if (inc % (int)(60 / (FrameRecordRate+1)) == 0)
        {

            if (eyeTrackingData.GazeRay.IsValid)
            {
                sight_data[0] = eyeTrackingData.GazeRay.Origin.x;
                sight_data[1] = eyeTrackingData.GazeRay.Origin.y;
                sight_data[2] = eyeTrackingData.GazeRay.Origin.z;
                sight_data[3] = eyeTrackingData.GazeRay.Direction.x;
                sight_data[4] = eyeTrackingData.GazeRay.Direction.y;
                sight_data[5] = eyeTrackingData.GazeRay.Direction.z;
                sight_data[6] = 0.5f*(eyeData.verbose_data.left.pupil_diameter_mm + eyeData.verbose_data.right.pupil_diameter_mm);    // pupil diameter    
            }
            Physiological_Recorded = Math.Round(timer, 3) + "\t" + Math.Round(CameraTrsf.position.x, 5) + "\t" + Math.Round(CameraTrsf.position.y, 5) + "\t" + Math.Round(CameraTrsf.position.z, 5) + "\t" + Math.Round(CameraTrsf.rotation.eulerAngles.x, 5) + "\t" + Math.Round(CameraTrsf.rotation.eulerAngles.y, 5) + "\t" + Math.Round(CameraTrsf.rotation.eulerAngles.z, 5) + "\t";
            for (int i = 0; i<9; i++)
            {
                Physiological_Recorded = Physiological_Recorded + Math.Round(sight_data[i], 3) + "\t";
            }
            Physiological_Recorded = Physiological_Recorded + "\n";
            sr.WriteLine(Physiological_Recorded);
            
            Physiological_Recorded = "";
            //["time","x","y","z","rot x","rot y","rot z", "x eye", "y eye", "z eye","x dir eye","y dir eye","z dir eye", "pupil","blink lef","blink right"]
            //ScreenCapture.CaptureScreenshot(DirName + "/SnapShot/" + Mathf.Round(1000*timer) + ".png");
        }
        inc = inc + 1;

        if (AppearTask2)
        {
            WriteTask2();
            AppearTask2 = false;
        }
        if (AppearTask3)
        {
            WriteTask3();
            AppearTask3 = false;
        }

    }

    void OnDestroy()
    {
        sr.Close();
        sr_task2.Close();
        sr_task3.Close();
    }

    void CreateDir(string DirNam)
    {
        if (!Directory.Exists(DirNam))
        {
            Directory.CreateDirectory(DirNam);
            Directory.CreateDirectory(DirNam + "/SnapShot");
        }
        else
        {
            Debug.Log("Directory already exist. Signals will be overwritten!");
        }
    }

    void WriteTask2()
    {
        sr_task2.WriteLine(timer + "\t" + x_target + "\t" + y_target + "\t" + z_target + "\t" + x + "\t" + y + "\t" + z + "\t");
    }
    void WriteTask3()
    {
        sr_task3.WriteLine(timer + "\t" + x + "\t" + y + "\t" + z);
    }
}

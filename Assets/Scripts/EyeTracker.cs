using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tobii.XR;

public class EyeTracker : MonoBehaviour
{

    public TobiiXR_Settings Settings;

    public GameObject[] Target;

    private int inc = 0;
    private int id = -5;

    Rigidbody Self;

    public Image Flash;

    public float minDir = 50f;
    private float Timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        var settings = new TobiiXR_Settings(); //initialisation of the TobiiXR settings => eye-tracker
        //TobiiXR.Start(settings);
        TobiiXR.Start(Settings);
        Self = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World); //acquisition of the data comming from the TobiiXR device

        // Check if gaze ray is valid
        if (eyeTrackingData.GazeRay.IsValid)
        {
            // The origin of the gaze ray is a 3D point
            var rayOrigin = eyeTrackingData.GazeRay.Origin;
            // The direction of the gaze ray is a normalized direction vector
            var rayDirection = eyeTrackingData.GazeRay.Direction;
            
            Debug.DrawRay(rayOrigin, rayDirection, Color.green);

            float t = (600f - rayOrigin.z);

            float x_pos = rayDirection.x * t + rayOrigin.x;
            float y_pos = rayDirection.y * t + rayOrigin.y;


            float[] Distance = new float[Target.Length];

            for (int i = 0; i < Target.Length; i++)
            {
                float x_target = Target[i].transform.position.x;
                float y_target = Target[i].transform.position.y;
                Distance[i] = Mathf.Sqrt(Mathf.Pow(x_pos - x_target, 2f) + Mathf.Pow(y_pos - y_target, 2f));
                if(Distance[i] < minDir)
                {
                    if(i == id)
                    {
                        inc = inc + 1;
                    }
                    else
                    {
                        id = i;
                        inc = 0;
                    }
                }
                if (id != -5)
                {
                    Color Target_c = Color.HSVToRGB(210f / 360f, 0.89f, Mathf.Min(inc,50f)/ 100f);
                    Image img = Target[id].GetComponent<Image>();
                    img.color = Target_c;

                    for (int j = 0; j < Target.Length; j++)
                    {
                        if(j != id)
                        {
                            Target_c = Color.HSVToRGB(210f / 360f, 0.89f, 0.04f);
                            img = Target[j].GetComponent<Image>();
                            img.color = Target_c;
                        }
                    }
                }
            }

        }
        if (inc > 150)
        {
            Timer = Timer + Time.deltaTime;
            Self.AddForce(new Vector3(0, -1000, 0));
        }
        if(Timer > 1.75f)
        {
            Color FColor = Flash.color; //taking the color
            FColor.a = FColor.a+0.05f; //updating alpha channel
            Flash.color = FColor;
        }
        if (Timer > 3.5f)
        {
            SceneManager.LoadScene(id + 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

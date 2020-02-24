using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.XR;

public class SightGeneration : MonoBehaviour
{
    void Start()
    {
        var settings = new TobiiXR_Settings();
        TobiiXR.Start(settings);
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


            Ray ray = new Ray(rayOrigin, rayDirection); //ray from eye to the sight direction
            RaycastHit m_Hit; //hit initialisation

            if (Physics.Raycast(ray, out m_Hit)) //if the ray meet sthg
            {
                GameObject FocusedObject = m_Hit.collider.gameObject;
                SightAnalysis sightscript = FocusedObject.GetComponent<SightAnalysis>();
                if (sightscript == null)
                {
                    //... if a wall ...
                    //Debug.Log(FocusedObject.transform.name);
                }
                else
                {
                    FocusedObject.GetComponent<SightAnalysis>().Time_looked = sightscript.Time_looked + Time.deltaTime;
                    FocusedObject.GetComponent<SightAnalysis>().seen = true;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComputation : MonoBehaviour
{
    // For the Task 2
    public static int n_sphere;
    public static int wrong;
    public static int right;
    public static List<int> ScoreState = new List<int>();
    public static List<float> LookedTime = new List<float>();

    // For the Task 3
    public static float TimeLooked;
    public static List<float> SpentTime = new List<float>();

    void Awake()
    {
        n_sphere = -1;
        wrong = 0;
        right = 0;
    }

    public void ScoreSaver()
    {
        //Debug.Log("Sphere "+n_sphere+"\t Looked :"+TimeLooked);
        SpentTime.Add(TimeLooked);
    }
}

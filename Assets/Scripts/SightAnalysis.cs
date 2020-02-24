using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightAnalysis : MonoBehaviour
{
    // For the Task 2
    public bool Go;
    public float pos_tresh_time;
    //public float neg_tresh_time;
    public float Time_looked;

    public Task2StimSpawner stim_spawner;

    // For the Task 3
    public ScoreComputation script_score;
    public bool seen = false;
    public bool sent = false;

    // For Both
    public float Exp_time;
    private float Score=0f;
    private float timer;

    //Determine which task in completed
    public GameManager gm;
    public bool test;
    void Start()
    {
        Time_looked = 0f;
        timer = 0f;
        pos_tresh_time = stim_spawner.GetComponent<Task2StimSpawner>().pos_tresh_time;
        //neg_tresh_time = stim_spawner.GetComponent<Task2StimSpawner>().neg_tresh_time;
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer = timer + Time.deltaTime;
        test = gm.GetComponent<GameManager>().MadeT2;
        if (!test)
        {
            if (seen)
            {
                Time_looked = Time_looked + Time.deltaTime;
            }
        }

        else if (test)
        {
            if (seen && Score == 0)
            {
                Score = timer;
            }
        }
    }

    void OnDestroy()
    {
        if(!test)
        {
            script_score = GameObject.FindObjectOfType(typeof(ScoreComputation)) as ScoreComputation;
            timer = 0;
            if (Go) // miss
            {
                ScoreComputation.LookedTime.Add(Time_looked);
                if(Time_looked > pos_tresh_time * Exp_time)
                {
                    ScoreComputation.right = ScoreComputation.right + 1;
                    ScoreComputation.ScoreState.Add(0);
                }
            }
            else if (!Go) //wrong 
            {
                ScoreComputation.LookedTime.Add(-Time_looked);
                if(Time_looked > pos_tresh_time * Exp_time)
                {
                    ScoreComputation.wrong = ScoreComputation.wrong + 1;
                    ScoreComputation.ScoreState.Add(1);
                }
            }
        }

        else if (test)
        {
            if (timer > Exp_time - 2 * Time.deltaTime && !sent)
            {
                if (!seen)
                {
                    Score = -1f;
                }
                sent = true;
                ScoreComputation.TimeLooked = Score;
                script_score = GameObject.FindObjectOfType(typeof(ScoreComputation)) as ScoreComputation;
                script_score.ScoreSaver();
            }
        }
    }
}

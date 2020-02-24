using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WindowsInput;
using WindowsInput.Native;

public class GameManager : MonoBehaviour
{
    public GameObject Introduction; //gameobject from the blinking text for the introduction
    public GameObject Task1_Text; //gameobject from the blinking text of task 1 - meditation
    public GameObject Task2_Text; //gameobject from the blinking text of task 2 - meditation
    public GameObject Task3_Text; //gameobject from the blinking text of task 3 - meditation
    public GameObject End_Text; //gameobject from the blinking text for the end text

    public AudioSource Audio_Task1; //audio source from the text at the begining of the first task
    public AudioSource Audio_Task1_end; //audio source from the text at the end of the first task
    public AudioSource Audio_Task2; //audio source from the text at the begining of the second task
    public AudioSource Audio_Task2_end; //audio source from the text at the end of the second task
    public AudioSource Audio_Task3; //audio source from the text at the end of the third task
    public AudioSource Audio_Task3_end; //audio source from the text at the end of the third task

    public GameObject SphereSpawner_Task1; //gamobject from the sphere spawner of the first task
    public GameObject SphereSpawner_Task2; //gamobject from the sphere spawner of the second task
    public GameObject SphereSpawner_Task3; //gamobject from the sphere spawner of the third task
    
    public float TaskDuration; //Total Task Duration
    public float Task1_Duration = 60 * 5f; //task1 duration in sec
    public float Task2_Duration = 60 * 5f; //task2 duration in sec
    public float Task3_Duration = 60 * 5f; //task3 duration in sec
    
    private float timer;
    private float subtimer = -500f; //timer inter class //To replace with the press space
    private bool onTask = false;
    private bool onBreak = false;

    public bool MadeT1 = false;
    public bool MadeT2 = false;

    string DirName = "";
    InputSimulator sim = new InputSimulator(); //input simulator for synchro with EEG recorder

    void Start()
    {
        TaskDuration = Task1_Duration + Task2_Duration + Task3_Duration;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onTask)
        {
            Task1_Text.SetActive(false);
            Task2_Text.SetActive(false);
            Task3_Text.SetActive(false);
            timer += Time.deltaTime;
            onBreak = true;
            subtimer = 0f;
        }
        else
        {
            subtimer += Time.deltaTime;
        }

        if (!onTask && !Introduction.activeSelf) //leaving any wait pannel
        {
            if (subtimer >20f)
            {
                onTask = true;
                if(timer < Task1_Duration)
                {
                    OnTask1();
                }
                else if(timer < Task2_Duration+Task1_Duration && MadeT1)
                {
                    OnTask2();
                }
                else if(timer < Task1_Duration+Task2_Duration+Task3_Duration && MadeT2)
                {
                    OnTask3();
                }
            }
        }

        if (timer==0 && Input.GetKeyDown("space")) //leaving the home pannel
        {
            Audio_Task1.Play();
            subtimer = 0f;
            Introduction.SetActive(false);
            Task1_Text.SetActive(true);
        }

        if (timer > Task1_Duration && !MadeT1)
        {
            SphereSpawner_Task1.SetActive(false);
            onTask = false;
            Text text = Task1_Text.GetComponent<Text>();
            text.text = "La première tâche est terminée.";
            //text.text = "La première tâche est terminée.\n\nAppuie sur la Touche Espace pour passer à la tâche suivante.";
            Task1_Text.SetActive(true);
            if (onBreak && !MadeT1)
            {
                onBreak = false;
                EndTask();
                Audio_Task1_end.Play();
            }
            if (subtimer > 5f)
            {
                subtimer = 0f;
                Task1_Text.SetActive(false);
                Task2_Text.SetActive(true);
                Audio_Task2.Play();
                MadeT1 = true;
                onBreak = true;
            }
        }

        if (timer > Task1_Duration + Task2_Duration && !MadeT2)
        {
            //EndTask();
            SphereSpawner_Task2.SetActive(false);
            onTask = false;
            Text text = Task2_Text.GetComponent<Text>();
            text.text = "La seconde tâche est terminée.";
            //text.text = "La seconde tâche est terminée.\n\nAppuie sur la Touche Espace pour passer à la tâche suivante.";
            SaveTask2();
            Task2_Text.SetActive(true);
            if (onBreak && !MadeT2)
            {
                onBreak = false;
                EndTask();
                Audio_Task2_end.Play();
            }
            if (subtimer > 5f)
            {
                Task2_Text.SetActive(false);
                Task3_Text.SetActive(true);
                MadeT2 = true;
                Audio_Task3.Play();
                onBreak = true;
            }
        }

        if (timer > Task1_Duration + Task2_Duration +Task3_Duration)
        {
            if (onBreak)
            {
                onBreak = false;
                EndTask();
                Audio_Task3_end.Play();
            }
            SphereSpawner_Task3.SetActive(false);
            onTask = false;
            SaveTask3();
            Task3_Text.SetActive(false);
            End_Text.SetActive(true);
        }
    }

    void OnTask1()
    {
        for (int i = 0; i < 50; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_1); //press 1 for EEG Recorder -> Task Begin
        }
        //Debug.Log("Task1");
        //interesting to play a sound in the backgorund
        //!\\ To Add 
        SphereSpawner_Task1.SetActive(true);
        MakeDir(); //create the directory of the records
    }

    void OnTask2()
    {
        for (int i = 0; i < 50; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        }
        //Debug.Log("Task2");
        SphereSpawner_Task2.SetActive(true);
    }
    void OnTask3()
    {
        for (int i = 0; i < 50; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
        }
        //Debug.Log("Task3");
        SphereSpawner_Task3.SetActive(true);
    }

    void EndTask() // press 2 for ending the task
    {
        for (int i = 0; i < 50; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_2);
        }
    }

    void MakeDir()
    {
        DirName = "TaskRecords_" + System.DateTime.Now.ToString("MM_dd__") + System.DateTime.Now.ToString("hh_mm");
        if (!Directory.Exists(DirName))
        {
            Directory.CreateDirectory(DirName);
        }
        else
        {
            DirName = DirName + "_bis";
            Directory.CreateDirectory(DirName);
        }
    }

    void SaveTask2()
    {
        //Task 2
        var task2 = "Total Number of Spheres: " + ScoreComputation.n_sphere + "\n Right: " + ScoreComputation.right + "\n Wrong: " + ScoreComputation.wrong;
        var sr2 = File.CreateText(DirName + "/SummaryTask2.txt");
        sr2.WriteLine(task2);
        sr2.Close();

        task2 = "";
        foreach (float f in ScoreComputation.LookedTime)
        {
            task2 = task2 + f + "\n";
        }
        sr2 = File.CreateText(DirName + "/TotalTask2.txt");
        sr2.WriteLine(task2);
        sr2.Close();
    }

    void SaveTask3()
    {
        //Task 3
        var task3 = "Total Number of Spheres: " + ScoreComputation.n_sphere + "\n Mean Time: " + ScoreComputation.SpentTime.Average();
        var sr3 = File.CreateText(DirName + "/SummaryTask3.txt");
        sr3.WriteLine(task3);
        sr3.Close();

        task3 = "";
        foreach (float f in ScoreComputation.SpentTime)
        {
            task3 = task3 + f + "\n";
        }
        sr3 = File.CreateText(DirName + "/TotalTask3.txt");
        sr3.WriteLine(task3);
        sr3.Close();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class Task1StimSpawner : MonoBehaviour
{
    public GameObject AuditoryPerturbation; //audio perturbator
    public GameObject VisualPerturbation; //visual perturbator
    public GameObject AudioVisuaPerturbation; //audiovisual perturbator

    public float A_appTime; //apparition time for the audio perturb
    public float V_appTime; //apparition time for the visual perturb
    public float AV_appTime; //apparition time for the audiovisual perturb

    private bool A_appeard = false; //has the auditory appeared ? 
    private bool V_appeard = false; //has the auditory appeared ? 
    private bool AV_appeard = false; //has the auditory appeared ? 

    private float timer;

    InputSimulator sim = new InputSimulator(); //input simulator for synchro with EEG recorder

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > A_appTime && !A_appeard)
        {
            PerturbApparition();
            AuditoryPerturbation.SetActive(true);
            Destroy(AuditoryPerturbation, 30);
            A_appeard = true;
        }

        if (timer > V_appTime && !V_appeard)
        {
            PerturbApparition();
            VisualPerturbation.SetActive(true);
            Destroy(VisualPerturbation, 30);
            V_appeard = true;
        }

        if (timer > AV_appTime && !AV_appeard)
        {
            PerturbApparition();
            AudioVisuaPerturbation.SetActive(true);
            Destroy(AudioVisuaPerturbation, 30);
            AV_appeard = true;
        }


    }

    void PerturbApparition()
    {
        for (int i = 0; i < 25; i++)
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.VK_4);
        }
    }
}

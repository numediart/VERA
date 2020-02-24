using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class KeySimulator : MonoBehaviour
{
    float timer = 0;
    InputSimulator sim = new InputSimulator();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        timer = timer + Time.deltaTime;
        if (timer > 5)
        {
            for (int i = 0; i < 10; i++)
            {
                sim.Keyboard.KeyPress(VirtualKeyCode.VK_1);
            }
            timer = 0;
        }


    }
}

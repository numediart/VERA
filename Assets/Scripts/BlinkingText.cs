using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public Text TitleText; //Text comp linked to the title
    private float Timer; //Timer

    public float blinking_vel=2f; //Velocity at which the text is blinking
    // Update is called once per frame
    void Update()
    {
        Timer = Timer + Time.deltaTime; //Timer
        Color TextColor = TitleText.color; //taking the color
        TextColor.a = Mathf.Abs(Timer%blinking_vel - blinking_vel/2)/(0.5f*blinking_vel); //updating alpha channel
        TitleText.color = TextColor; //update color 
    }
}

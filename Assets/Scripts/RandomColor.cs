using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Color AvoidColor;
    private Color new_color = Color.black;
    private float distance = 0f;
    float H,S,V,H_;
    void Start()
    {
        Color.RGBToHSV(new_color, out H_, out S, out V);
        while (distance < 0.25)
        {
            new_color.r = Random.Range(0, 1f);
            new_color.g = Random.Range(0, 1f);
            new_color.b = Random.Range(0f, 1f);
            Color.RGBToHSV(new_color, out H, out S, out V);
            distance = Mathf.Abs(H - H_);
        }
        gameObject.GetComponent<Renderer>().material.color = new_color;
    }
}

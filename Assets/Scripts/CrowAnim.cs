using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAnim : MonoBehaviour
{
    Animator CrowAnimator;

    public AudioSource CrowEating;
    public AudioSource CrowSinging;

    private float state = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        CrowAnimator = GetComponent<Animator>();
        state = Random.Range(-5f, 5f);
        CrowAnimator.SetFloat("State", state);
        if (state > 0)
        {
            CrowSinging.Play();
        }
        else
        {
            CrowEating.Play();
        }
    }
}

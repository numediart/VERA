using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthdayAgentSpawner : MonoBehaviour
{
    public GameObject CatPrefab;
    public GameObject Balloon;
    public GameObject BallonPrefab;
    public Transform BalloonPosition;

    public AudioSource FireCracking;

    public GameObject GiftBox;
    public float PushForce;
    private bool PushBox = true;

    private int inc = 0;
    private int cat_spawningtime = 350; //corresponding to 10'
    private bool isBalloon = true;
    private int balloon_react = 750;
    private int fireCrackTime = 500;
    private int falling_box = 650;
    private bool firePlay = false;
    public float VForce = 5f;
    private Rigidbody Balloon_rgb;

    private float Timer;
    void Start()
    {
        Balloon_rgb = Balloon.GetComponent<Rigidbody>();
        BalloonPosition = BallonPrefab.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        inc = inc + 1;

        Timer = Timer + Time.deltaTime;
        if (inc % cat_spawningtime == 0 && inc != 0)
        {
            float spawn_pos = Random.Range(-120f, -40f);//remettre -120f
            if (Random.Range(-1f, 1f) > 0)
            {
                Instantiate(CatPrefab, new Vector3(-20f, 25f, spawn_pos), Quaternion.identity);
            }
            else
            {
                Instantiate(CatPrefab, new Vector3(spawn_pos, 25f, -20f), Quaternion.identity);
            }
        }

        if (inc % balloon_react == 0 && inc != 0 && isBalloon)
        {
            GameObject BalloonRope = Balloon.transform.GetChild(0).gameObject;
            Destroy(BalloonRope);
            Balloon_rgb.AddForce(0, VForce, 0);
            Destroy(Balloon, 15f);
            isBalloon = false;
        }
        if (inc % (balloon_react + 16 * 50) == 0 && inc != 0 && !isBalloon)
        {
            isBalloon = true;
            GameObject Balloon = Instantiate(BallonPrefab, BalloonPosition);
            Balloon_rgb = Balloon.GetComponent<Rigidbody>();
        }

        if (inc % fireCrackTime == 0 && inc != 0 && !firePlay)
        {
            firePlay = true;
            FireCracking.Play();
        }

        if (inc % falling_box == 0 && inc != 0 && PushBox)
        {
            Rigidbody boxRgb = GiftBox.GetComponent<Rigidbody>();
            boxRgb.AddForce(PushForce, 0, 0);
            PushBox = false;
        }
    }
}

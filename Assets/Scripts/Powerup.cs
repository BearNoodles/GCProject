using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class Powerup : MonoBehaviour {

    public GameObject player;
    public int powerNo;
    public int timer;
    private int startTimer;
    private float resetTime;
    private bool isActive;
    public static GameObject[] powerups;

    private static AudioSource sound;

    // Use this for initialization
    void Start ()
    {
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        resetTime = timer;
        startTimer = timer;

        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        if (resetTime < timer)
        {
            resetTime += Time.deltaTime;
            if (resetTime >= timer)
            {
                foreach (GameObject p in powerups)
                    p.SetActive(true);
                timer = startTimer;
            }
        }

    }

    private void SetActivePowerup()
    {
        foreach (GameObject p in powerups)
        {
            p.GetComponent<Powerup>().isActive = false;
            p.GetComponent<Powerup>().timer = timer;
            p.GetComponent<Powerup>().resetTime = 0;
        }
        sound.Play();
        isActive = true;
    }

    public static int ActivePowerup()
    {
        foreach (GameObject power in powerups)
        {
            if (power.GetComponent<Powerup>().isActive)
            {
                return power.GetComponent<Powerup>().powerNo;
            }
        }
        return 0;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            SetActivePowerup();
            GameObject.FindGameObjectWithTag("Player").GetComponent<RigidbodyFirstPersonController>().SetPower(ActivePowerup(), timer);
            gameObject.SetActive(false);
        }
    }
}

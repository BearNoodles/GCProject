using UnityEngine;
using System.Collections;
using System;

public class Checkpoint : MonoBehaviour {

    public bool isActive = false;
    public static GameObject[] checkpoints;
    private AudioSource sound;

	// Use this for initialization
	void Start ()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        Array.Sort(checkpoints, (x, y) => String.Compare(x.name, y.name));
        sound = GetComponent<AudioSource>();
    }

    private void SetActive()
    {
        if (!isActive)
            sound.Play();

        foreach (GameObject point in checkpoints)
        {
            point.GetComponent<Checkpoint>().isActive = false;
            point.GetComponent<Renderer>().material.color = Color.white;
        }

        isActive = true;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public static Vector3 SetResetPosition()
    {
        Vector3 reset = new Vector3(-30, 10, -150);
        if (checkpoints != null)
        {
            foreach (GameObject point in checkpoints)
            {
                if (point.GetComponent<Checkpoint>().isActive)
                {
                    reset = point.transform.position;
                    break;
                }
            }
        }
        return reset;
    }

    public static Vector3 NextCheckpoint()
    {
        Vector3 reset = new Vector3(0, 0, 0);
        if (checkpoints != null)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].GetComponent<Checkpoint>().isActive)
                {
                    checkpoints[i].GetComponent<Checkpoint>().isActive = false;
                    checkpoints[(i + 1) % checkpoints.Length].GetComponent<Checkpoint>().isActive = true;
                    reset = checkpoints[(i + 1) % checkpoints.Length].transform.position;
                    break;
                }
            }
        }
        return reset;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            SetActive();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour {

    float volume2d;
    float volume3d;

    public List<GameObject> speakers;


    private void Awake()
    {
        volume2d = 0.15f;
        volume3d = 1.0f;

        speakers = new List<GameObject>();

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Speaker"))
        {
            speakers.Add(g);
        }

        foreach (GameObject g in speakers)
        {
            g.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
            g.GetComponent<AudioSource>().Play();
        }

        MuteAll();
    }
    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public float GetVolume(bool spatialised)
    {
        if(spatialised)
        {
            return volume3d;
        }
        return volume2d;
    }

    public void MuteAll()
    {
        foreach(GameObject s in speakers)
        {
            s.GetComponent<AudioSource>().volume = 0;
        }
    }

    public void ResetSpeakers()
    {
        volume2d = 0.15f;
        volume3d = 1.0f;

        speakers.Clear();

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Speaker"))
        {
            if(g.GetComponent<AudioSource>().isActiveAndEnabled)
                speakers.Add(g);
        }
        while(speakers.Count > 3)
        {
            speakers.RemoveAt(speakers.Count);
        }

        foreach (GameObject g in speakers)
        {
            g.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
            g.GetComponent<AudioSource>().Play();
        }

        MuteAll();
    }
    
    
}

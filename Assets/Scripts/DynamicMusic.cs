using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour {

    float volume2d;
    float volume3d;

    public List<GameObject> speakers;


    private void Awake()
    {
        //Volumes for when music is 2D or 3D
        volume2d = 0.04f;
        volume3d = 1.0f;

        //Find all speakers in scene
        speakers = new List<GameObject>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Speaker"))
        {
            speakers.Add(g);
        }

        //Get speakers audio sources and set the music clip playing
        foreach (GameObject g in speakers)
        {
            g.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
            g.GetComponent<AudioSource>().Play();
        }

        //Set all speakers to zero volume (but leave playing)
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

    //Return either 2D or 3D volume
    public float GetVolume(bool spatialised)
    {
        if(spatialised)
        {
            return volume3d;
        }
        return volume2d;
    }

    //Mutes all speakers
    public void MuteAll()
    {
        foreach(GameObject s in speakers)
        {
            s.GetComponent<AudioSource>().volume = 0;
        }
    }

    //Finds speakers again when scene is reset
    //Awake is not called again since object uses dont destroy on load
    public void ResetSpeakers(float musicTime)
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
            g.GetComponent<AudioSource>().time = musicTime;
            g.GetComponent<AudioSource>().Play();
        }
    }
    
    
}

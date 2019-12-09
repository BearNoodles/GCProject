using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

    DynamicMusic music;

    public bool music3D;

    public List<GameObject> speakersOn;
    List<AudioSource> sources;

    float volume2D;
    float volume3D;

	// Use this for initialization
	void Start ()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<DynamicMusic>();
        volume2D = music.GetVolume(false);
        volume3D = music.GetVolume(true);
        Debug.Log("2 is " + volume2D);
        Debug.Log("3 is " + volume3D);

        sources = new List<AudioSource>();

        foreach (GameObject s in speakersOn)
        {
            sources.Add(s.GetComponent<AudioSource>());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SetSpeakers()
    {
        music.MuteAll();
        foreach (AudioSource s in sources)
        {
            SetSpatialize(s, music3D);
        }
    }

    public void SetSpatialize(AudioSource source, bool value)
    {
        int blendValue = 0;
        
        if (value)
        {
            blendValue = 1;
            source.volume = volume3D;
        }
        else
        {
            source.volume = volume2D;
        }
        source.spatialize = value;
        source.spatialBlend = blendValue;

    }

    public void Reset()
    {

    }
    

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("col");
        if (col.tag == "Player")
        {
            SetSpeakers();
        }
    }
}

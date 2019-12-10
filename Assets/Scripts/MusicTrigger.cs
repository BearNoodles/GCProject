using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

    DynamicMusic music;

    //Sets wether this triggers sets the music £d or 2D
    public bool music3D;

    //List of speakers to switch on with this trigger
    public List<GameObject> speakersOn;
    List<AudioSource> sources;

    float volume2D;
    float volume3D;

	// Use this for initialization
	void Start ()
    {
        //Find dynamic music object
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<DynamicMusic>();
        volume2D = music.GetVolume(false);
        volume3D = music.GetVolume(true);

        //List of audio sources in relevant speakers
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

    //Set up speakers when player touches the trigger
    void SetSpeakers()
    {
        sources = new List<AudioSource>();

        //Clear sources in case resetting game messes it up
        sources.Clear();
        foreach (GameObject s in speakersOn)
        {
            sources.Add(s.GetComponent<AudioSource>());
        }

        //Mute all other speakers in scene using dynamic music object
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<DynamicMusic>();
        music.MuteAll();

        //Set relevant speakers on with appropriate spatialisation
        foreach (AudioSource s in sources)
        {
            SetSpatialize(s, music3D);
        }
    }

    //Sets a sources spatialisation values and volume
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
        if (col.tag == "Player")
        {
            SetSpeakers();
        }
    }
}

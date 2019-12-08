using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour {

    float Volume2d;
    float Volume3d;

	// Use this for initialization
	void Start ()
    {
        Volume2d = 0.15f;
        Volume3d = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SetSpatialize(bool value)
    {
        int blendValue = 0;
        
        AudioSource source = GetComponent<AudioSource>();
        if (value)
        {
            blendValue = 1;
            source.volume = Volume3d;
        }
        else
        {
            source.volume = Volume2d;
        }
        source.spatialize = value;
        source.spatialBlend = blendValue;
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            SetSpatialize(false);
        }
    }
}

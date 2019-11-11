using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

        //DONT CALL THIS ON START
        SetSpatialize(false);	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SetSpatialize(bool value)
    {
        int blendValue = 0;
        if(value)
        {
            blendValue = 1;
        }
        AudioSource source = GetComponent<AudioSource>();
        source.spatialize = value;
        source.spatialBlend = blendValue;
        source.volume = 0.2f;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            SetSpatialize(false);
            Destroy(this);
        }
    }
}

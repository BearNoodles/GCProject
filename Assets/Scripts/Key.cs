using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public int keyNumber;
    public int midiNo;
    KeyboardPuzzle keyPuzzle;

	// Use this for initialization
	void Start ()
    {
        keyPuzzle = transform.parent.GetComponent<KeyboardPuzzle>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private bool CheckKey()
    {
        if(keyPuzzle.GetNextKey() == keyNumber)
        {
            return true;
        }

        return false;
    }

    private void PressSwitch()
    {
        keyPuzzle.PlayNote(midiNo);
        Vector3 pos = transform.position;
        pos.y -= gameObject.transform.lossyScale.y / 2;
        transform.position = pos;
        if (CheckKey())
        {
            keyPuzzle.AdvanceKeNumber();
        }
        else
        {
            keyPuzzle.ResetPuzzle();
        }

    }

    private void UnpressSwitch()
    {
        Vector3 pos = transform.position;
        pos.y += transform.lossyScale.y / 2;
        transform.position = pos;
        keyPuzzle.StopNote(midiNo);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PressSwitch();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            UnpressSwitch();
        }
    }
}

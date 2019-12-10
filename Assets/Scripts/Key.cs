using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    //Order of key in puzzle
    public int keyNumber;

    //Note which key will play
    public int midiNo;

    KeyboardPuzzle keyPuzzle;

	// Use this for initialization
	void Start ()
    {
        //Get parent object
        keyPuzzle = transform.parent.GetComponent<KeyboardPuzzle>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Checks if the next key in the puzzle matches this key
    private bool CheckKey()
    {
        if(keyPuzzle.GetNextKey() == keyNumber)
        {
            return true;
        }

        return false;
    }

    //Called when player touches key
    private void PressSwitch()
    {
        //Adds a note to the synthesiser
        keyPuzzle.PlayNote(midiNo);

        //Moves key to pressed position
        Vector3 pos = transform.position;
        pos.y -= gameObject.transform.lossyScale.y / 2;
        transform.position = pos;

        //Advances puzzle to next key or resets puzzle if wrong
        if (CheckKey())
        {
            keyPuzzle.AdvanceKeNumber();
        }
        else
        {
            keyPuzzle.ResetPuzzle();
        }

    }

    //Called when player stops touching key
    private void UnpressSwitch()
    {
        //Moves key to unpressed position
        Vector3 pos = transform.position;
        pos.y += transform.lossyScale.y / 2;
        transform.position = pos;

        //Stop this key playing a note in the synthesiser
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

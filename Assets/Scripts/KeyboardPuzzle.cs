using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPuzzle : MonoBehaviour {

    List<GameObject> keyObjects;
    int nextKey;
    public int finalKey;

    Synth synthesiser;

    private AudioSource correctSound;

    public List<GameObject> puzzleObjects;

    bool puzzleComplete;

    // Use this for initialization
    void Start ()
    {
        puzzleComplete = false;
        nextKey = 0;

        synthesiser = GetComponent<Synth>();

        keyObjects = new List<GameObject>();

	    for(int i = 0; i < transform.childCount; i++)
        {
            keyObjects.Add(transform.GetChild(i).gameObject);
        }

        foreach (AudioSource sound in GetComponents<AudioSource>())
        {
            if (sound.clip && sound.clip.name == "correct")
                correctSound = sound;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(nextKey);
	}

    public void PlayNote(int midiNo)
    {
        synthesiser.AddNote(midiNo);
    }

    public void StopNote(int midiNo)
    {
        synthesiser.RemoveNote(midiNo);
    }

    public int GetNextKey()
    {
        return nextKey;
    }

    public void AdvanceKeNumber()
    {
        if(puzzleComplete)
        {
            return;
        }
        nextKey++;
        if(nextKey > finalKey)
        {
            CompletePuzzle();
        }
    }

    public void ResetPuzzle()
    {
        nextKey = 0;
    }

    private void CompletePuzzle()
    {
        //correctSound.Play();
        synthesiser.AddTimedNote(57, 0.5f);
        synthesiser.AddTimedNote(58, 0.5f);
        synthesiser.AddTimedNote(59, 0.5f);
        synthesiser.AddTimedNote(60, 0.5f);
        puzzleComplete = true;
        for (int i = 0; i < puzzleObjects.Count; i++)// GameObject thing in switchControls)
        {
            if (puzzleObjects[i] != null)
            {
                foreach (MonoBehaviour mono in puzzleObjects[i].gameObject.GetComponents<MonoBehaviour>())
                {
                    if (mono.isActiveAndEnabled)
                        mono.enabled = false;
                    else
                        mono.enabled = true;
                }
            }
        }
    }
}

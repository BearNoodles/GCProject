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

        //Next key is the next key to be pressed in the puzzle
        nextKey = 0;

        //Synthesiser
        synthesiser = GetComponent<Synth>();

        //The gameobjects representing the piano keys
        keyObjects = new List<GameObject>();

        //Add key objects to a list of gameobjects
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

	}

    //Individual keys call this to add a note to the synthesiser
    public void PlayNote(int midiNo)
    {
        synthesiser.AddNote(midiNo);
    }

    //Stop a note currently playing in the synthesiser
    public void StopNote(int midiNo)
    {
        synthesiser.RemoveNote(midiNo);
    }

    //Returns which key is next in the puzzle
    public int GetNextKey()
    {
        return nextKey;
    }

    //Advances the nextkey variable if the puzzle is not complete
    //Completes puzzle if the final key is pressed in order
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

    //Resets the order of the keys to be pressed
    public void ResetPuzzle()
    {
        nextKey = 0;
    }

    private void CompletePuzzle()
    {
        //Plays correct sound
        //correctSound.Play();

        //Stops any currently playing sounds and queues some notes to play a tune
        synthesiser.ClearNotes();
        synthesiser.AddTimedNote(60, 0.5f);
        synthesiser.AddTimedNote(57, 0.5f);
        synthesiser.AddTimedNote(67, 0.5f);
        synthesiser.AddTimedNote(64, 0.5f);
        synthesiser.AddTimedNote(62, 0.5f);
        puzzleComplete = true;

        //Enables scripts on attached game objects so they activate
        //Door opens and lift starts moving
        for (int i = 0; i < puzzleObjects.Count; i++)
        {
            if (puzzleObjects[i] != null)
            {
                foreach (MonoBehaviour mono in puzzleObjects[i].GetComponents<MonoBehaviour>())
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

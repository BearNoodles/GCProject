using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPuzzle : MonoBehaviour {

    List<GameObject> keyObjects;
    int nextKey;
    public int finalKey;

    Synth synthesiser;


    public List<GameObject> puzzleObjects;

    bool puzzleComplete;

    // Use this for initialization
    void Start ()
    {
        nextKey = 0;

        synthesiser = GetComponent<Synth>();

        keyObjects = new List<GameObject>();

	    for(int i = 0; i < transform.childCount; i++)
        {
            keyObjects.Add(transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
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
        nextKey++;
        if(nextKey > finalKey)
        {

        }
    }

    public void ResetPuzzle()
    {
        nextKey = 0;
    }

    private void CompletePuzzle()
    {
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

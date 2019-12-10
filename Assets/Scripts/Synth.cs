using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Synth : MonoBehaviour {

    //[Range(50, 1000)]
    //public float frequency = 440.0f;

    enum SynthType { sine, square, triangle};

    SynthType type;

    public class TimedNote
    {
        public float timer;
        public float maxTime;
        public int midiNumber;
        public bool remove = false;
    }
    
    [Range(0.0f, 1.0f)]
    float gain = 0.0f;

    [Range(0.0f, 1.0f)]
    public float volume;

    List<TimedNote> timedNotes;
    List<float> noteFreqs;
    float increment;
    float phase;
    float samplingFrequency = 48000.0f;

    int[] scaleNotes;
    float thisFrequency;

    int currentNotes;

    //int timeIndex;

    // Use this for initialization
    void Start()
    {
        //List of notes currently playing
        noteFreqs = new List<float>();

        //List of notes to be queued 
        timedNotes = new List<TimedNote>();
        
        //Synth wave type
        type = SynthType.sine;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check player Input
        Vector2 input = GetInput();
        

        //Checks if there are any timed notes left to play
        if(timedNotes.Count > 0)
        {
            //If no notes are currently playing add first note from timed notes
            if(noteFreqs.Count <= 0)
            {
                AddNote(timedNotes[0].midiNumber);
            }
            //Otherwise increase the time of the currently playing note
            else
            {
                timedNotes[0].timer += Time.deltaTime;
            }

            //If currently playing timed note has played for set time then remove from both timednotes and notefreqs
            if (timedNotes[0].timer > timedNotes[0].maxTime)
            {
                RemoveNote(timedNotes[0].midiNumber);
                timedNotes.RemoveAt(0);
            }
        }
        
        //Change synth wave type when K is pressed
        if (Input.GetKeyDown(KeyCode.K))
        {
            type++;
            if((int)type > 2)
            {
                type = SynthType.sine;
            }
        }

        //set gain
        gain = volume;

        //Half the gain if its a square wave because my ears
        if (type == SynthType.square)
        {
            gain = volume / 2;
        }
    }

    private Vector2 GetInput()
    {
        Vector2 input = new Vector2
        {
            x = CrossPlatformInputManager.GetAxis("Horizontal"),
            y = CrossPlatformInputManager.GetAxis("Vertical")
        };
        return input;
    }

    //Sets audio data to be played
    void OnAudioFilterRead(float[] data, int channels)
    {
        //Gain factor is used to reduce volume of multiple notes playing at once
        float gainFactor = gain;
        if(noteFreqs.Count > 0)
        {
            gainFactor = gain / noteFreqs.Count;
        }

        //Loop for each note in list
        for (int n = 0; n < noteFreqs.Count; n++)
        {
            //incrememnt to be added to wave phase based on note frequency
            increment = noteFreqs[n] * 2.0f * Mathf.PI / samplingFrequency;
            for (int i = 0; i < data.Length; i += channels)
            {
                phase += increment;

                //Sin wave
                if(type == SynthType.sine)
                {
                    data[i] += gainFactor * Mathf.Sin(phase);
                }

                //Square wave
                else if (type == SynthType.square)
                {
                    if(gain * Mathf.Sin(phase) >= 0)
                    {
                        data[i] += gainFactor * 0.6f;
                    }
                    else
                    {
                        data[i] += -gainFactor * 0.6f;
                    }
                }

                //Triangle wave
                else
                {
                    data[i] += gainFactor * Mathf.PingPong(phase, 1.0f);
                }

                //Copy left channel to right
                if (channels == 2)
                {
                    data[i + 1] = data[i];
                }

                //Reset the phase
                if (phase > (Mathf.PI * 2))
                {
                    phase = 0.0f;
                }
            }
        }

        
    }

    float CalculateFrequencyFromMIDINumber(float midino)
    {
        float freq = 440.0f * Mathf.Pow(2.0f, ((midino - 69.0f) / 12.0f));
        return freq;
    }

    //public float CreateSine(int timeIndex, float frequency, float sampleRate, float amplitude)
    //{
    //    return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate) * amplitude;
    //}

    public void AddNote(int midiNo)
    {
        noteFreqs.Add(CalculateFrequencyFromMIDINumber(midiNo));
    }

    public void AddTimedNote(int midiNo, float time)
    {
        TimedNote temp = new TimedNote() { maxTime = time, midiNumber = midiNo };
        timedNotes.Add(temp);
    }

    public void RemoveNote(int midiNo)
    {
        noteFreqs.Remove(CalculateFrequencyFromMIDINumber(midiNo));
    }

    public void ClearNotes()
    {
        noteFreqs.Clear();
    }

    public void Stop()
    {
        gain = 0.0f;
    }
}

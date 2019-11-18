using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Synth : MonoBehaviour {

    //[Range(50, 1000)]
    //public float frequency = 440.0f;
    
    [Range(0.0f, 1.0f)]
    float gain = 0.0f;

    [Range(0.0f, 1.0f)]
    public float volume;

    List<float> noteFreqs;
    float increment;
    float phase;
    float samplingFrequency = 48000.0f;

    public int[] scaleNotes;
    float thisFrequency;

    int currentNotes;

    int timeIndex;

    // Use this for initialization
    void Start()
    {
        scaleNotes = new int[7] { 69, 71, 72, 74, 76, 77, 79 };
        currentNotes = 0;
        noteFreqs = new List<float>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 input = GetInput();
		//if(Input.GetKey(KeyCode.Space))
  //      {
  //          PlayNote(scaleNotes[5]);
  //      }
  //      else if(Input.GetKey(KeyCode.D))
  //      {
  //          PlayNote(scaleNotes[1]);
  //      }
  //      else if(Input.GetKey(KeyCode.A))
  //      {
  //          PlayNote(scaleNotes[4]);
  //      }
  //      else if(Input.GetKey(KeyCode.W))
  //      {
  //          PlayNote(scaleNotes[0]);
  //      }
  //      else if (Input.GetKey(KeyCode.S))
  //      {
  //          PlayNote(scaleNotes[2]);
  //      }
  //      else
  //      {
  //          gain = 0.0f;
  //      }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddNote(scaleNotes[5]);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            RemoveNote(scaleNotes[5]);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddNote(scaleNotes[1]);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            RemoveNote(scaleNotes[1]);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AddNote(scaleNotes[4]);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            RemoveNote(scaleNotes[4]);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            AddNote(scaleNotes[0]);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            RemoveNote(scaleNotes[0]);
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            AddNote(scaleNotes[2]);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            RemoveNote(scaleNotes[2]);
        }

        else
        {
            //gain = 0.0f;
        }



        gain = volume;
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


    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int n = 0; n < noteFreqs.Count; n++)
        {
            increment = noteFreqs[n] * 2.0f * Mathf.PI / samplingFrequency;
            for (int i = 0; i < data.Length; i += channels)
            {
                phase += increment;

                //Sin wave
                //data[i] += gain * Mathf.Sin(phase);

                //Square wave
                if(gain * Mathf.Sin(phase) >= 0)
                {
                    data[i] += gain * 0.6f;
                }
                else
                {
                    data[i] += -gain * 0.6f;
                }

                //Triangle wave
                //data[i] += gain * Mathf.PingPong(phase, 1.0f);

                
                if (channels == 2)
                {
                    data[i + 1] = data[i];
                }

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

    public float CreateSine(int timeIndex, float frequency, float sampleRate, float amplitude)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate) * amplitude;
    }

    void AddNote(int midiNo)
    {
        noteFreqs.Add(CalculateFrequencyFromMIDINumber(midiNo));
    }

    void RemoveNote(int midiNo)
    {
        noteFreqs.Remove(CalculateFrequencyFromMIDINumber(midiNo));
    }

    public void Stop()
    {
        gain = 0.0f;
    }
}

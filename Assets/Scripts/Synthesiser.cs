using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesiser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    float CalculateFrequencyFromMIDINumber(float midino)
    {
        float freq = 440.0f * Mathf.Pow(2.0f, ((midino - 69.0f) / 12.0f));
        return freq;
    }



    int Main()
    {
        List<int> sinSamples;
        sinSamples = new List<int>();
	    List<int> sinSamplesNotes;
        sinSamplesNotes = new List<int>();

        int big = 0, small = 0;
        foreach (int i in sinSamples)
        {
            if (i > big)
            {
                big = i;
            }
            /*if (small == 0)
    		{
    			small = i;
    		}*/
            if (i < small)
            {
                small = i;
            }
        }
    
        //for (auto i : samples)
        //{
        //    rSamples.insert(rSamples.begin(), i);
        //}
    
        float amplitude = 20000.0f;
        float frequency = 440.0f;
        float d = (20000.0f * (3.0f + (1.0f / 3.0f))) / 44100.0f;
    
        float t = -1.0f;

        float[] notes;
        notes = new float[7]{ 69, 71, 72, 74, 76, 77, 79 };
        for (int i = 0; i < 7; i++)
        {
            notes[i] = CalculateFrequencyFromMIDINumber(notes[i]);
        }
        for (int j = 0; j < 7; j++)
        {
            for (int i = 0; i < 44100; i++)
            {
                float time = (float)i / 44100.0f;
                float value = amplitude * Mathf.Sin(time * notes[j] * 2 * 3.141592654f);
                /*amplitude /= 1.0005f;*/
                if (amplitude > 0)
                {
                    amplitude -= d;
                }
                else if (t == -1)
                {
                    //t = time;
                }
                sinSamplesNotes.Add((int)value);
            }
            amplitude = 20000.0f;
        }
   
        int MN = 123;
    
    
        //for (auto & i : samples)
        //{
        //    i *= 1;
        //}
        

        //CREATE A SOUND CLIP
        //sf::SoundBuffer modBuffer;


        //MAKE ALL THIS WORK

        //modBuffer.loadFromSamples(sinSamplesNotes.data(), sinSamplesNotes.size(), 1, 44100);
    
        // sf::Sound controls playback of a sound.
        //sf::Sound sound(modBuffer);
    
        //if (!modBuffer.saveToFile("output.wav"))
        //{
        //    die("saving failed");
        //}
    
        // play() starts playback in a separate thread -- so it returns
        // immediately, with the sound playing in the background.

        //sound.play();
        //sound.setLoop(true);
    
        // Wait for the sound to finish playing.
        //while (sound.getStatus() == sf::Sound::Playing)
        //{
        //    sf::sleep(sf::milliseconds(1));
        //}
    
        return 0;
    }
}

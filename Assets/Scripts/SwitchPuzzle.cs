using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchPuzzle : MonoBehaviour
{

    private static GameObject[] switches;
    public List<GameObject> switchControls;
    private bool isActive;
    public int group, number;
    private static int maxGroup;
    private static int[] current;
    private static int[] maxNumber;

    private AudioSource correctSound;
    private AudioSource wrongSound;
    private AudioSource click;

    // Use this for initialization
    void Start()
    {
        //switches = GameObject.FindGameObjectsWithTag("PuzzleSwitch");
        //isActive = false;
        //foreach (GameObject s in switches)
        //    if (s.GetComponent<SwitchPuzzle>().group > maxGroup)
        //        maxGroup = s.GetComponent<SwitchPuzzle>().group;
        maxGroup=1000;
        current = new int[maxGroup];
        for (int i = 0; i < maxGroup; i++)
            current[i] = -1;
        maxNumber = new int[maxGroup];
        //for (int i = 0; i < maxGroup; i++)
        //    foreach (GameObject s in switches)
        //        if (s.GetComponent<SwitchPuzzle>().group == i && maxNumber[i] < s.GetComponent<SwitchPuzzle>().number)
        //            maxNumber[i] = s.GetComponent<SwitchPuzzle>().number;

        foreach (AudioSource sound in GetComponents<AudioSource>())
        {
            if (sound.clip.name == "correct")
                correctSound = sound;
            if (sound.clip.name == "wrong")
                wrongSound = sound;
            if (sound.clip.name == "click")
                click = sound;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void PressSwitch()
    {
        current[group]++;
        if (number == current[group])
        {
            isActive = true;
            correctSound.Play();
            click.Play();
            Vector3 pos = transform.position;
            pos.y -= gameObject.transform.lossyScale.y / 2;
            transform.position = pos;
            if (current[group] == maxNumber[group])
                FinalSwitch();
        }

        else
        {
            isActive = false;
            wrongSound.Play();
            //foreach (GameObject s in switches)
            //{
            //    if (s.GetComponent<SwitchPuzzle>().isActive == true && s.GetComponent<SwitchPuzzle>().group == group)
            //    {
            //        Vector3 pos = s.transform.position;
            //        pos.y += s.transform.lossyScale.y / 2;
            //        s.transform.position = pos;
            //        s.GetComponent<SwitchPuzzle>().isActive = false;
            //    }
            //}
            current[group] = -1;
        }
    }

    void FinalSwitch()
    {
        for (int i = 0; i < switchControls.Count; i++)// GameObject thing in switchControls)
        {
            if (switchControls[i] != null)
            {
                foreach (MonoBehaviour mono in switchControls[i].gameObject.GetComponents<MonoBehaviour>())
                {
                    if (mono.isActiveAndEnabled)
                        mono.enabled = false;
                    else
                        mono.enabled = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!isActive)
                PressSwitch();
        }
    }
}
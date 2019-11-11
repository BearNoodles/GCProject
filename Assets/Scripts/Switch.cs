using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class Switch : MonoBehaviour {
    

    public List<GameObject> things;
    public GameObject player;
    private AudioSource sound;

    public float pressDistance;


	void Start ()
    {
        sound = gameObject.GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 25))
        {
            if ((player.transform.position - gameObject.transform.position).magnitude < pressDistance && hit.collider.gameObject == gameObject && hit.collider != null)
            {
                PressSwitch();
            }
        }

	}

    void PressSwitch()
    {
        sound.Play();

        foreach (GameObject thing in things)
        {
            foreach (MonoBehaviour mono in thing.gameObject.GetComponents<MonoBehaviour>())
            {
                if (mono.isActiveAndEnabled)
                    mono.enabled = false;
                else
                    mono.enabled = true;
            }
        }
    }
}
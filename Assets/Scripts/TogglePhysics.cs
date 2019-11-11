using UnityEngine;
using System.Collections;

public class TogglePhysics : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}

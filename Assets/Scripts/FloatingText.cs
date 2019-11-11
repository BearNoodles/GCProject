using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    private GameObject player;
    public string text;
    public int drawDistance;
    private Vector3 scale;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<TextMesh>().text = text;
        gameObject.transform.localScale = new Vector3(1 / gameObject.transform.lossyScale.x,
           1 / gameObject.transform.lossyScale.y, 1 / gameObject.transform.lossyScale.z);
    }

    
	
	// Update is called once per frame
	void Update ()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude > drawDistance)
            gameObject.GetComponent<TextMesh>().text = "";
        else
            gameObject.GetComponent<TextMesh>().text = text;
        
        gameObject.transform.rotation = Quaternion.LookRotation(gameObject.transform.position - player.transform.position);
    }
}

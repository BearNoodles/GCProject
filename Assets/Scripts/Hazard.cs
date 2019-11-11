using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Hazard : MonoBehaviour {
    
    private Vector3 zeroPos;
    private float platformWidth, platformLength;
    private GameObject[,] platforms;
    private System.Random rng;
    private int columnNo;
    private bool countDown;
    
    private float destroyCounter;
    private GameObject toDestroy;

    public GameObject player;
    public GameObject platform;
    public int rows;
    public int columns;
    public int xGap, zGap;
    public float destroyTimer;

    private AudioSource sound;

    // Use this for initialization
    void Start ()
    {
        countDown = false;
        rng = new System.Random();
        columnNo = -1;
        zeroPos = gameObject.transform.position;
        platformWidth = platform.transform.GetChild(0).localScale.x;
        platformLength = platform.transform.GetChild(0).localScale.z;
        
        platforms = new GameObject[rows, columns];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                platforms[i,j] = (GameObject)Instantiate(platform, new Vector3(zeroPos.x + (platformWidth + xGap) * i, zeroPos.y,
                    zeroPos.z + (platformLength + zGap) * j), Quaternion.identity, gameObject.transform);

        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePlatforms();
    }

    void UpdatePlatforms()
    {
        int rand = rng.Next(0, rows);
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
            {
                if (player.transform.IsChildOf(platforms[i, j].transform) && columnNo < j)
                {
                    columnNo = j;
                    toDestroy = platforms[i, j];
                    destroyCounter = 0;
                    countDown = true;
                    for (int x = 0; x < rows - 1; x++)
                        platforms[(i + rand + x) % rows, (j + 1) % columns].SetActive(false);
                    sound.Play();
                }
                
            }
        if (countDown)
        {
            DestroyPlatform(toDestroy);
        }
    } 

    void DestroyPlatform(GameObject platform)
    {
        destroyCounter += Time.deltaTime;
        if (destroyCounter >= destroyTimer)
        {
            sound.Play();
            player.transform.parent = null;
            platform.SetActive(false);
            destroyCounter = 0;
            countDown = false;
        }
    }
}

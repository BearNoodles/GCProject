using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public float doorSpeed;
    private enum doorState {opening, closing, stationary};
    private doorState state;
    public enum OpenDir { x, y, z};
    public OpenDir dir;
    public bool isDoorOpenOnStart;
    private Vector3 startPos;

    public GameObject player;
    private Transform door;

    private AudioSource sound;

    // Use this for initialization
    void Start()
    {
        door = transform.GetChild(0);
        startPos = door.transform.position;
        state = isDoorOpenOnStart? doorState.opening : doorState.stationary;

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDoor();
    }


    void UpdateDoor()
    {
        switch (state)
        {
            case doorState.opening:
                OpenDoor();
                if (!sound.isPlaying)
                    sound.Play();
                break;
            case doorState.closing:
                CloseDoor();
                if (!sound.isPlaying)
                    sound.Play();
                break;
            default:
                sound.Stop();
                state = doorState.stationary;
                break;
        }
    }
    void OpenDoor()
    {
        Vector3 curPos = door.transform.position;
        float maxOpen = Mathf.Max(dir == OpenDir.x ? door.transform.lossyScale.x : 0,
                dir == OpenDir.y ? door.transform.lossyScale.y : 0,
                dir == OpenDir.z ? door.transform.lossyScale.z : 0);

        if (Mathf.Abs(curPos.x - startPos.x) >= maxOpen ||
                Mathf.Abs(curPos.y - startPos.y) >= maxOpen ||
                Mathf.Abs(curPos.z - startPos.z) >= maxOpen) //Set isOpen to false once maxOpen is reached
        {
            state = doorState.stationary;
        }

        else
        {
            if (Mathf.Abs(curPos.x - startPos.x) <= maxOpen ||
                Mathf.Abs(curPos.y - startPos.y) <= maxOpen ||
                Mathf.Abs(curPos.z - startPos.z) <= maxOpen)
            {
                curPos += new Vector3(dir == OpenDir.x ? doorSpeed * Time.deltaTime : 0,
                dir == OpenDir.y ? doorSpeed * Time.deltaTime : 0,
                dir == OpenDir.z ? doorSpeed * Time.deltaTime : 0);
            }
        }

        transform.GetChild(0).position = curPos;
    }

    void CloseDoor()
    {
        Vector3 curPos = door.transform.position;

        if (Mathf.Abs(curPos.x - startPos.x) >= doorSpeed * Time.deltaTime ||
            Mathf.Abs(curPos.y - startPos.y) >= doorSpeed * Time.deltaTime ||
            Mathf.Abs(curPos.z - startPos.z) >= doorSpeed * Time.deltaTime)
        {
            curPos -= new Vector3(dir == OpenDir.x ? doorSpeed * Time.deltaTime : 0,
            dir == OpenDir.y ? doorSpeed * Time.deltaTime : 0,
            dir == OpenDir.z ? doorSpeed * Time.deltaTime : 0);
        }

        else
            state = doorState.stationary;

        transform.GetChild(0).position = curPos;
    }
        


    

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            state = doorState.opening;
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            state = doorState.closing;
        }
    }
}

using UnityEngine;
using System.Collections;

public class InactiveDoor : MonoBehaviour {

    public enum CloseDir { x, y, z };
    public CloseDir dir;
    public float closeSpeed;
    public Vector3 closePos;

    private Transform door;
    

    // Use this for initialization
    void Start ()
    {
        door = transform.GetChild(0);
        closePos = door.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CloseDoor();	
	}

    void CloseDoor()
    {
        Vector3 curPos = door.transform.position;

        if (Mathf.Abs(curPos.x - closePos.x) >= closeSpeed * Time.deltaTime ||
            Mathf.Abs(curPos.y - closePos.y) >= closeSpeed * Time.deltaTime ||
            Mathf.Abs(curPos.z - closePos.z) >= closeSpeed * Time.deltaTime)
        {
            curPos -= new Vector3(dir == CloseDir.x ? closeSpeed * Time.deltaTime : 0,
            dir == CloseDir.y ? closeSpeed * Time.deltaTime : 0,
            dir == CloseDir.z ? closeSpeed * Time.deltaTime : 0);
        }
        
        transform.GetChild(0).position = curPos;
    }
}

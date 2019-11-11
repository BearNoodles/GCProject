using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject parent;
    private Rigidbody rb, rbParent;
    Vector3 mousePos;
    public int cameraSpeed;
    public int cameraRotSpeed;


    // Use this for initialization
    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rbParent = parent.GetComponent<Rigidbody>();
        mousePos = Input.mousePosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos;
        Vector3 rotx;
        Vector3 roty;
        Vector3 direction;

        


        pos = rbParent.velocity;
        if (Input.GetAxisRaw("W") == 1)
        {
            pos = transform.forward;
            if (Input.GetAxisRaw("A") == 1)
                pos = transform.forward -transform.right;
            else if (Input.GetAxisRaw("D") == 1)
                pos = transform.forward + transform.right;
        }
        else if (Input.GetAxisRaw("A") == 1)
        {
            pos = -transform.right;
            if (Input.GetAxisRaw("W") == 1)
                pos = transform.forward - transform.right;
            else if (Input.GetAxisRaw("S") == 1)
                pos = -transform.forward - transform.right;
        }
        else if (Input.GetAxisRaw("S") == 1)
        {
            pos = -transform.forward;
            if (Input.GetAxisRaw("A") == 1)
                pos = -transform.forward - transform.right;
            else if (Input.GetAxisRaw("D") == 1)
                pos = -transform.forward + transform.right;
        }
        else if (Input.GetAxisRaw("D") == 1)
        {
            pos = transform.right;
            if (Input.GetAxisRaw("W") == 1)
                pos = transform.forward + transform.right;
            else if (Input.GetAxisRaw("S") == 1)
                pos = -transform.forward + transform.right;
        }
        else
            pos = new Vector3(0, 0, 0);

        rbParent.velocity = pos * cameraSpeed;
        rb.velocity = pos * cameraSpeed;

        direction = Input.mousePosition - mousePos;
        rotx = new Vector3(0, 0, 0);
        roty = new Vector3(0, 0, 0);

        if (direction.x != 0 && rb.transform.rotation.y < 90 && rb.transform.rotation.y > -90)
        {
            roty.y = direction.x;
            //if (Input.GetAxisRaw("Horizontal") == 1)
            //rot.y = cameraSpeed;
            //else
            //rot.y = 0;
        }

        else
            roty.y = 0;

        if (direction.y != 0)
        {
            rotx.x = -direction.y;
            //if (Input.GetAxisRaw("Horizontal") == 1)
            //rot.y = cameraSpeed;
            //else
        }

        else
            rotx.x = 0;
        
        rb.transform.Rotate(rotx);
        rbParent.transform.Rotate(roty);
        /*
        direction = Input.mousePosition - mousePos;
        direction.Normalize();
        rb.transform.Rotate(direction);
        */
        //rb.transform.RotateAroundLocal(rb.transform.position, direction.magnitude);

        mousePos = Input.mousePosition;

        Debug.Log(mousePos);
    }
}

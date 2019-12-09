using UnityEngine;
using System.Collections;

public class KeepSound : MonoBehaviour {
    

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 4)
        {
            Destroy(gameObject);
        }
    }
}

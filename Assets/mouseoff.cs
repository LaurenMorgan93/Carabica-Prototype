using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseoff : MonoBehaviour
{
    public bool mouseturnoff = true;
    void Start()
    {
        if(mouseturnoff)
        {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

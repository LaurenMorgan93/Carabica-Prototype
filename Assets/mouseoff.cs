using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseoff : MonoBehaviour
{
    public bool mouseturnoff = true;
    public Button restartButton;

    public float initTime;

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

        initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") || Time.time >= initTime+30)
        {
            restartButton.onClick.Invoke();
        }
    }
}

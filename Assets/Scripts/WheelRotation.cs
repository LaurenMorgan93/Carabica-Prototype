using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float rotationStrenght = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = new Vector3(0, 0, Input.GetAxis("Horizontal") * rotationStrenght);
        transform.eulerAngles = newRotation;
    }
}

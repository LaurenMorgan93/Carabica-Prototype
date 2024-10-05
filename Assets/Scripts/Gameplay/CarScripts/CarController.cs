using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody _rb;

    public float drivingForce;

    public float turnSpeed;
    public float maxRotation = 45;

    public float travelSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        NonWheelApproach();
    }

    private void NonWheelApproach()
    {
        _rb.AddForce(drivingForce * transform.right * Input.GetAxisRaw("Horizontal"), ForceMode.Acceleration);

        //transform.position += Input.GetAxisRaw("Horizontal") * transform.right * drivingForce * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(0, maxRotation * Input.GetAxisRaw("Horizontal"), 0), Time.fixedDeltaTime * turnSpeed * Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }
}

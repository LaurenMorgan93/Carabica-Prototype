using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    static public CarController instance;

    private Rigidbody _rb;

    public float drivingForce;

    public float turnSpeed;
    public float maxRotation = 45;

    public float travelSpeed;
    public float travelSpeedAcceleration = 1;

    public float awfulyHotCoffeePotSpeed;

    public Transform awfulyHotCoffeePot;
    public Rigidbody potRB;

    public Vector2 randomForceRange;

    public Score scoreManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        instance = this;
    }

    private void FixedUpdate()
    {
        NonWheelApproach();
    }

    private void NonWheelApproach()
    {
        _rb.AddForce(drivingForce * transform.right * Input.GetAxisRaw("Horizontal"), ForceMode.Acceleration);
        potRB.AddForce(Random.Range(randomForceRange.x, randomForceRange.y) * awfulyHotCoffeePot.right * Input.GetAxisRaw("Horizontal"), ForceMode.Impulse);
        potRB.AddForce(Random.Range(randomForceRange.x/5, randomForceRange.y/5) * awfulyHotCoffeePot.forward * -Mathf.Abs( Input.GetAxisRaw("Horizontal")), ForceMode.Impulse);

        awfulyHotCoffeePot.transform.position = new Vector3(awfulyHotCoffeePot.transform.position.x + Input.GetAxisRaw("Horizontal") * Time.deltaTime * awfulyHotCoffeePotSpeed, awfulyHotCoffeePot.transform.position.y, awfulyHotCoffeePot.transform.position.z);

        //transform.position += Input.GetAxisRaw("Horizontal") * transform.right * drivingForce * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(0, maxRotation * Input.GetAxisRaw("Horizontal"), 0), Time.fixedDeltaTime * turnSpeed * Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }

    private void Update()
    {
        travelSpeed += travelSpeedAcceleration * Time.deltaTime;
        scoreManager.SpeedMultiplier = travelSpeed/10;
    }
}

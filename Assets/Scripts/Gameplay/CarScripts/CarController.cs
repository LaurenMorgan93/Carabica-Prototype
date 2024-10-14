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

    public Transform awfulyHotCoffeePot, pourPot, potParticles;
    public Vector2 maxSides;

    public Rigidbody potRB;

    public Vector2 randomForceRange;

    public Score scoreManager;

    public float maxSpeed;
    public float decelarationRate; 
    public float speedDecelGap;

    public bool isUnderEffect;

    public ParticleSystemForceField pSForce;

    public float particleForceIntensity;

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
        potRB.AddForce(Random.Range(randomForceRange.x, randomForceRange.y) * awfulyHotCoffeePot.right * Input.GetAxisRaw("Horizontal") * 1.5f, ForceMode.Impulse);
        potRB.AddForce(Random.Range(randomForceRange.x/4, randomForceRange.y/4) * awfulyHotCoffeePot.forward * -Mathf.Abs( Input.GetAxisRaw("Horizontal")), ForceMode.Impulse);

        print(Input.GetAxisRaw("Horizontal"));

        pSForce.directionX = Input.GetAxisRaw("Horizontal") * particleForceIntensity;

        //awfulyHotCoffeePot.rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * 50), Time.deltaTime * awfulyHotCoffeePotSpeed));
    }

    Vector3 lastMousePos;
    Vector3 mousePositionDelta;

    private void Start()
    {
        mousePositionDelta = Input.mousePosition;
    }

    private void Update()
    {
        if (maxSpeed > travelSpeed)
        {
            travelSpeed += travelSpeedAcceleration * Time.deltaTime;
        }
        else if (travelSpeed + speedDecelGap >  maxSpeed)
        {
            travelSpeed -= decelarationRate * Time.deltaTime;
        }
        mousePositionDelta = Input.mousePosition - lastMousePos;

        
        scoreManager.SpeedMultiplier = travelSpeed/10;

        //pourPot.eulerAngles = new Vector3(0, 0, Mathf.Clamp(0 + mousePositionDelta.x, -30, 30));
        //potParticles.eulerAngles = new Vector3(0, 0, Mathf.Clamp(potParticles.eulerAngles.z + mousePositionDelta.x/50, -30, 30));

        pourPot.localPosition = new Vector3(Mathf.Clamp(pourPot.localPosition.x + mousePositionDelta.x/100, maxSides.x, maxSides.y), pourPot.localPosition.y, 0);

        //awfulyHotCoffeePot.eulerAngles = new Vector3(0, 0, Mathf.Lerp(0, Input.GetAxisRaw("Horizontal") * 50, Time.deltaTime * awfulyHotCoffeePotSpeed));

        lastMousePos = Input.mousePosition;
    }
}

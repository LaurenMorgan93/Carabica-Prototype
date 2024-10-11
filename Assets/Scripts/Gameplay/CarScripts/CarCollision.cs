using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private Rigidbody _rb;
    private CarController _carController;

    public Score scoreManager;
    private GameObject _lastCollidedObject;

    public float cupPropulsionForce;

    public float collisionSleepWakeUp = 5f;

    private CoffeePot _coffeePot;

    private CarSoundManager _carSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carController = GetComponent<CarController>();
        _carSound = GetComponent<CarSoundManager>();

        _coffeePot = FindObjectOfType<CoffeePot>().GetComponent<CoffeePot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.gameObject.name);

        if (collision.collider.gameObject != _lastCollidedObject && (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Wall")))
        {
            _carController.potRB.AddExplosionForce(cupPropulsionForce * 1000, _carController.awfulyHotCoffeePot.position - Vector3.down*2, 10, 40);

            _carController.travelSpeed /= 2;

            scoreManager.TakeDamage();

            _coffeePot.sleepStatus += collisionSleepWakeUp;

            _lastCollidedObject = collision.collider.gameObject;

            //print(_lastCollidedObject);
            
            _carSound.PlayCrashEffect();
        }
    }
}

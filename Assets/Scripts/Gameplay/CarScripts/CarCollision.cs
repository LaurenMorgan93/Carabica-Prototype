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

    private bool canBeHit;

    public float hitCoolDown;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carController = GetComponent<CarController>();
        _carSound = GetComponent<CarSoundManager>();

        _coffeePot = FindObjectOfType<CoffeePot>().GetComponent<CoffeePot>();

        canBeHit = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.gameObject.name);

        if(canBeHit)
        {
        if (collision.collider.gameObject != _lastCollidedObject && (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Wall")))
        {
            _carController.potRB.AddExplosionForce(cupPropulsionForce * 1000, _carController.awfulyHotCoffeePot.position - Vector3.down*2, 10, 40);

            _carController.travelSpeed /= 2;

            scoreManager.TakeDamage();

            _coffeePot.sleepStatus += collisionSleepWakeUp;

            _lastCollidedObject = collision.collider.gameObject;

            //print(_lastCollidedObject);

            if (collision.collider.CompareTag("Obstacle")){
                var objectRB = _lastCollidedObject.GetComponent<Rigidbody>();
                objectRB.constraints = RigidbodyConstraints.None;
                //objectRB.AddTorque(_lastCollidedObject.transform.forward * 20);
                objectRB.AddForce(new Vector3(0, 0.2f, 1f) * 1000);
            }

            _carSound.PlayCrashEffect();

            StartCoroutine("hitCoolDownTimer");
        }
        }
    }

    IEnumerator hitCoolDownTimer()
    {
        canBeHit = false;

        yield return new WaitForSeconds(hitCoolDown);

        canBeHit = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private Rigidbody _rb;
    private CarController _carController;

    public Score scoreManager;
    private GameObject _lastCollidedObject;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _carController = GetComponent<CarController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.gameObject.name);

        if (collision.collider.gameObject != _lastCollidedObject && (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Wall")))
        {
            _carController.travelSpeed /= 2;

            scoreManager.TakeDamage();

            _lastCollidedObject = collision.collider.gameObject;

            print(_lastCollidedObject);
        }
    }
}

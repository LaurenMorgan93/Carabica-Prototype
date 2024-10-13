using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pedestrian : MonoBehaviour
{
    public RoadPiece m_RoadPiece;
    public int m_Lane;

    private int _pieceMovementTrigger;

    public Vector2 randomRange;

    private float _speed;

    public bool isInitialised = false;

    private UnityEvent Reached = new UnityEvent();

    private Coroutine movementRoutine;

    private void Awake()
    {
        _speed = Random.Range(randomRange.x, randomRange.y);
        _pieceMovementTrigger = Random.Range(3, 9);

        Reached.AddListener(StartMovement);
    }

    private void Update()
    {
        if(m_RoadPiece == RoadManager.instance.roadPieces[_pieceMovementTrigger] && isInitialised && movementRoutine == null)
        {
            Reached.Invoke();
        }
    }

    public void StartMovement()
    {
        movementRoutine = StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            if (m_Lane == 0)
                transform.position += transform.right * _speed * Time.deltaTime;
            else
                transform.position += transform.right * -_speed * Time.deltaTime;

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }

    }
}

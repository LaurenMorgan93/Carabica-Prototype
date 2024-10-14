using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    public CarController car;

    public Vector2 speedRange;
    public Vector2 angleRange;
    

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, -MathEva.map(car.travelSpeed, speedRange.x, speedRange.y, angleRange.x, angleRange.y));
    }
}

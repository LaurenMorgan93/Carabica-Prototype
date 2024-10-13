using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyelidManager : MonoBehaviour
{
    public float currentSleepLevel;
    public CoffeePot coffeePotScript;

    public AnimationCurve eyelidTravelCurve;

    public Transform[] eyelidTransforms;
    public Vector3[] eyelidStartPos;

    public float[] EndPoints;

    private float currentSleepLerped;
    public float lerpSpeed = 0.2f;
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            eyelidStartPos[i] = eyelidTransforms[i].localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentSleepLevel = 1 - (coffeePotScript.sleepStatus / 100);

        currentSleepLerped = Mathf.Lerp(currentSleepLerped, eyelidTravelCurve.Evaluate(currentSleepLevel), lerpSpeed);

        for (int i = 0; i < 2; i++)
        {
            eyelidTransforms[i].localPosition = new Vector3(eyelidTransforms[i].position.x, Mathf.Lerp(eyelidStartPos[i].y, EndPoints[i], currentSleepLerped
                ), 0 );
        }
    }
}

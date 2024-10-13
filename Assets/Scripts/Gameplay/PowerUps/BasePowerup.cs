using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BasePowerup : MonoBehaviour
{
    static public BasePowerup instance;

    private PostProcessVolume _volume;
    public float enterTime;

    [Header("Beer Values")]
    public float drunkTime;
    public float driftMultiplier;
    public Vector2 dizzyScale;
    public float dizzynessEffect;

    [Header("Ciggy Values")]
    public float nicotineTime;
    public float carSlowDownRate;

    public Animator actionAnimator;

    private IEnumerator Effect(bool isBeer)
    {
        CarController.instance.isUnderEffect = true;

        var chromaticAberation = ScriptableObject.CreateInstance<ChromaticAberration>();
        var lensDistortion = ScriptableObject.CreateInstance<LensDistortion>();

        chromaticAberation.enabled.Override(true);
        lensDistortion.enabled.Override(true);

        chromaticAberation.intensity.Override(0);

        lensDistortion.intensity.Override(0);
        lensDistortion.intensityX.Override(1);
        lensDistortion.intensityY.Override(1);
        lensDistortion.scale.Override(1);

        //.scale.Override(1);

        var volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 0, lensDistortion, chromaticAberation);


        if (isBeer)
        {
            Score.instance.CurrentHitPoints++;

            float defaultCarTurnSpeed = CarController.instance.turnSpeed;

            for (float i = 0; i <= enterTime; i += Time.deltaTime)
            {
                chromaticAberation.intensity.value = Mathf.Lerp(0, 0.5f, i / enterTime);
                lensDistortion.scale.value = Mathf.Lerp(1, dizzyScale.x, i / enterTime);
                lensDistortion.intensity.value = Mathf.Lerp(0, -50, i / enterTime);
                CarController.instance.turnSpeed = Mathf.Lerp(defaultCarTurnSpeed, defaultCarTurnSpeed * driftMultiplier, i / enterTime);

                yield return null;
            }

            for(float i = 0; i <= drunkTime; i += Time.deltaTime)
            {
                lensDistortion.intensityX.value = Mathf.Abs(Mathf.Sin(Time.time * 5) * dizzynessEffect/10);
                lensDistortion.intensityY.value = Mathf.Abs(Mathf.Sin(Time.time / 3) * dizzynessEffect/10);

                lensDistortion.scale.value = Mathf.Clamp(Mathf.Abs(Mathf.Sin(Time.time /2)), dizzyScale.x, dizzyScale.y);

                yield return null;
            }

            float lastLensXDist = lensDistortion.intensityX.value;
            float lastLensYDist = lensDistortion.intensityY.value;

            float lastLensScale = lensDistortion.scale.value;   

            for (float i = 0; i <= enterTime; i += Time.deltaTime)
            {
                chromaticAberation.intensity.value = Mathf.Lerp(0.5f, 0, i / enterTime);
                lensDistortion.intensity.value = Mathf.Lerp(-50, 0, i / enterTime);
                lensDistortion.intensityY.value = Mathf.Lerp(lastLensYDist, 1, i / enterTime);
                lensDistortion.intensityX.value = Mathf.Lerp(lastLensXDist, 1, i / enterTime);
                lensDistortion.scale.value = Mathf.Lerp(lastLensScale, 1, i / enterTime);

                CarController.instance.turnSpeed = Mathf.Lerp(defaultCarTurnSpeed * driftMultiplier, defaultCarTurnSpeed, i / enterTime);

                yield return null;
            }

        } else
        {

            float lastCarSpeed = CarController.instance.travelSpeed;
            float defaultCarAcceleration = CarController.instance.travelSpeedAcceleration;

            CarController.instance.travelSpeedAcceleration = 0;

            for (float i = 0; i <= enterTime; i += Time.deltaTime)
            {
                CarController.instance.travelSpeed = Mathf.Lerp(lastCarSpeed, lastCarSpeed / carSlowDownRate, i/enterTime);

                lensDistortion.intensity.value = Mathf.Lerp(0, -70, i / enterTime);
                chromaticAberation.intensity.value = Mathf.Lerp(0, 0.5f, i / enterTime);

                yield return null;
            }

            print(lensDistortion.intensity.value);

            print("done");

            CarController.instance.travelSpeedAcceleration = defaultCarAcceleration/carSlowDownRate;

            yield return new WaitForSeconds(nicotineTime);

            for (float i = 0; i <= enterTime; i += Time.deltaTime)
            {
                CarController.instance.travelSpeed = Mathf.Lerp(lastCarSpeed/carSlowDownRate, lastCarSpeed, i/enterTime);

                lensDistortion.intensity.value = Mathf.Lerp(-70, 0, i / enterTime);
                chromaticAberation.intensity.value = Mathf.Lerp(0.5f, 0, i / enterTime);

                yield return null;
            }

            CarController.instance.travelSpeedAcceleration = defaultCarAcceleration;
        }

        CarController.instance.isUnderEffect = false;
        RuntimeUtilities.DestroyVolume(volume, true, false);
    }

    public void StartEffect(bool isBeer)
    {
        StartCoroutine(Effect(isBeer));

        if(isBeer)
        {
            actionAnimator.SetTrigger("Beer");
        }
        else
        {   
            actionAnimator.SetTrigger("Smoke");
        }

    }

    private void Awake()
    {
        instance = this;
    }

    //private void Start()
    //{
    //    StartEffect(true);
    //}
}

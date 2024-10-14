using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CoffeePot : MonoBehaviour  // this script is attached to the coffee pot object
{
    //coffee variables
    public float coffeeLevel = 0; // Current coffee level 
    public float maxCoffeeLevel = 100f; // Maximum capacity of the coffee pot
    public Slider coffeeMeter;
    public float coffeeGainPerParticle = 0.1f;
    public SpriteRenderer cupSprite;
    public Sprite[] cupSprites;

    public CarController car;
    public float speedIncrease;

    public AnimationHandlerScript animHandleScript;

    public Transform spawnPoint;
    public Transform offPoint;

    // sleepiness variables 
    public float sleepStatus = 100; // 100 = awake, 0 = asleep
    public float sleepDrainRate; // the rate that you get sleepy without DRINKING coffee. You will continue to get sleepy while filling the coffee cup. 

    public Vector3 startPosition;

    private AudioSource _audioSource;
    void Awake()
    {
        startPosition = transform.position;

        _audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        transform.position = startPosition;
    }
    
    private void OnParticleCollision(GameObject other)
    {
        // Check if the other colliding object is coffee 
        if (other.CompareTag("Coffee"))
        {
            FillCoffee();
        }
    }

    private int FillStage(int ammountOfStages, float maxCoffee, float currentCoffee)
    {
        float stageDiv = maxCoffee / ammountOfStages;

        if (currentCoffee / stageDiv > ammountOfStages)
        {
            return ammountOfStages - 1;
        }

        return (int) (currentCoffee / stageDiv);
    }

 

    private void FillCoffee()
    {
        // Increase coffee level 
        if (coffeeLevel < maxCoffeeLevel)
        {
            coffeeLevel += coffeeGainPerParticle; // Adjust this to change how quickly the pot fills
           // Debug.Log("Coffee Level: " + coffeeLevel);
            coffeeMeter.value = coffeeLevel;

            _audioSource.Play();
            
        }

        if (coffeeLevel >= maxCoffeeLevel)
        {
            // if you fill the coffeee cup , call the drinking coffee logic here 
            animHandleScript.TriggerCoffeeAnim();

            transform.position = offPoint.position;
            
            //EmptyCoffee();
        }
    }

    public void EmptyCoffee()
    {
        // reset sleepiness here 
        sleepStatus = 100;
        
        // empty the coffee pot and start again
        coffeeLevel = 0f;
        coffeeMeter.value = coffeeLevel;

        car.maxSpeed += speedIncrease;

        transform.position = spawnPoint.position;
    }

    public  void ManageSleepStatus() //this function is set to repeat in Start
    {
        if (sleepStatus > 0)
        {
            // drain the sleepStatus (100 = awake 0= asleep)
            sleepStatus -= sleepDrainRate; // your sleep level is drained by the sleep drain rate until you drink coffee 
        
            // add logic here to update the eyes closing based on current sleep level 
        }
    }

    private void FixedUpdate()
    {
        ManageSleepStatus();
        cupSprite.sprite = cupSprites[FillStage(cupSprites.Length, maxCoffeeLevel, coffeeLevel)];
    }
}
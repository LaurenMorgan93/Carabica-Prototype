using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CoffeePot : MonoBehaviour  // this script is attached to the coffee pot object
{
    public float coffeeLevel = 0; // Current coffee level 
    public float maxCoffeeLevel = 100f; // Maximum capacity of the coffee pot

    public Slider coffeeMeter;
    
    private void OnParticleCollision(GameObject other)
    {
        // Check if the other colliding object is coffee 
        if (other.CompareTag("Coffee"))
        {
            FillCoffee();
        }
    }

 

    private void FillCoffee()
    {
        // Increase coffee level 
        if (coffeeLevel < maxCoffeeLevel)
        {
            coffeeLevel += 0.1f; // Adjust this to change how quickly the pot fills
            Debug.Log("Coffee Level: " + coffeeLevel);
            coffeeMeter.value = coffeeLevel;
        }

        if (coffeeLevel >= maxCoffeeLevel)
        {
            // drinking coffeee logic // reset the meter
            EmptyCoffee();
        }
    }

    public void EmptyCoffee()
    {
        // enter drinking logic here
        
        // empty the coffee pot and start again
        coffeeLevel = 0f;
        coffeeMeter.value = coffeeLevel;
    }
}
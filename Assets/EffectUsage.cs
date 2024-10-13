using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUsage : MonoBehaviour
{
    public CoffeePot coffeePot;

    public void EmptyCoffee()
    {
        coffeePot.EmptyCoffee();
    }

    public void StartBeer()
    {
        BasePowerup.instance.StartEffectTrue(true);
    }

    public void StartSmoke()
    {
        BasePowerup.instance.StartEffectTrue(false);
    }
}

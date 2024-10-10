using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandlerScript : MonoBehaviour
{
    public Animator animatorHandler;

    public void TriggerCoffeeAnim()
    {
        print("what the simgoid");
        animatorHandler.SetTrigger("Coffee");
    }

}

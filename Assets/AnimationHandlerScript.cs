using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandlerScript : MonoBehaviour
{
    public Animator animatorHandler;

    private float timer;

    private void FixedUpdate()
    {
        timer -= 0.1f;
    }

    public void TriggerCoffeeAnim()
    {
        if(timer < 0)
        {
            animatorHandler.SetTrigger("Coffee");
            timer = 3f;
        }
    }

}

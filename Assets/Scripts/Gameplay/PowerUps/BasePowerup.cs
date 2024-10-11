using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerup : MonoBehaviour
{
    public float effectPower;

    public virtual void Effect() { }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            Effect();
    }
}

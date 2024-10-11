using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerup : MonoBehaviour
{
    public bool isBeer;
    public float effectPower;

    public void Effect()
    {
        if (isBeer)
        {
            Score.instance.CurrentHitPoints++;
        } else
        {

        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            Effect();
    }
}

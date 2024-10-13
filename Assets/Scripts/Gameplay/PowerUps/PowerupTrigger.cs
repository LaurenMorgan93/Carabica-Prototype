using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTrigger : MonoBehaviour
{
    public bool isBeer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            BasePowerup.instance.StartEffect(isBeer);
        }

        Destroy(gameObject);
    }
}

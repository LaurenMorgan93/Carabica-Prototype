using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLane : MonoBehaviour
{
    public Transform bottom, top;
    public Transform interactableContainer;

    public List<GameObject> interactables = new List<GameObject>();

    public float spawnInteractavleCheckRadius;

    public bool CheckForNearbyInteractables(Vector3 pos)
    {
        return Physics.CheckSphere(pos, spawnInteractavleCheckRadius, LayerMask.GetMask("Obstacles", "Interactables"));
    }

    public Vector3 CreateSpawnPosition()
    {
        return new Vector3(transform.position.x, 0, Random.Range(bottom.position.z, top.position.z));
    }

    public void CreateObjectInLane(GameObject obj)
    {
        Vector3 spawnPos = CreateSpawnPosition();

        while(CheckForNearbyInteractables(spawnPos))
        {
            spawnPos = CreateSpawnPosition();
        }

        Instantiate(obj, spawnPos, obj.transform.rotation, interactableContainer);
    }
}

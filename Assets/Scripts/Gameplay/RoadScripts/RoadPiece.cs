using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPiece : MonoBehaviour
{
    public Transform endRoad;

    public List<RoadLane> roadLanes = new List<RoadLane>();

    //TO BE CHANGED ~ FOR FIRST PLAYTEST ONLY
    public void SpawnObstacle(GameObject obstacle, int count, int laneID = -1)
    {
        int initialCount = 0;

        if(laneID == -1)
        {
            while(initialCount < count)
            {
                int randomSeed = Random.Range(0, 4);

                roadLanes[randomSeed].CreateObjectInLane(obstacle);

                initialCount++;
            }
        }
    }

    public void SpawnPowerup(GameObject powerupPrefab, int laneID =-1)
    {

    }
}

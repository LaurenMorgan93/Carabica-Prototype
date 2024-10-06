using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public CarController car;

    public GameObject roadPrefab;
    public List<RoadPiece> roadPieces;

    public int chunkSpawnCount;

    public List<GameObject> obstacles;

    private void CheckFirstChunk()
    {
        var piece = roadPieces[0];

        if (piece.endRoad.position.z <= car.transform.position.z-10)
        {
            var roadObj = Instantiate(roadPrefab, roadPieces[roadPieces.Count-1].endRoad.position, roadPrefab.transform.rotation, transform);

            roadPieces.Add(roadObj.GetComponent<RoadPiece>());

            if(chunkSpawnCount % 3 == 0)
            {
                int count = 1 + (int)(chunkSpawnCount / 9);

                roadPieces[roadPieces.Count - 1].SpawnObstacle(obstacles[0], count);
            }

            roadPieces.RemoveAt(0);

            Destroy(piece.gameObject);

            chunkSpawnCount++;
        }
    } 

    private void Update()
    {
        CheckFirstChunk();

        foreach (var piece in roadPieces)
        {
            piece.transform.position += -piece.transform.forward * Time.deltaTime * car.travelSpeed;
        }
    }
}

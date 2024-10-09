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

    public Transform tower;

    private void CheckFirstChunk()
    {
        var piece = roadPieces[0];

        if (piece.endRoad.position.z <= car.transform.position.z-10)
        {
            var roadObj = Instantiate(roadPrefab, roadPieces[roadPieces.Count-1].endRoad.position, roadPrefab.transform.rotation, transform);

            roadPieces.Add(roadObj.GetComponent<RoadPiece>());

            roadPieces.RemoveAt(0);

            Destroy(piece.gameObject);

            chunkSpawnCount++;
        }
    }

    private void ObstacleGenerationLogic()
    {

    }

    private void Update()
    {
        CheckFirstChunk();

        foreach (var piece in roadPieces)
        {
            piece.transform.position += -piece.transform.forward * Time.deltaTime * car.travelSpeed;
        }

        tower.position += Vector3.up * car.travelSpeed / 10 * Time.deltaTime;
        tower.localScale += Vector3.one * Time.deltaTime * car.travelSpeed/10 * Time.deltaTime;
    }
}

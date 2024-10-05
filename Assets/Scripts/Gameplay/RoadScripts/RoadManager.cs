using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public CarController car;

    public GameObject roadPrefab;
    public List<RoadPiece> roadPieces;

    private void CheckFirstChunk()
    {
        var piece = roadPieces[0];

        if (piece.endRoad.position.z <= car.transform.position.z-5)
        {
            var roadObj = Instantiate(roadPrefab, roadPieces[roadPieces.Count-1].endRoad.position, roadPrefab.transform.rotation, transform);

            roadPieces.Add(roadObj.GetComponent<RoadPiece>());

            roadPieces.RemoveAt(0);

            Destroy(piece.gameObject);
        }
    } 

    private void Update()
    {
        CheckFirstChunk();

        foreach (var piece in roadPieces)
        {
            piece.transform.position += -piece.transform.forward * Time.deltaTime * carSpeed;
        }
    }
}

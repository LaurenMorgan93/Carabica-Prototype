using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    static public RoadManager instance;

    public Score scoreManager;

    public CarController car;

    public GameObject roadPrefab;
    public List<RoadPiece> roadPieces;

    public int chunkRateUpgradeDistance = 15;
    public int minObstacleChunkRate = 3;
    public int maxObstacleChunkRate = 10;
    private int _chunkRate;

    public int chunkSpawnCount;

    public int obstacleIncreseLevel = 30;
    public int maxObstacleChunkCount = 10;
    private int _currentObstacleChunkCount;

    public List<GameObject> staticObstacles, movingObstacles, powerups = new List<GameObject>();

    public Transform tower;

    private int _lastSpawnedPowerup = -1;

    public RoadPiece currentPowerupChunk = null;

    private void CheckFirstChunk()
    {
        var piece = roadPieces[0];

        if (piece.endRoad.position.z <= car.transform.position.z - 10)
        {
            var roadObj = Instantiate(roadPrefab, roadPieces[roadPieces.Count - 1].endRoad.position, roadPrefab.transform.rotation, transform);

            roadPieces.Add(roadObj.GetComponent<RoadPiece>());
            roadPieces.RemoveAt(0);

            GenerationLogic(roadObj.GetComponent<RoadPiece>());

            Destroy(piece.gameObject);

            chunkSpawnCount++;
        }
    }

    public int PowerupSpawnCheck()
    {
        if (!car.isUnderEffect && currentPowerupChunk == null)
        {
            if(scoreManager.CurrentHitPoints < scoreManager.MaxHitPoints && _lastSpawnedPowerup != 0)
            {
                return 0;
            }

            if (_lastSpawnedPowerup != 1 && scoreManager.DistanceTravelled > 15)
            {
                return 1;
            }
        }

        return -1;
    }

    private void GenerationLogic(RoadPiece spawnedChunk)
    {
        if (chunkSpawnCount % _chunkRate == 0)
        {
            print("aaaa");
            var generations = _currentObstacleChunkCount;
            int randomLane = 0;

            _lastSpawnedPowerup = PowerupSpawnCheck();

            if (_lastSpawnedPowerup >= 0 && chunkSpawnCount != 0)
            {
                generations--;

                randomLane = Random.Range(0, 4);

                spawnedChunk.SpawnPowerup(powerups[_lastSpawnedPowerup], randomLane);
            }

            for (int i = 0; i < generations; ++i)
            {
                randomLane = Random.Range(0, 4);

                bool isStaticObstacle = true;

                if (scoreManager.DistanceTravelled > 5 && Random.value > 0.5f)
                    isStaticObstacle = false;

                int prefabChoice = Random.Range(0, staticObstacles.Count);

                if (!isStaticObstacle)
                {
                    prefabChoice = Random.Range(0, movingObstacles.Count);

                    if (Random.value > 0.5)
                        randomLane = 0;
                    else
                        randomLane = 3;

                    Pedestrian pedestrian = spawnedChunk.SpawnObstacle(movingObstacles[prefabChoice], 1, randomLane).GetComponent<Pedestrian>();

                    pedestrian.m_RoadPiece = spawnedChunk;
                    pedestrian.m_Lane = randomLane;
                    pedestrian.isInitialised = true;

                    continue;
                }

                spawnedChunk.SpawnObstacle(staticObstacles[prefabChoice], 1, randomLane);

                print("obstacle");
            }
        }

        if (_currentObstacleChunkCount < maxObstacleChunkCount && chunkSpawnCount % obstacleIncreseLevel == 0)
        {
            _currentObstacleChunkCount++;
        }

        if (_chunkRate > minObstacleChunkRate && scoreManager.DistanceTravelled % chunkRateUpgradeDistance == 0)
        {
            _chunkRate--;
        }

    }

    private void Awake()
    {
        instance = this;

        _currentObstacleChunkCount = 1;
        _chunkRate = maxObstacleChunkRate;
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

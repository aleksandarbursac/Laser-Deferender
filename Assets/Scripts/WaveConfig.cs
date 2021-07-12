using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveSpeedLastWaypoint = 2f;

    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; set => timeBetweenSpawns = value; }
    public float SpawnRandomFactor { get => spawnRandomFactor; set => spawnRandomFactor = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float MoveSpeedLastWaypoint { get => moveSpeedLastWaypoint; set => moveSpeedLastWaypoint = value; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoint = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoint.Add(child);
        }
        return waveWaypoint;
    }
}

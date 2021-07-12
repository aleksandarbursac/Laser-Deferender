using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (int currEnemy = 0; currEnemy < wave.NumberOfEnemies; currEnemy++)
        {
            var newEnemy =
            Instantiate(wave.EnemyPrefab,
                wave.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
            yield return new WaitForSeconds(wave.TimeBetweenSpawns + Random.Range(0f, wave.SpawnRandomFactor));
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        //isnt it Count - 1
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

}

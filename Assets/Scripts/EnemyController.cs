using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { NULL, Rat, FastRat }

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [SerializeField] private List<EnemyBehavior> enemies;
    [SerializeField] private List<Transform> spawns;
    [SerializeField] private List<Wave> waves;

    private int enemyDefeatedCount = 0;
    private int currentWave = -1;
    private List<EnemyPool> pools = new List<EnemyPool>();



    private void Start()
    {
        instance = this;

        pools.AddRange(new List<EnemyPool>(GetComponentsInChildren<EnemyPool>(true)));
        foreach (EnemyPool pool in pools) enemies.AddRange(pool.GetAll());
    }

    public static void OnEnemyDefeated()
    {
        instance.enemyDefeatedCount++;

        if (instance.enemyDefeatedCount >= instance.waves[instance.currentWave].enemies.Count) 
        {
            if (instance.currentWave + 1 >= instance.waves.Count) GameController.GameOver();
            else SpawnNextWave();
        }
    }

    public static void SpawnNextWave()
    {
        instance.StopCoroutine(instance.SpawnNextWaveCoroutine());
        instance.StartCoroutine(instance.SpawnNextWaveCoroutine());
    }

    private IEnumerator SpawnNextWaveCoroutine()
    {
        currentWave++;
        Wave wave = waves[currentWave];
        enemyDefeatedCount = 0;

        Debug.Log("Spawning wave " + wave.name);
        
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < wave.enemies.Count; i++)
        {
            Debug.Log("Spawning enemy " + wave.enemies[i].ToString());
            Vector2 spawnPos = GetRandomSpawn();
            pools[(int)wave.enemies[i] - 1].GetNext().Spawn(spawnPos);

            yield return new WaitForSeconds(wave.cooldown);
        }
    }

    private Vector2 GetRandomSpawn()
    {
        return spawns[Random.Range(0, spawns.Count)].position;
    }
}
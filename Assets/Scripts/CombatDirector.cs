using System;
using System.Collections;
using UnityEngine;

public class CombatDirector : MonoBehaviour
{
    public int Difficulty;
    public int SpawnInterval;
    private EnemySpawner _enemySpawner;

    private IEnumerator _startSpawning;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.Difficulty = Difficulty;
        _startSpawning = StartSpawning();
    }

    private void Start()
    {
        StartCoroutine(_startSpawning);
    }

    private IEnumerator StartSpawning()
    {
        _enemySpawner.Spawn(_enemySpawner.RockGolem);
        yield return new WaitForSeconds(SpawnInterval);
        StartCoroutine(StartSpawning());
        yield return null;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatDirector : MonoBehaviour
{
    public int Difficulty = 1;
    public int DifficultyTimer = 30;

    public int FirstSpawn = 10;
    public int SpawnInterval = 7;
    public Transform EnemyContainer;
    
    private EnemySpawner _enemySpawner;
    private IEnumerator _startSpawning;

    private List<Enemy> _mobList;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.SetEnemyContainer(EnemyContainer);
        _startSpawning = StartSpawning();
    }

    private void Start()
    {
        _mobList = new List<Enemy>() {_enemySpawner.RockGolem, _enemySpawner.Wolf, _enemySpawner.Ghost};
        StartCoroutine(_startSpawning);
        StartCoroutine(IncreaseDifficulty());
    }

    private IEnumerator IncreaseDifficulty()
    {
        Difficulty++;
        yield return new WaitForSeconds(DifficultyTimer);
        StartCoroutine(IncreaseDifficulty());
    }
    
    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(FirstSpawn);
        StartCoroutine(SpawnContinuous());
    }
    private IEnumerator SpawnContinuous()
    {
        GenerateMobs();
        yield return new WaitForSeconds(SpawnInterval);
        StartCoroutine(SpawnContinuous());
    }

    private void GenerateMobs()
    {
        switch (Difficulty)
        {
            case 1:
                _enemySpawner.Spawn(_enemySpawner.RockGolem);
                break;
            case 2:
                _enemySpawner.Spawn(_mobList[Random.Range(0,2)]);
                SpawnInterval = 5;
                break;
            case 3:
                _enemySpawner.Spawn(_mobList[Random.Range(0,3)]);
                SpawnInterval = 3;
                break;
        }
    }
}
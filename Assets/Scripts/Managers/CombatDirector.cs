using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatDirector : MonoBehaviour
{
    public int Difficulty = 0;
    public int DifficultyTimer = 30;
    public int MaxDifficulty = 6;

    public float SpawnInterval = 7;
    public Transform EnemyContainer;
    
    private EnemySpawner _enemySpawner;

    private List<Enemy> _mobList;
    private List<Enemy> _currentMobList = new();
    [SerializeField]private int _wave = 0;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.SetEnemyContainer(EnemyContainer);
    }

    private void Start()
    {
        _mobList = new List<Enemy>() {
            _enemySpawner.RockGolem,
            _enemySpawner.Wolf,
            _enemySpawner.Ghost};
        StartCoroutine(IncreaseDifficulty());
        StartCoroutine(StartSpawning(0f));
    }

    private IEnumerator IncreaseDifficulty()
    {
        Difficulty++;
        switch (Difficulty)
        {
            case 0:
                Debug.LogWarning($"Invalid value: {Difficulty} for difficulty!");
                break;
            case 1:
                _currentMobList.Add(_mobList[0]);
                break;
            case 2:
                _currentMobList.Add(_mobList[1]);
                SpawnInterval = 5f;
                break;
            case 3:
                _currentMobList.Add(_mobList[2]);
                SpawnInterval = 4f;
                break;
            default:
                if (Difficulty <= MaxDifficulty)
                {
                    SpawnInterval -= 0.25f;
                }
                break;
        }
        
        yield return new WaitForSeconds(DifficultyTimer);
        StartCoroutine(IncreaseDifficulty());
    }
    
    private IEnumerator StartSpawning(float firstSpawn)
    {
        yield return new WaitForSeconds(firstSpawn);
        StartCoroutine(SpawnContinuous());
    }
    private IEnumerator SpawnContinuous()
    {
        _wave++;
        GenerateMobs();
        yield return new WaitForSeconds(SpawnInterval);
        StartCoroutine(SpawnContinuous());
    }

    private void GenerateMobs()
    {
        _enemySpawner.Spawn(_currentMobList[Random.Range(0,_currentMobList.Count)]);
    }
}
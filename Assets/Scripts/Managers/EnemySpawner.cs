﻿using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Enemy Wolf;
    public Enemy RockGolem;
    public Enemy Ghost;

    private Camera _camera;
    private int _amountOfEnemies;
    private Transform _enemyContainer;

    private Vector3 _mapBounds;

    private void Awake()
    {
        _camera = Camera.main;
        _mapBounds = Globals.MapSize;
    }

    public void SetEnemyContainer(Transform go)
    {
        _enemyContainer = go;
    }

    public void Spawn(Enemy enemy, Vector3 location = default)
    {
        if (location == default)
            location = FindLocation();
        
        Enemy nEnemy = Instantiate(enemy, location, Quaternion.identity);
        nEnemy.transform.SetParent(_enemyContainer.transform);
        
        _amountOfEnemies = Globals.Enemies.Count;
    }

    private Vector3 FindLocation()
    {
        Vector3 _randomLocation = Vector3.zero;

        bool _found = false;
        
        while (!_found)
        {
            int _tries = 30;
            for (int _i = 0; _i < _tries ; _i++)
            {
                _randomLocation = RandomLocationInBounds();
                if (!PointInCameraView(_randomLocation))
                {
                    break;
                }
            }
        
            if (NavMesh.SamplePosition(_randomLocation, out NavMeshHit _hit, 2.0f, NavMesh.AllAreas))
            {
                _randomLocation = _hit.position;
            }
            else
            {
                continue;
            }
            
            if (Physics.Raycast(_randomLocation + new Vector3(0, 4, 0), Vector3.down, 6f, Globals.GroundMask))
            {
                _found = true;
            }
        }

        return _randomLocation;
    }

    private Vector3 RandomLocationInBounds()
    {
        float _randomX = Random.Range(0f, _mapBounds.x / 2f);
        float _randomY = Random.Range(0f, 0f);
        float _randomZ = Random.Range(0f, _mapBounds.z / 2f);
        
        return new Vector3(_randomX, _randomY, _randomZ);
    }

    private bool PointInCameraView(Vector3 point) {
        Vector3 viewport = _camera.WorldToViewportPoint(point);
        bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
        bool inFrontOfCamera = viewport.z > 0;
        bool objectBlockingPoint = false;

        Vector3 directionBetween = (point - _camera.transform.position).normalized;
 
        float distance = Vector3.Distance(_camera.transform.position, point);

        if(Physics.Raycast(_camera.transform.position, directionBetween, out RaycastHit depthCheck, distance + 0.05f)) {
            if(depthCheck.point != point) {
                objectBlockingPoint = true;
            }
        }
 
        return inCameraFrustum && inFrontOfCamera && !objectBlockingPoint;
    }

    private bool Is01(float a) {
        return a > 0 && a < 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, _mapBounds);
    }
}
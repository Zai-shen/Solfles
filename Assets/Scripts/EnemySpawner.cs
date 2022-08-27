using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector]public int Difficulty;
    public Enemy Wolf;
    public Enemy RockGolem;
    public Enemy Ghost;

    private Camera _camera;
    private int _amountOfEnemies;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Spawn(Enemy enemy, Vector3 location = default)
    {
        if (location == default)
            location = FindLocation();
        
        Enemy nEnemy = Instantiate(enemy, location, Quaternion.identity);
        
        _amountOfEnemies = Globals.Enemies.Count;
    }

    private Vector3 FindLocation()
    {
        Vector3 _randomLocation = Vector3.zero;
        bool found = false;

        while (!found)
        {
            int tries = 200;
            for (int i = 0; i < tries ; i++)
            {
                _randomLocation = RandomLocationInBounds();
                // Debug.Log($"Try location: {_randomLocation}");
                if (!PointInCameraView(_randomLocation))
                {
                    break;
                }
            }
            Debug.Log($"{_randomLocation} is out of camera :)");

            if (Physics.Raycast(_randomLocation + new Vector3(0, 5, 0), Vector3.down, 10f, Globals.GroundMask))
            {
                Debug.Log($"Found ground at: {_randomLocation}");
                found = true;
            }
            else
            {
                // Debug.Log($"Found NO ground at: {_randomLocation}");
            }
        }

        return _randomLocation;
    }

    private static Vector3 RandomLocationInBounds()
    {
        float _randomX = Random.Range(0f, Globals.MapSize.x / 2f);
        float _randomY = Random.Range(0f, 0f);
        float _randomZ = Random.Range(0f, Globals.MapSize.z / 2f);
        
        return new Vector3(_randomX, _randomY, _randomZ);
    }

    private bool PointInCameraView(Vector3 point) {
        Vector3 viewport = _camera.WorldToViewportPoint(point);
        bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
        bool inFrontOfCamera = viewport.z > 0;
 
        RaycastHit depthCheck;
        bool objectBlockingPoint = false;
 
        Vector3 directionBetween = point - _camera.transform.position;
        directionBetween = directionBetween.normalized;
 
        float distance = Vector3.Distance(_camera.transform.position, point);
 
        if(Physics.Raycast(_camera.transform.position, directionBetween, out depthCheck, distance + 0.05f)) {
            if(depthCheck.point != point) {
                objectBlockingPoint = true;
            }
        }
 
        return inCameraFrustum && inFrontOfCamera && !objectBlockingPoint;
    }
 
    public bool Is01(float a) {
        return a > 0 && a < 1;
    }
}
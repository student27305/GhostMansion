using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject _player;
    [SerializeField]
    LayerMask _playerLineOfSightLayerMask;
    [SerializeField]
    float _minimumSpawnDistanceFromPlayer = 15f;
    [SerializeField]
    float _minimumSpawnDistanceFromEnemies = 5f;
    [SerializeField]
    GameObject _enemyPrefab;

    GameObject[] _patrolPoints;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPosition");
        if (GameManager.Instance.Difficulty == 0)
        {
            SpawnEnemies(2);
        }
        else if (GameManager.Instance.Difficulty == 1)
        {
            SpawnEnemies(3);
        }
        else
        {
            SpawnEnemies(5);
        }
    }
    bool SpawnEnemy()
    {
        // Check for valid spawn position so enemy does not immediately chase or threaten player.
        List<GameObject> validSpawnPoints = new List<GameObject>();
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject spawnPoint in _patrolPoints)
        {
            if(
                !HasPositionLineOfSightToPlayer(gameObject.transform.position) // Do not spawn in player LOS
                && GetDistanceToPlayer(spawnPoint.transform.position) >= _minimumSpawnDistanceFromPlayer // Do not spawn too close
                && DistanceToClosestGameobject(spawnPoint.transform.position,existingEnemies) > _minimumSpawnDistanceFromEnemies) // Do not spawn to close to other enemies
            {
                validSpawnPoints.Add(spawnPoint);
            }
        }
        if(validSpawnPoints.Count <= 0) 
        { 
            Debug.LogError("Couldn't find a valid spot to spawn an enemy!");
            return false;
        }
        Vector3 spawnPosition = validSpawnPoints[Random.Range(0, validSpawnPoints.Count)].transform.position;
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        return true;
    }

    public void SpawnEnemies(int count) {
        for(int i = 0; i < count; i++) { SpawnEnemy(); }
    }

    float DistanceToClosestGameobject(Vector3 position, GameObject[] gameObjects)
    {
        float closestDistance = Mathf.Infinity;
        foreach(var gameObject in gameObjects)
        {
            float distance = Vector3.Distance(position, gameObject.transform.position);
            if (distance <= closestDistance) { closestDistance = distance; }
        }
        return closestDistance;
    }

    bool HasPositionLineOfSightToPlayer(Vector3 position)
    {
        return !Physics.Linecast(_player.transform.position, position, _playerLineOfSightLayerMask);
    }
    float GetDistanceToPlayer(Vector3 position)
    {
        return Vector3.Distance(_player.transform.position, position);
    }
}

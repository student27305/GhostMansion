using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _LevelManager : MonoBehaviour
{
    GameObject _player;
    [SerializeField]
    LayerMask _playerLineOfSightLayerMask;
    [SerializeField]
    float _minimumSpawnDistanceFromPlayer = 15f;
    [SerializeField]
    GameObject _enemyPrefab;

    GameObject[] _patrolPoints;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPosition");
    }
    public void SpawnEnemy()
    {
        // Check for valid spawn position so enemy does not immediately chase or threaten player.
        List<GameObject> validSpawnPoints = new List<GameObject>();
        foreach (GameObject spawnPoint in _patrolPoints)
        {
            if(!HasPositionLineOfSightToPlayer(gameObject.transform.position) && GetDistanceToPlayer(spawnPoint.transform.position) >= _minimumSpawnDistanceFromPlayer)
            {
                validSpawnPoints.Add(spawnPoint);
            }
        }
        Vector3 spawnPosition = validSpawnPoints[Random.Range(0, validSpawnPoints.Count)].transform.position;
        GameObject.Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
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

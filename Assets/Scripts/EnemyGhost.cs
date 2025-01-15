using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGhost : MonoBehaviour
{
    GameObject _player;
    EnemyState _state;
    NavMeshAgent _agent;

    [SerializeField]
    LayerMask _sightLayerMask;
    [SerializeField]
    float _spottingCooldown = 0.5f;
    [SerializeField]
    float _chaseLostSightDuration = 3f;
    [SerializeField]
    float _patrolDestinationReachedThreshold = 1.5f;

    float _timeSinceLastSpoted = 0f;
    float _timeSinceLostSightOfPlayer = 0f;

    Vector3 _patrolDestination;
    GameObject[] _patrolPositions;
    bool _hasLineOfSightToPlayer = false;

    enum EnemyState
    {
        Patrol, Chase
    }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _patrolPositions = GameObject.FindGameObjectsWithTag("PatrolPosition");
        StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessSight();
        if(_state == EnemyState.Patrol)
        {
            Patrol();
        }
        else if(_state == EnemyState.Chase)
        {
            Chase();
        }
    }
    void Patrol()
    {
        if(Vector3.Distance(transform.position, _patrolDestination) <= _patrolDestinationReachedThreshold)
        {
            _patrolDestination = GetRandomPatrolPosition();
            _agent.SetDestination(_patrolDestination);
        }
        if(_hasLineOfSightToPlayer)
        {
            StartChase();
        }
    }

    void Chase() 
    { 
        _agent.SetDestination(_player.transform.position);
        if (_timeSinceLostSightOfPlayer >= _chaseLostSightDuration) 
        {
            StartPatrol();
        }
    }

    void StartPatrol()
    {
        _state = EnemyState.Patrol;
        _timeSinceLastSpoted = _spottingCooldown;
        _patrolDestination = GetRandomPatrolPosition();
        _agent.SetDestination(_patrolDestination);
    }

    void StartChase()
    {
        _state = EnemyState.Chase;
        _timeSinceLastSpoted = 0;
    }

    Vector3 GetRandomPatrolPosition()
    {
        return _patrolPositions[Random.Range(0, _patrolPositions.Length)].transform.position;
    }

    bool CheckLineOfSightToPlayer()
    {
        return !Physics.Linecast(transform.position, _player.transform.position, _sightLayerMask);
    }

    void ProcessSight()
    {
        _timeSinceLastSpoted += Time.deltaTime;
        if (_timeSinceLastSpoted >= _spottingCooldown)
        {
            _timeSinceLastSpoted = 0f;
            _hasLineOfSightToPlayer = CheckLineOfSightToPlayer();
        }
        if (_hasLineOfSightToPlayer) {
            _timeSinceLostSightOfPlayer = 0;
        }
        else
        {
            _timeSinceLostSightOfPlayer += Time.deltaTime;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    public LayerMask GroundM, PlayerM, IgnoreSightCheck;

    #region Patroling

    public bool UsePatroling = false;
    public bool UseAggroRange = false;
    public float AggroRange = 10f;
    public Vector3 WalkPoint;
    private bool _walkPointSet;
    public float WalkPointRange;
    
    #endregion

    #region Attacking

    public float AttackRange;
    public float TimeBetweenAttacks;
    private bool alreadyAttacked;

    #endregion

    #region States

    public bool PlayerInSight, PlayerInAttackRange;

    #endregion

    #region Navigation

    private float _distanceToPlayer;
    private Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    
    #endregion

    
    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _navMeshPath = new NavMeshPath();
        _target = PlayerManager.Instance.Player.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        _distanceToPlayer = Vector3.Distance(_target.position, transform.position);
        PlayerInSight = !CheckPlayerIsOccluded();
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerM);

        if (UseAggroRange)
        {
            if (UsePatroling && !PlayerInAttackRange) Patrole();
            if (_distanceToPlayer <= AggroRange)
            {
                if ((PlayerInSight && !PlayerInAttackRange) || (!PlayerInSight)) ChasePlayer();
                if (PlayerInSight && PlayerInAttackRange) Attack();
            }
        }
        else
        {
            ChasePlayer();
            if (PlayerInSight && PlayerInAttackRange) Attack();
        }
    }

    private bool CheckPlayerIsOccluded()
    {
        bool occluded = true;
        RaycastHit _hit;

        for (float i = 0; i <= 1.2f; i += 0.4f)
        {
            Vector3 heightDifference = new(0,i,0);
            Vector3 _dirToPlayer = (_target.transform.position + heightDifference) - (transform.position + heightDifference);
            if (Physics.Raycast(transform.position + heightDifference, _dirToPlayer, out _hit, _distanceToPlayer, ~IgnoreSightCheck))
            {
                // Debug.DrawLine(transform.position, _dirToPlayer * _hit.distance, Color.black);
                // Debug.Log($"Hit the following: {_hit.transform.name}");
                occluded = true;
            }
            else
            {
                // Debug.DrawLine(transform.position + heightDifference, _dirToPlayer * 20f, Color.black);
                return false;
            }
        }

        return occluded;
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);
        FaceTarget();
        
        if (alreadyAttacked) return;

        //Do Attack here
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = Vector3.one / 10f;
        cube.transform.position = transform.position + new Vector3(0,0.5f,0);
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), TimeBetweenAttacks);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void ChasePlayer()
    {
        if (_agent.CalculatePath(_target.position, _navMeshPath))
            _agent.SetDestination(_target.position);
        else
            _agent.SetDestination(transform.position);
    }

    private void Patrole()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet) _agent.SetDestination(WalkPoint);

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float _randomX = Random.Range(-WalkPointRange, WalkPointRange);
        float _randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float _height = 2f;
        Vector3 _difference = new(_randomX, _height, _randomZ);

        WalkPoint = transform.position + _difference;
        
        if (_agent.CalculatePath(WalkPoint, _navMeshPath))//Physics.Raycast(WalkPoint, -transform.up, 5f, GroundM))
        {
            _walkPointSet = true;
        }else if (PlayerInAttackRange)
        {
            _walkPointSet = true;
        }
    }

    private void FaceTarget()
    {
        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        
        if (UseAggroRange)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, AggroRange);
        }

        if (UsePatroling)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, WalkPoint);
        }
        
        if (PlayerInSight)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{

    public LayerMask GroundM, PlayerM;

    #region Patroling

    public bool UsePatroling = false;
    public bool UseAggroRange = false;
    public float AggroRange = 10f;
    public Vector3 WalkPoint;
    private bool walkPointSet;
    public float WalkPointRange;
    
    #endregion

    #region Attacking

    public float AttackRange;
    public float TimeBetweenAttacks;
    private bool alreadyAttacked;

    #endregion

    #region States

    public bool PlayerInSightRange, PlayerInAttackRange;

    #endregion

    #region Navigation

    private float _distanceToPlayer;
    private Transform _target;
    private NavMeshAgent _agent;
    
    #endregion

    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _target = PlayerManager.Instance.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _distanceToPlayer = Vector3.Distance(_target.position, transform.position);
        PlayerInSightRange = Physics.CheckSphere(transform.position, AggroRange, PlayerM);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerM);
        
        if (!UseAggroRange || (_distanceToPlayer <= AggroRange))
        {

            if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
            if (PlayerInSightRange && PlayerInAttackRange) Attack();
            {
                
            }
        }
        else
        {
            if (UsePatroling && !PlayerInSightRange && !PlayerInAttackRange) Patrole();
        }
    }

    void Attack()
    {
        _agent.SetDestination(transform.position);
        
        FaceTarget();

        if (!alreadyAttacked)
        {
            //Do Attack here
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = Vector3.one / 10f;
            cube.transform.position = transform.position + new Vector3(0,0.5f,0);
            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    void ChasePlayer()
    {
        _agent.SetDestination(_target.position);
    }

    void Patrole()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            _agent.SetDestination(WalkPoint);
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float height = 2f;

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y + height, transform.position.z + randomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 5f, GroundM))
        {
            walkPointSet = true;
        }
    }
    
    void FaceTarget()
    {
        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (UseAggroRange)
        {
            Gizmos.DrawWireSphere(transform.position, AggroRange);
        }

        if (UsePatroling)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, WalkPoint);
        }
        
        if (PlayerInSightRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
        }
        
        Gizmos.color = Color.magenta; 
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        

    }
}

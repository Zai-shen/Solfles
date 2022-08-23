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
        float distance = Vector3.Distance(_target.position, transform.position);
        PlayerInSightRange = Physics.CheckSphere(transform.position, AggroRange, PlayerM);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, PlayerM);
        
        if (!UseAggroRange || (distance <= AggroRange))
        {

            if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
            if (PlayerInSightRange && PlayerInAttackRange) Attack();
            {
                
            }
            
            if (distance <= _agent.stoppingDistance)
            {
                FaceTarget();
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

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, GroundM))
        {
            walkPointSet = true;
        }
    }
    
    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6f);
        
        //transform.LookAt(target)
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (UseAggroRange)
        {
            Gizmos.DrawWireSphere(transform.position, AggroRange);
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

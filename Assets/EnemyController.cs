using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool UseAggroRadius = false;
    public float AggroRadius = 10f;

    public LayerMask GroundM, PlayerM;

    #region Patroling
    
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    #endregion

    #region Attacking

    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    #endregion


    private Transform _target;
    private NavMeshAgent _agent;
    
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

        if (!UseAggroRadius || (distance <= AggroRadius))
        {
            _agent.SetDestination(_target.position);

            if (distance <= _agent.stoppingDistance)
            {
                //Attack
                
                //Face
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6f);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (UseAggroRadius)
        {
            Gizmos.DrawWireSphere(transform.position, AggroRadius);
        }
        Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
    }
}

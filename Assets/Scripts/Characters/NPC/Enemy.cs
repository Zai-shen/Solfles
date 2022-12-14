using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Attack),typeof(Health))]
public class Enemy : MonoBehaviour
{

    public LayerMask IgnoreSightCheck;

    #region Patroling

    public bool UsePatroling = false;
    public bool UseAggroRange = false;
    public float AggroRange = 10f;
    public Vector3 WalkPoint;
    private bool _walkPointSet;
    public float WalkPointRange;
    
    #endregion

    #region Health

    public Health Health;

    #endregion
    
    #region Attacking

    private Attack _attack;

    #endregion

    #region States

    public bool PlayerInSight, PlayerInAttackRange;

    #endregion

    #region Navigation

    public float SearchCooldown = 0.1f;
    private float _searchCooldown;
    public float MoveSpeed = 2f;
    private Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    
    #endregion

    #region Animation

    protected Animator _animator;

    #endregion
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;
        _navMeshPath = new NavMeshPath();
        _animator = GetComponent<Animator>();
        Health = GetComponent<Health>();
        Health.OnDeath += Die;
        _attack = GetComponent<Attack>();
        _attack.EAnimator = _animator;
    }
    
    private void Start()
    {
        _target = PlayerManager.Instance.Player.transform;
        _attack.Target = _target;
    }

    private void OnEnable()
    {
        Globals.Enemies.Add(this.gameObject);
    }

    private void OnDisable()
    {
        Globals.Enemies.Remove(this.gameObject);
    }
    
    private void Update()
    {
        PlayerInAttackRange = _attack.CheckTargetInAttackRange();

        if (UseAggroRange)
        {
            PlayerInSight = !_attack.CheckTargetIsOccluded(IgnoreSightCheck);
            if (UsePatroling && !PlayerInAttackRange) Patrole();
            float _distanceToPlayer = _attack.DistanceToTarget();
            if (_distanceToPlayer <= AggroRange)
            {
                if (((PlayerInSight && !PlayerInAttackRange) || (!PlayerInSight)) && !_attack.OnCooldown) ChasePlayer();
                if (PlayerInSight && PlayerInAttackRange) Attack();
            }
        }
        else
        {
            FaceTarget();

            _searchCooldown += Time.deltaTime;
            if (_searchCooldown >= SearchCooldown)
            {
                if (!_attack.OnCooldown) ChasePlayer();
                _searchCooldown -= SearchCooldown;
            }

            if (PlayerInAttackRange)
            {
                PlayerInSight = !_attack.CheckTargetIsOccluded(IgnoreSightCheck);
            }
            if (PlayerInSight && PlayerInAttackRange) Attack();
        }
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);
        _animator.SetFloat("MovSpeed", 0f);
        FaceTarget();
        
        _attack.DoStartAttack();
    }

    private void ChasePlayer()
    {
        if (_agent.CalculatePath(_target.position, _navMeshPath))
        {
            _agent.SetDestination(_target.position);
            _animator.SetFloat("MovSpeed", 1f);
        }
        else
        {
            _agent.SetDestination(transform.position);
            _animator.SetFloat("MovSpeed", 0f);
        }
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
        const float _height = 2f;
        Vector3 _difference = new(_randomX, _height, _randomZ);

        WalkPoint = transform.position + _difference;
        
        if (_agent.CalculatePath(WalkPoint, _navMeshPath))
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
    
    void Die()
    {
        Destroy(this.gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
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

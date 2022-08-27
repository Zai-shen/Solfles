using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Attack),typeof(Health))]
public class Friend : MonoBehaviour
{

    public LayerMask IgnoreSightCheck;
    
    #region Health

    public Health Health;

    #endregion
    
    #region Attacking

    private List<GameObject> _enemiesInAttackRange = new();
    public float SearchCooldown = 0.5f;
    private float _searchCooldown;
    private Attack _attack;

    #endregion
    
    #region Navigation

    private Player _player;
    public float MoveSpeed = 5f;
    private Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    
    #endregion

    #region Animation

    protected Animator _animator;

    #endregion
    
    #region States

    public bool EnemyInSight, EnemyInAttackRange;

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
        _player = PlayerManager.Instance.Player;
    }

    private void Update()
    {
        ChasePlayer();
        
        Transform daTarget = null;
        _searchCooldown += Time.deltaTime;
        if (_searchCooldown >= SearchCooldown)
        {
            daTarget = FindEnemyToAttack();
            _searchCooldown -= SearchCooldown;
        }
        
        if (daTarget != null)
        {
            _attack.Target = daTarget;
            _attack.DoAttack();
        }
    }
    
    private void ChasePlayer()
    {
        if (_agent.CalculatePath(_player.transform.position, _navMeshPath))
        {
            _agent.SetDestination(_player.transform.position);
            _animator.SetFloat("MovSpeed", 1f);
        }
        else
        {
            _agent.SetDestination(transform.position);
            _animator.SetFloat("MovSpeed", 0f);
        }
    }
    
    void Die()
    {
        Destroy(this.gameObject);
    }
    
    private Transform FindEnemyToAttack()
    {
        if (Physics.CheckSphere(transform.position, _attack.AttackRange, Globals.EnemyMask))
        {
            _enemiesInAttackRange.Clear();
            float _closestDistance = Mathf.Infinity;
            Transform _enemyToAttack = null;
            
            foreach (GameObject _enemy in Globals.Enemies)
            {
                _attack.Target = _enemy.transform;
                EnemyInAttackRange = _attack.CheckTargetInAttackRange();
                EnemyInSight = !_attack.CheckTargetIsOccluded(IgnoreSightCheck);
                if (EnemyInAttackRange && EnemyInSight)
                {
                    _enemiesInAttackRange.Add(_enemy);
                    if (_attack.DistanceToTarget() < _closestDistance)
                    {
                        _enemyToAttack = _enemy.transform;
                    }
                }
            }
            
            return _enemyToAttack;
        }
    
        return null;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, _player ? _player.transform.position : Vector3.zero );
    }
}

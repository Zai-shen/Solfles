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

    public float SearchCooldown = 0.5f;
    private float _searchCooldown;
    private Attack _attack;

    #endregion
    
    #region Navigation

    public float MoveSpeed = 5f;
    private Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    
    #endregion

    #region Animation

    protected Animator _animator;

    #endregion
    
    // private void Awake()
    // {
    //     _agent = GetComponent<NavMeshAgent>();
    //     _agent.speed = MoveSpeed;
    //     _navMeshPath = new NavMeshPath();
    //     _animator = GetComponent<Animator>();
    //     Health = GetComponent<Health>();
    //     Health.OnDeath += Die;
    //     _attack = GetComponent<Attack>();
    //     _attack.EAnimator = _animator;
    // }
    //
    // private void Start()
    // {
    //     _target = PlayerManager.Instance.Player.transform;
    //     _attack.Target = _target;
    // }
    //
    // private void OnEnable()
    // {
    //     Globals.Enemies.Add(this.gameObject);
    // }
    //
    // private void OnDisable()
    // {
    //     Globals.Enemies.Remove(this.gameObject);
    // }
    //
    // private void Update()
    // {
    //     if (!_attack.OnCooldown) ChasePlayer();
    //     
    //     Transform daTarget = null;
    //     _searchCooldown += Time.deltaTime;
    //     if (_searchCooldown >= SearchCooldown)
    //     {
    //         daTarget = FindEnemyToAttack();
    //         _searchCooldown -= SearchCooldown;
    //     }
    //     
    //     if (daTarget != null)
    //     {
    //         _attack.Target = daTarget;
    //         _attack.DoAttack();
    //     }
    //     if (PlayerInSight && PlayerInAttackRange) Attack();
    // }
    //
    // private void Attack()
    // {
    //     _agent.SetDestination(transform.position);
    //     _animator.SetFloat("MovSpeed", 0f);
    //     FaceTarget();
    //     
    //     _attack.DoAttack();
    // }
    //
    // private void ChasePlayer()
    // {
    //     if (_agent.CalculatePath(_target.position, _navMeshPath))
    //     {
    //         _agent.SetDestination(_target.position);
    //         _animator.SetFloat("MovSpeed", 1f);
    //     }
    //     else
    //     {
    //         _agent.SetDestination(transform.position);
    //         _animator.SetFloat("MovSpeed", 0f);
    //     }
    // }
    //
    // private void Patrole()
    // {
    //     if (!_walkPointSet) SearchWalkPoint();
    //
    //     if (_walkPointSet) _agent.SetDestination(WalkPoint);
    //
    //     if (_agent.remainingDistance <= _agent.stoppingDistance)
    //     {
    //         _walkPointSet = false;
    //     }
    // }
    //
    // private void SearchWalkPoint()
    // {
    //     float _randomX = Random.Range(-WalkPointRange, WalkPointRange);
    //     float _randomZ = Random.Range(-WalkPointRange, WalkPointRange);
    //     const float _height = 2f;
    //     Vector3 _difference = new(_randomX, _height, _randomZ);
    //
    //     WalkPoint = transform.position + _difference;
    //     
    //     if (_agent.CalculatePath(WalkPoint, _navMeshPath))
    //     {
    //         _walkPointSet = true;
    //     }else if (PlayerInAttackRange)
    //     {
    //         _walkPointSet = true;
    //     }
    // }
    //
    // private void FaceTarget()
    // {
    //     transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
    // }
    //
    // void Die()
    // {
    //     Destroy(this.gameObject);
    // }
    //
    // private Transform FindEnemyToAttack()
    // {
    //     if (Physics.CheckSphere(transform.position, _attack.AttackRange, Globals.EnemyMask))
    //     {
    //         Globals.EnemiesInAttackRange.Clear();
    //         float _closestDistance = Mathf.Infinity;
    //         Transform _enemyToAttack = null;
    //         
    //         foreach (GameObject _enemy in Globals.Enemies)
    //         {
    //             _attack.Target = _enemy.transform;
    //             EnemyInAttackRange = _attack.CheckTargetInAttackRange();
    //             EnemyInSight = !_attack.CheckTargetIsOccluded(IgnoreSightCheck);
    //             if (EnemyInAttackRange && EnemyInSight)
    //             {
    //                 Globals.EnemiesInAttackRange.Add(_enemy);
    //                 if (_attack.DistanceToTarget() < _closestDistance)
    //                 {
    //                     _enemyToAttack = _enemy.transform;
    //                 }
    //             }
    //         }
    //         
    //         return _enemyToAttack;
    //     }
    //
    //     return null;
    // }
    //
    // private void OnDrawGizmosSelected()
    // {
    //     if (UseAggroRange)
    //     {
    //         Gizmos.color = Color.magenta;
    //         Gizmos.DrawWireSphere(transform.position, AggroRange);
    //     }
    //
    //     if (UsePatroling)
    //     {
    //         Gizmos.color = Color.blue;
    //         Gizmos.DrawLine(transform.position, WalkPoint);
    //     }
    //     
    //     if (PlayerInSight)
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.green;
    //         Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
    //     }
    // }
}

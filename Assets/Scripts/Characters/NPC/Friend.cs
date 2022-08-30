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
    public Healthbar HealthBar;

    #endregion
    
    #region Attacking

    private List<GameObject> _enemiesInAttackRange = new();
    public float SearchCooldown = 0.5f;
    private float _searchCooldown;
    private Attack _attack;

    #endregion
    
    #region Navigation

    public int FindRange = 3;
    private bool _found;
    private Player _player;
    private const float _maxDistanceToPlayer = 2f;
    private const float _standingDistanceToPlayer = 1.5f;
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
        HealthBar?.SetMaxHealth(Health.HealthPoints);
        _attack = GetComponent<Attack>();
        _attack.EAnimator = _animator;
    }

    private void OnEnable()
    {
        Health.OnDeath += Die;
        Health.OnTakeDamage += SetUIHealth;
    }
    
    private void OnDisable()
    {
        Health.OnDeath += Die;
        Health.OnTakeDamage -= SetUIHealth;
    }

    private void SetUIHealth(int currentHealth)
    {
        HealthBar?.SetHealth(currentHealth);
    }
    
    private void Start()
    {
        _player = PlayerManager.Instance.Player;
    }

    private void Update()
    {
        Transform daTarget = null;
        _searchCooldown += Time.deltaTime;
        if (_searchCooldown >= SearchCooldown)
        {
            if (!_found)
            {
                if (DistanceToPlayer() <= FindRange)
                {
                    HealthBar.transform.parent.gameObject.SetActive(true);
                    _found = true;
                    Globals.Friends.Add(this.gameObject);
                    _animator.SetTrigger("Jumping");
                    _player.FriendFound.Invoke();
                }
            }
            else
            {
                ChasePlayer();
                daTarget = FindEnemyToAttack();
                _searchCooldown -= SearchCooldown;
            }
        }
        
        _animator.SetFloat("MovSpeed", _agent.velocity.magnitude); 
        
        if (daTarget != null && _found)
        {
            _attack.Target = daTarget;
            _attack.DoAttack();
        }
    }
    
    private void ChasePlayer()
    {
        if (DistanceToPlayer() <= _maxDistanceToPlayer)
            return;
        
        bool posNeg1 = (Random.Range(0,2) == 1);
        bool posNeg2 = (Random.Range(0,2) == 1);
        Vector3 _randomisation = new(
            (posNeg1?1f:-1f) * Random.Range(0.5f, _standingDistanceToPlayer),
            0,
            (posNeg2?1f:-1f) * Random.Range(0.5f, _standingDistanceToPlayer));
        Vector3 _nearPlayer = _player.transform.position + _randomisation;
        const int tries = 30;
 
        for (int i = 0; i < tries; i++)
        {
            if (_agent.CalculatePath(_nearPlayer, _navMeshPath))
            {
                _agent.SetDestination(_nearPlayer);
                return;
            }
        }

        _agent.SetDestination(transform.position);
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, _player.transform.position);
    }

    private void Die()
    {
        _player.Health.TakeDamage(999);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement),
    typeof(Attack),
    typeof(Health))]
public class Player : MonoBehaviour
{
    public LayerMask IgnoreSightCheck, EnemyM;
    private PlayerMovement _playerMovement;

    #region Health

    public Health Health;

    #endregion

    #region Attack

    public float SearchCooldown = 0.5f;
    private float _searchCooldown;
    private Attack _attack;

    #endregion
    
    #region States

    public bool EnemyInSight, EnemyInAttackRange;

    #endregion
    
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        Health = GetComponent<Health>();
        Health.OnDeath += Die;
        _attack = GetComponent<Attack>();
        _attack.EAnimator = GetComponent<Animator>();
    }

    private void Die()
    {
        _attack.EAnimator.enabled = false;
        _playerMovement.enabled = false;
        _attack.enabled = false;
    }
    
    private void Update()
    {
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

    private Transform FindEnemyToAttack()
    {
        if (Physics.CheckSphere(transform.position, _attack.AttackRange, EnemyM))
        {
            Globals.EnemiesInAttackRange.Clear();
            float _closestDistance = Mathf.Infinity;
            Transform _enemyToAttack = null;
            
            foreach (GameObject _enemy in Globals.Enemies)
            {
                _attack.Target = _enemy.transform;
                EnemyInAttackRange = _attack.CheckTargetInAttackRange();
                EnemyInSight = !_attack.CheckTargetIsOccluded(IgnoreSightCheck);
                if (EnemyInAttackRange && EnemyInSight)
                {
                    Globals.EnemiesInAttackRange.Add(_enemy);
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
}

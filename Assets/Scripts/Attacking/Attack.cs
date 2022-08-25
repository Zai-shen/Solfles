using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Target;
    [HideInInspector]public Animator EAnimator;

    #region AttackStats

    public float AttackRange = 1f;
    public float Cooldown = 1f;
    private bool onCooldown;
    
    #endregion
    
    public void DoAttack()
    {
        if (onCooldown) return;
        EarlyAttack();

        Invoke(nameof(MainAttack), Cooldown / 2f);

        onCooldown = true;
        Invoke(nameof(ResetAttack), Cooldown);
    }

    protected virtual void MainAttack() { }
    
    protected virtual void EarlyAttack() { }
    
    protected virtual void LateAttack() { }

    private void ResetAttack()
    {
        onCooldown = false;
        LateAttack();
    }

    private void Update()
    {
        EAnimator.SetBool("IsAttacking",onCooldown);
    }

    public float DistanceToTarget(Transform target)
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public bool CheckTargetInAttackRange(Transform target, LayerMask lm = default)
    {
        return DistanceToTarget(Target) <= AttackRange;
    }

    public bool CheckTargetIsOccluded(Transform target, LayerMask _ignoreLayers)
    {
        bool occluded = true;
        RaycastHit _hit;

        for (float i = 0; i <= 1.2f; i += 0.4f)
        {
            Vector3 heightDifference = new(0,i,0);
            Vector3 _dirToTarget = (target.transform.position + heightDifference) - (transform.position + heightDifference);
            if (Physics.Raycast(transform.position + heightDifference, _dirToTarget, out _hit, DistanceToTarget(Target), ~_ignoreLayers))
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
}

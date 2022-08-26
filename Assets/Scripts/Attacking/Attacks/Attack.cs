using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Target;
    [HideInInspector]public Animator EAnimator;

    #region AttackStats

    public int AttackDamage = 1;
    public float AttackRange = 1f;
    public float Cooldown = 1f;
    public bool OnCooldown;
    
    #endregion
    
    public void DoAttack()
    {
        if (OnCooldown) return;

        EarlyAttack();
        Invoke(nameof(MainAttack), Cooldown / 2f);
        Invoke(nameof(LateAttack), Cooldown);

        OnCooldown = true;
        Invoke(nameof(ResetAttack), Cooldown);
    }

    protected virtual void MainAttack()
    {
        if (!Target)
        {
            Debug.Log("No Target!");
            return;
        }
    }

    protected virtual void EarlyAttack()
    {
        if (!Target)
        {
            Debug.Log("No Target!");
            return;
        }
    }

    protected virtual void LateAttack()
    {
        if (!Target)
        {
            Debug.Log("No Target!");
            return;
        }
    }

    private void ResetAttack()
    {
        OnCooldown = false;
    }

    private void Update()
    {
        EAnimator.SetBool("IsAttacking",OnCooldown);
    }

    public float DistanceToTarget()
    {
        return Vector3.Distance(Target.position, transform.position);
    }

    public bool CheckTargetInAttackRange()
    {
        return DistanceToTarget() <= AttackRange;
    }

    public bool CheckTargetIsOccluded(LayerMask _ignoreLayers)
    {
        bool occluded = true;
        RaycastHit _hit;

        for (float i = 0; i <= 1.2f; i += 0.4f)
        {
            Vector3 heightDifference = new(0,i,0);
            Vector3 _dirToTarget = (Target.transform.position + heightDifference) - (transform.position + heightDifference);
            if (Physics.Raycast(transform.position + heightDifference, _dirToTarget, out _hit, DistanceToTarget(), ~_ignoreLayers))
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Target;
    [HideInInspector]public Animator EAnimator;

    #region AttackStats

    public float AttackRange;
    public float Cooldown;
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
}

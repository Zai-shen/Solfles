using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Target;

    #region AttackStats

    public float AttackRange;
    public float TimeBetweenAttacks;
    private bool onCooldown;
    
    #endregion

    public void DoAttack()
    {
        if (onCooldown) return;

        HandleAttack();

        onCooldown = true;
        Invoke(nameof(ResetAttack), TimeBetweenAttacks);
    }

    protected virtual void HandleAttack()
    {
        Debug.Log("Base Attack.");
    }

    private void ResetAttack()
    {
        onCooldown = false;
    }
}

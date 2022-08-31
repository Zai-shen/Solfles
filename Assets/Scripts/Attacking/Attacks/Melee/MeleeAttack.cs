using UnityEngine;

public class MeleeAttack : Attack
{
    public float HitDistanceTolerance = 1f;
    public GameObject HitEffect;

    protected override void CheckIsValidAttack()
    {
        base.CheckIsValidAttack();
        if (!IsValidAttack || ((DistanceToTarget() - HitDistanceTolerance) >= AttackRange))
        {
            IsValidAttack = false;
        }
    }
}
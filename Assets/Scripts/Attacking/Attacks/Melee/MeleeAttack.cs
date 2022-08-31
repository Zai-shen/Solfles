using UnityEngine;

public class MeleeAttack : Attack
{
    public GameObject HitEffect;

    protected override void DoAttack()
    {
        if(!Target) return;
        base.DoAttack();
    }
}
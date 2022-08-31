using UnityEngine;

public class RangedAttack : Attack
{
    public GameObject Projectile;     
    
    protected override void DoAttack()
    {
        if(!Target) return;
        base.DoAttack();
    }
}
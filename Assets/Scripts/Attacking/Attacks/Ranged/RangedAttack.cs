using UnityEngine;

public class RangedAttack : Attack
{
    public GameObject Projectile;     
    
    protected override void EarlyAttack()
    {
        if(!Target) return;
        base.EarlyAttack();
    }
    protected override void MainAttack()
    {
        if(!Target) return;
        base.MainAttack();
    }
    protected override void LateAttack()
    {
        if(!Target) return;
        base.LateAttack();
    }
    
}
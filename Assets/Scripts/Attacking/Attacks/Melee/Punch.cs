using UnityEngine;

public class Punch : MeleeAttack
{
    protected override void MainAttack()
    {
        base.MainAttack();
        Health _targetHealth = Target.GetComponent<Health>();
        _targetHealth.TryTakeDamage(AttackDamage);
    }
}
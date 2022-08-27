using UnityEngine;

public class Punch : MeleeAttack
{
    protected override void MainAttack()
    {
        base.MainAttack();
        Vector3 _closingDistance = new Vector3(0, 1.25f, 0.5f);
        Instantiate(HitEffect, transform.position + _closingDistance, Quaternion.identity);
        Health _targetHealth = Target.GetComponent<Health>();
        _targetHealth.TryTakeDamage(AttackDamage);
    }
}
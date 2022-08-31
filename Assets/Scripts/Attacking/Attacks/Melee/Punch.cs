using UnityEngine;

public class Punch : MeleeAttack
{
    protected override void DoAttack()
    {
        base.DoAttack();
        if(!Target) return;
        
        Vector3 _spawnDistance = new Vector3(0, 1f, 1f);
        Vector3 _spawnDirection = (Target.position - transform.position).normalized;
        Vector3 _spawnLocation = (transform.position + new Vector3(0, _spawnDistance.y, 0)) + (_spawnDirection * _spawnDistance.z);
        Instantiate(HitEffect, _spawnLocation, Quaternion.identity);
        Health _targetHealth = Target.GetComponent<Health>();
        _targetHealth.TryTakeDamage(AttackDamage);
    }
}
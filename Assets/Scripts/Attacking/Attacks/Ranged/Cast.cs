using UnityEngine;

public class Cast : RangedAttack
{
    public float ShotForce = 25f;

    protected override void MainAttack()
    {
        base.MainAttack();
        if (!Target) return;

        Vector3 midPlayerBody = new Vector3(0,1.85f / 2f,0);
        Vector3 _spawnDistance = new Vector3(-0.3f, 1.35f, 0.3f);
        Vector3 _spawnDirection = (Target.position - transform.position).normalized;
        Vector3 _spawnLocation = (transform.position + new Vector3(0, _spawnDistance.y,0)) + (_spawnDirection * _spawnDistance.z) + (transform.right * _spawnDistance.x);

        GameObject shot = Instantiate(Projectile, _spawnLocation, Quaternion.identity);
        Projectile pShot = shot.GetComponent<Projectile>();
        pShot.ShotForce = ShotForce;
        pShot.ShotDamage = AttackDamage;
        pShot.Shoot(((Target.position + midPlayerBody) - _spawnLocation).normalized);
    }
    
}
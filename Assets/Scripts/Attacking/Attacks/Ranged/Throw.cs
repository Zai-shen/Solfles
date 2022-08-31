using UnityEngine;

public class Throw : RangedAttack
{
    public float ShotForce = 15f;

    protected override void DoAttack()
    {
        base.DoAttack();
        if(!Target) return;

        Vector3 _spawnDistance = new Vector3(0, 1.25f, 0f);
        Vector3 _spawnLocation = (transform.position + new Vector3(0, _spawnDistance.y, 0));

        GameObject shot = Instantiate(Projectile, _spawnLocation, Quaternion.identity);
        Projectile pShot = shot.GetComponent<Projectile>();
        pShot.ShotForce = ShotForce;
        pShot.ShotDamage = AttackDamage;
        pShot.Shoot(((Target.position) - _spawnLocation).normalized);
    }
}
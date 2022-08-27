using UnityEngine;

public class Throw : RangedAttack
{
    public float ShotForce = 15f;

    protected override void MainAttack()
    {
        base.MainAttack();

        Vector3 projSpawn = transform.position + new Vector3(0, 1.5f, 0f);
        GameObject shot = Instantiate(Projectile, projSpawn, Quaternion.identity);
        Projectile pShot = shot.GetComponent<Projectile>();
        pShot.ShotForce = ShotForce;
        pShot.ShotDamage = AttackDamage;
        pShot.Shoot(((Target.position) - projSpawn).normalized);
    }
}
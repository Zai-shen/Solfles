using UnityEngine;

public class Cast : RangedAttack
{
    public float ShotForce = 25f;

    protected override void MainAttack()
    {
        base.MainAttack();
        if (!Target) return;

        Vector3 midPlayerBody = new Vector3(0,1.85f / 2f,0);
        Vector3 projSpawn = transform.position + new Vector3(-0.3f, 1.3f, 0.3f);
        GameObject shot = Instantiate(Projectile, projSpawn, Quaternion.identity);
        Projectile pShot = shot.GetComponent<Projectile>();
        pShot.ShotForce = ShotForce;
        pShot.ShotDamage = AttackDamage;
        pShot.Shoot(((Target.position + midPlayerBody) - projSpawn).normalized);
    }
    
}
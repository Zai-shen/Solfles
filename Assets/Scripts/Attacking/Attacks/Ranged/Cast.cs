using UnityEngine;

public class Cast : RangedAttack
{
    public float ShotForce = 25f;

    protected override void MainAttack()
    {
        base.MainAttack();
        
        GameObject shot = Instantiate(Projectile, transform.position + new Vector3(-0.3f, 1.3f, 0.3f), Quaternion.identity);
        Projectile pShot = shot.GetComponent<Projectile>();
        pShot.ShotForce = ShotForce;
        pShot.ShotDamage = AttackDamage;
        pShot.Shoot(transform.forward);
    }
    
}
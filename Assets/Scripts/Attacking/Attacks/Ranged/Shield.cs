using UnityEngine;

public class Shield : RangedAttack
{
    public float ShotForce = 10f;
    
    protected override void LateAttack()
    {
        base.LateAttack();
        
        GameObject shot = Instantiate(Projectile, transform.position + new Vector3(0f, 1.5f, -0.3f), Quaternion.identity);
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        rb.AddForce((Target.position - transform.position).normalized * ShotForce, ForceMode.Impulse);
    }
    
}
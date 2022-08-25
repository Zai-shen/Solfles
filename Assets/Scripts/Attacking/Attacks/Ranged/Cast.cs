using UnityEngine;

public class Cast : RangedAttack
{

    protected override void MainAttack()
    {
        base.MainAttack();
        
        Debug.Log("RangedAttack base");
        GameObject shot = Instantiate(Projectile, transform.position + new Vector3(-0.3f, 1.3f, 0.3f), Quaternion.identity);
        Rigidbody rb = shot.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
    }
    
}
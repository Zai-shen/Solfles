using UnityEngine;

public class Cast : RangedAttack
{
    public float ShotForce = 25f;

    protected override void MainAttack()
    {
        base.MainAttack();
        
        // Debug.Log("Cast");
        GameObject shot = Instantiate(Projectile, transform.position + new Vector3(-0.3f, 1.3f, 0.3f), Quaternion.identity);
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ShotForce, ForceMode.Impulse);
    }
    
}
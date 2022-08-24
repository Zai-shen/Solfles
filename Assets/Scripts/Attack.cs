using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Target;
    
    public float AttackRange;
    public float TimeBetweenAttacks;
    private bool onCooldown;
    
    public void DoAttack()
    {
        if (onCooldown) return;

        HandleAttack();

        onCooldown = true;
        Invoke(nameof(ResetAttack), TimeBetweenAttacks);
    }

    private void HandleAttack()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = Vector3.one / 10f;
        cube.transform.position = transform.position + new Vector3(0, 1f, 0.35f);
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
    }

    private void ResetAttack()
    {
        onCooldown = false;
    }
}

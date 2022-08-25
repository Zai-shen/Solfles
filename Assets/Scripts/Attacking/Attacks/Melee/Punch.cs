using UnityEngine;

public class Punch : MeleeAttack
{

    protected override void EarlyAttack()
    {
        base.EarlyAttack();
        // Debug.Log("Punch");
    }
    
}
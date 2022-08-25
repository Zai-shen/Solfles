﻿using UnityEngine;

public class RangedAttack : Attack
{

    protected override void HandleAttack()
    {
        Debug.Log("RangedAttack base");
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = Vector3.one / 10f;
        cube.transform.position = transform.position + new Vector3(0, 1f, 0.35f);
        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
    }
    
}
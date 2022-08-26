using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public ParticleSystem HitEffect;
    
    public float ShotForce;
    public int ShotDamage;
    public float LifeTimeAfterCollision = 1f;
    private Rigidbody _rb;
    private bool _didHit;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    
    public void Shoot(Vector3 direction)
    {
        _rb.AddForce(direction * ShotForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colliderGo = collision.gameObject;
        if ((((1 << colliderGo.layer) == Globals.PlayerMask)
            || ((1 << colliderGo.layer) == Globals.EnemyMask))
            && !_didHit)
        {
            _didHit = true;
            colliderGo.GetComponent<Health>()?.TryTakeDamage(ShotDamage);
            if (HitEffect)
            {
                Instantiate(HitEffect, transform.position, Quaternion.identity);
            }

        }
        
        StartCoroutine(DelayedDestroy(LifeTimeAfterCollision));
    }
    
    private IEnumerator DelayedDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnDeath;
    public Action<int> OnTakeDamage;
    public bool GodMode;
    public int HealthPoints = 100;
    private int _startingHP;

    private bool _tookDamageRecently;
    public float _invulnerabilityCooldown;
    
    private void Awake()
    {
        _startingHP = HealthPoints;
    }
    
    public void ResetToFull()
    {
        HealthPoints = _startingHP;
    }

    public void TryTakeDamage(int dmg)
    {
        if (!enabled) return;
        if (_tookDamageRecently) return;
        
        TakeDamage(dmg);
        StartCoroutine(MakeInvulnerable(_invulnerabilityCooldown));
    }

    private IEnumerator MakeInvulnerable(float duration)
    {
        _tookDamageRecently = true;
        yield return new WaitForSeconds(duration);
        _tookDamageRecently = false;
        yield return null;
    }
    
    public void TakeDamage(int dmg)
    {
        HealthPoints -= dmg;
        OnTakeDamage?.Invoke(HealthPoints);
        
        if (HealthPoints <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        if (GodMode)
            return;
        
        OnDeath?.Invoke();
    }
}

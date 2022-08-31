using System;
using System.Collections;
using UnityEngine;

public class Shield : RangedAttack
{
    public GameObject PlayerShield;
    private Material _shieldMat;
    private Color _shieldMatColor;
    
    public float ShotForce = 10f;

    public Color ShieldFXFinalColor = new Color(0.9f, 0.1f, 0.1f, 1);
    public bool CRRunning;
    private void Awake()
    {
        _shieldMat = PlayerShield.GetComponent<MeshRenderer>().material;
        _shieldMatColor = _shieldMat.color;
    }

    protected override IEnumerator AttackDuringAnimation()
    {
        StartCoroutine(ShieldFX(Cooldown));
        yield return base.AttackDuringAnimation();
    }

    protected override void DoAttack()
    {
        base.DoAttack();
        if(!Target) return;
        
        Vector3 _spawnDistance = new Vector3(0, 1.25f, 0.4f);
        Vector3 _spawnDirection = (new Vector3(0,0, _spawnDistance.z) - transform.position).normalized;
        Vector3 _spawnLocation = (transform.position + new Vector3(0, _spawnDistance.y, 0)) + (_spawnDirection * _spawnDistance.z);
        
        GameObject shot = Instantiate(Projectile, _spawnLocation, Quaternion.identity);
        Projectile pShot = shot.GetComponent<Projectile>();
        pShot.ShotForce = ShotForce;
        pShot.ShotDamage = AttackDamage;
        pShot.Shoot((Target.position - _spawnLocation).normalized);
    }

    private IEnumerator ShieldFX(float duration)
    {
        CRRunning = true;
        float _elapsedTime = 0;
        Color endColor = _shieldMatColor * ShieldFXFinalColor;

        while (_elapsedTime < duration)
        {
            Color _newColor = Color.Lerp(_shieldMatColor, endColor, (_elapsedTime / duration));
            ShieldFXFinalColor = _newColor;
            _shieldMat.SetColor("_BaseColor", _newColor);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }  
        _shieldMat.SetColor("_BaseColor", _shieldMatColor);
        
        CRRunning = false;
        yield return null;
    }
}
using System;
using System.Collections;
using UnityEngine;

public class Shield : RangedAttack
{
    public GameObject PlayerShield;
    private Material _shieldMat;
    private Color _shieldMatColor;
    
    public float ShotForce = 10f;
    public float ShotLifeTime = 5f;

    public Color ShieldFXFinalColor = new Color(0.9f, 0.1f, 0.1f, 1);
    public bool CRRunning;
    private void Awake()
    {
        _shieldMat = PlayerShield.GetComponent<MeshRenderer>().material;
        _shieldMatColor = _shieldMat.color;
    }

    protected override void EarlyAttack()
    {
        base.EarlyAttack();
        StartCoroutine(ShieldFX(Cooldown));
    }

    protected override void LateAttack()
    {
        base.LateAttack();
        
        GameObject shot = Instantiate(Projectile, transform.position + new Vector3(0f, 1.5f, -0.3f), Quaternion.identity);
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        rb.AddForce((Target.position - transform.position).normalized * ShotForce, ForceMode.Impulse);

        StartCoroutine(DelayedDestroy(ShotLifeTime, shot));
    }

    private IEnumerator DelayedDestroy(float time, GameObject goToDestroy)
    {
        yield return new WaitForSeconds(time);
        Destroy(goToDestroy);
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
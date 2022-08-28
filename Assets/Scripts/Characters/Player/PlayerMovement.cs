using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    private AudioSource _audioSource;
    private float _stepCooldown;
    public float StepCooldown = 0.25f;
    
    
    [SerializeField] private Vector3 _currentMovement;
 
    private Animator _animator;   
    private CharacterController _characterController;
    private bool _HasMoved;
    
    private readonly Matrix4x4 isoFix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
    }

    private void FixedUpdate()
    {
        if (!_HasMoved)
        {
            return;
        }
        DoRun();
    }

    private void Run()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _currentMovement = new Vector3(horizontalInput, 0, verticalInput);
        _currentMovement = isoFix.MultiplyPoint3x4(_currentMovement);
        _currentMovement.Normalize();

            _stepCooldown += Time.deltaTime;
        if (_HasMoved && (_currentMovement.magnitude >= 0.01f))
        {
            if (_stepCooldown >= StepCooldown)
            {
                _audioSource.pitch = Random.Range(0.7f, 1.3f);
                _audioSource.Play();
                _stepCooldown -= StepCooldown;
            }
        }
        
        Animate();
    }

    private void Animate()
    {
        if (!_HasMoved && _currentMovement != default)
        {
            StartCoroutine(StandUp());
        }

        if (_HasMoved && _currentMovement != Vector3.zero)
        {
            transform.forward = _currentMovement;
        }

        if (!_HasMoved)
        {
            _animator.SetBool("IsSleeping", true);
        }
        else
        {
            _animator.SetFloat("MovSpeed", _currentMovement.magnitude);
        }
        
    }

    private void DoRun()
    {
        _characterController.Move(_currentMovement * Time.deltaTime * MovementSpeed);
    }

    public void Jump()
    {
        _animator.SetTrigger("Jumping");
    }

    private IEnumerator StandUp()
    {
        _animator.SetTrigger("StandingUp");
        yield return new WaitForSeconds(3f);
        _animator.SetBool("IsSleeping", false);
        _HasMoved = true;
    }
}

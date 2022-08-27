using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    [SerializeField] private Vector3 _currentMovement;
 
    private Animator _animator;   
    private CharacterController _characterController;
    private bool _HasMoved;
    
    private readonly Matrix4x4 isoFix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        Jump();
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
        
        Animate();
    }

    private void Animate()
    {
        if (!_HasMoved && _currentMovement != default)
        {
            StartCoroutine(StandUp());
        }

        if (!_HasMoved)
        {
            _animator.SetBool("IsSleeping", true);
        }
        
        if (_currentMovement != Vector3.zero)
        {
            transform.forward = _currentMovement;
        }

        _animator.SetFloat("MovSpeed", _currentMovement.magnitude);
    }

    private void DoRun()
    {
        _characterController.Move(_currentMovement * Time.deltaTime * MovementSpeed);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jumping");
        }
    }

    private IEnumerator StandUp()
    {
        _animator.SetBool("IsSleeping", false);
        _animator.SetTrigger("StandingUp");
        yield return new WaitForSeconds(3f);
        _HasMoved = true;
    }
}

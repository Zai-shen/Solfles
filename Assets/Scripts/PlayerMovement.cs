using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
 
    private Animator _animator;   
    private CharacterController _characterController;

    Matrix4x4 isoFix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    [SerializeField] private Vector3 _currentMovement;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
    }

    private void FixedUpdate()
    {
        DoRun();
    }

    void Run()
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
        if (_currentMovement != Vector3.zero)
        {
            transform.forward = _currentMovement;
        }

        _animator.SetFloat("MovSpeed", _currentMovement.magnitude);
    }

    void DoRun()
    {
        _characterController.Move(_currentMovement * Time.deltaTime * MovementSpeed);
    }
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jumping");
        }
    }
}

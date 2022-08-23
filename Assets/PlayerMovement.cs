using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
 
    private Animator _animator;   
    private CharacterController _characterController;

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
    }
    
    void Run()
    {
        _currentMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.Move(_currentMovement * Time.deltaTime * MovementSpeed);
        _animator.SetFloat("MovSpeed", _currentMovement.magnitude);
    }
}

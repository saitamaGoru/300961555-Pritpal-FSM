using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runMultiplier;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity; 

    [Header("Looking")]
    [SerializeField] private float _mouseSens;
    [SerializeField] private float _upDownRange;

    private float _verticalRotation; 
    private Camera _mainCamera;
    private Vector3 _currentMov = Vector3.zero;

    private CharacterController _characterController;

    private PlayerInput _playerInputs;
    [Header("New Input Action")]
    [SerializeField] private Vector2 _moveInput, _lookInput;
    [SerializeField] private bool _isJumpPresded, _isSprintPressed;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInputs = new PlayerInput();
        _mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;

        _playerInputs.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInputs.Player.Move.canceled += _ => _moveInput = Vector2.zero;

        _playerInputs.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _playerInputs.Player.Look.canceled += _ => _lookInput = Vector2.zero;

        _playerInputs.Player.Jump.started += Jump;
        _playerInputs.Player.Jump.canceled += Sprint;

    }
    void Update()
    {
        HandleRotation();
        Move();
    }


    private void Move()
    {
        float speedMulti = _isSprintPressed ? _runMultiplier : 1f;

        float verticalSpeed = _moveInput.y * _walkSpeed * speedMulti;
        float horiSpeed = _moveInput.x * _walkSpeed * speedMulti;

        Vector3 horizontalMovement = new Vector3(horiSpeed, 0, verticalSpeed);

        horizontalMovement = transform.rotation * horizontalMovement;
        HandleGravityAndJump();
        _currentMov.x = horizontalMovement.x;
        _currentMov.z = horizontalMovement.z;
        _characterController.Move(_currentMov * (Time.deltaTime * speedMulti));
    }

    private void OnEnable() => _playerInputs.Enable();
    private void OnDisable() => _playerInputs.Disable();

     private void HandleGravityAndJump()
    {
        if(_characterController.isGrounded)
        {
            _currentMov.y = -0.05f;
        }
        else{
            _currentMov.y = _gravity * Time.deltaTime;
        }
    }


     private void HandleRotation()
    {
        float mouseXRotation = _lookInput.x * _mouseSens;
        transform.Rotate(0,mouseXRotation,0);

        _verticalRotation = _lookInput.y * _mouseSens;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_upDownRange, _upDownRange);
        _mainCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0,0);
    }



    private void Jump(InputAction.CallbackContext ctx)
    {
        _isJumpPresded = ctx.ReadValueAsButton();
    }

    private void Sprint(InputAction.CallbackContext ctx)
    {
        _isSprintPressed = ctx.ReadValueAsButton();
    }
}

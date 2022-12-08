using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float Speed = 100f;
    private Camera _camera;
    private Rigidbody _rigidbody;
    private Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>() * Speed;
    }

    private void Update()
    {
        _rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y), ForceMode.Force);
    }

    public float GetCameraDistanceValue()
    {
        if (_rigidbody.velocity.magnitude > 15f) return 2f;
        else if (_rigidbody.velocity.magnitude > 10f) return 1.5f;
        else return 1f;
    }
    
}
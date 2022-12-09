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
        if (context.performed)
        {
            // Input direction correction, according to the player velocity
            Vector3 direction = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).normalized;
            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            inputVector = Quaternion.Euler(angle * Vector3.up) * inputVector;
        }
    }

    private void Update()
    {
        _rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y), ForceMode.Force);
    }

    /// <summary>
    /// Get the distance the camera should have, according to the Player velocity
    /// </summary>
    /// <returns></returns>
    public float GetCameraDistanceValue()
    {
        if (_rigidbody.velocity.magnitude > 15f) return 2f;
        else if (_rigidbody.velocity.magnitude > 10f) return 1.5f;
        else return 1f;
    }
}

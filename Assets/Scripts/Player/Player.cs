using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool RouleMaBoule = false;
    public float Speed = 10f;
    private Camera _camera;
    private Rigidbody _rigidbody;
    private Vector2 inputVector = Vector2.zero;

    [System.NonSerialized] public bool Arrived = false;
    [System.NonSerialized] public Snowman Snowman;

    private Vector3 InitPos;
    private Vector3 InitEul;
    private Vector3 InitSca;
    private float time;
    public float Timer{get{ return time;}}
    bool started;

    private void Awake()
    {
        time = 0.0f;
        started = false;
        InitPos = transform.position;
        InitEul = transform.eulerAngles;
        InitSca = transform.localScale;
        
        _camera = GetComponentInChildren<Camera>();
        _rigidbody = GetComponent<Rigidbody>();
        if(!RouleMaBoule) _rigidbody.isKinematic = true;
    }

    public void StartRun()
    {
        started = true;
        _rigidbody.isKinematic = false;
    }

    public void Reset()
    {
        Arrived = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.position = InitPos;
        transform.eulerAngles = InitEul;
        transform.localScale = InitSca;
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>() * Speed;
        if (context.performed)
        {
            // Input direction correction, according to the player velocity
            Vector3 direction = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).normalized;
            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
            inputVector = Quaternion.Euler(angle * Vector3.up) * new Vector3(inputVector.x, 0, 0);
        }
    }

    private void Update()
    {
        _rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y), ForceMode.Force);
    }

    private void FixedUpdate() {
        if(started){
            time += Time.fixedDeltaTime;
        }    
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

    public void EndRun()
    {
        started = false;
        _rigidbody.isKinematic = true;
        Snowman.Build(this);
    }
}

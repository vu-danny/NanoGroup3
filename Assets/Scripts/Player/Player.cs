using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool RouleMaBoule = false;
    public float Speed = 10f;

    [SerializeField] private AnimationCurve SpeedBasedOnScale;
    [SerializeField] private Camera _camera;
     [System.NonSerialized] public Rigidbody _rigidbody;
    private Vector2 inputVector = Vector2.zero;

    [System.NonSerialized] public bool Arrived = false;
    [System.NonSerialized] public Snowman Snowman;

    private Vector3 InitPos;
    private Vector3 InitEul;
    private Vector3 InitSca;
    private float time;
    public float Timer{get{ return time;}}
    bool started;

    public int number;

    private void Awake()
    {
        time = 0.0f;
        started = false;
        InitPos = transform.position;
        InitEul = transform.eulerAngles;
        InitSca = transform.localScale;
        
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
        if(context.performed)
            inputVector = context.ReadValue<Vector2>() * Speed;
    }

    private void Update()
    {
        _rigidbody.AddForce(inputVector.x * _camera.transform.right, ForceMode.Force);
        _rigidbody.velocity =
            Vector3.ClampMagnitude(_rigidbody.velocity, SpeedBasedOnScale.Evaluate(transform.localScale.x));
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Debug.DrawLine(transform.position, transform.position + inputVector.x * _camera.transform.right);
        Debug.DrawLine(_camera.transform.position, _camera.transform.position + inputVector.x * _camera.transform.right);
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

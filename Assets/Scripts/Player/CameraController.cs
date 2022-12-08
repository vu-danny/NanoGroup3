using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player Player;
    private Vector3 BaseOffset;
    private Vector3 offset;

    private Coroutine CoroutineDistanceStamp;
    private float CameraDistanceValue = 1f;
    private Coroutine CoroutineRotationStamp;
    private float oldAngle = 0f;
    
    
    void Start()
    {
        BaseOffset = transform.localPosition - Player.transform.localPosition;
        offset = BaseOffset;
    }
 
    void LateUpdate()
    {
        if (Player.GetCameraDistanceValue() != CameraDistanceValue)
        {
            Vector3 TargetOffset = new Vector3(0f, 
                BaseOffset.y * Player.GetCameraDistanceValue(), 
                BaseOffset.z * Player.GetCameraDistanceValue());
            
            if (CoroutineDistanceStamp != null)
            {
                StopCoroutine(CoroutineDistanceStamp);
                CoroutineDistanceStamp = null;
            }
            CoroutineDistanceStamp = StartCoroutine(DistanceEffect(TargetOffset, 1f));
        }
        transform.position = Player.transform.position + offset;
        transform.LookAt(Player.transform.position + (Vector3.up));
        
        Vector3 vel = Player.GetComponent<Rigidbody>().velocity;
        Vector3 direction = new Vector3(vel.x, 0, vel.z).normalized;
        float angle = Vector3.Angle(Vector3.forward, direction);
        transform.RotateAround(Player.transform.position, Vector3.up, angle);
        
        // Ne marche pas encore
        /*
        if (Mathf.Abs(oldAngle - angle) > 3)
        {
            if (CoroutineRotationStamp != null)
            {
                StopCoroutine(CoroutineRotationStamp);
                CoroutineRotationStamp = null;
            }
            //CoroutineRotationStamp = StartCoroutine(SmoothRotation(angle, 1f));
        }
        else
        {
            transform.RotateAround(Player.transform.position, Vector3.up, angle);
            oldAngle = angle; 
        }*/
    }

    private IEnumerator DistanceEffect(Vector3 TargetOffset, float duration)
    {
        Vector3 CurrentOffset = offset;
        CameraDistanceValue = Player.GetCameraDistanceValue();
        
        float timer = 0f;
        while (timer < duration)
        {
            offset = Vector3.Lerp(CurrentOffset, TargetOffset, timer / duration);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator SmoothRotation(float TargetAngle, float duration)
    {
        float CurrentAngle = oldAngle;
        oldAngle = TargetAngle; 
        
        float timer = 0f;
        Debug.Log("started");
        while (timer < duration)
        {
            //Debug.Log(Mathf.Lerp(CurrentAngle, TargetAngle, timer / duration));
            transform.RotateAround(Player.transform.position, Vector3.up, Mathf.Lerp(CurrentAngle, TargetAngle, timer / duration));
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("ended");
        transform.RotateAround(Player.transform.position, Vector3.up, TargetAngle);
    }

    private void OnDrawGizmos()
    {
        Vector3 vel = Player.GetComponent<Rigidbody>().velocity;
        Vector3 point1 = Player.transform.position + Vector3.forward;
        Vector3 point2 = Player.transform.position + new Vector3(vel.x, 0, vel.z).normalized;
        
        Gizmos.DrawLine(Player.transform.position, point1);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Player.transform.position, point2);
    }
}

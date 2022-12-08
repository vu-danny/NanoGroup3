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
    private float CameraDistance = 1f;
    private Coroutine CoroutineRotationStamp;
    private float CameraAngle = 0f;
    
    
    void Start()
    {
        BaseOffset = transform.localPosition - Player.transform.localPosition;
        offset = BaseOffset;
    }
 
    void LateUpdate()
    {
        if (Player.GetCameraDistanceValue() != CameraDistance)
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
        float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
        
        // Correction de la rotation, en prenant en compte les valeurs passant au négatif
        if (Mathf.Abs(CameraAngle - angle) > 100f)
        {
            if (angle > CameraAngle) CameraAngle += 360;
            else angle += 360;
        }
        
        if (CoroutineRotationStamp == null && Mathf.Abs(CameraAngle - angle) > 5)
            CoroutineRotationStamp = StartCoroutine(SmoothRotation(angle, .5f));
        
        transform.RotateAround(Player.transform.position, Vector3.up, CameraAngle);
    }

    private IEnumerator DistanceEffect(Vector3 TargetOffset, float duration)
    {
        Vector3 CurrentOffset = offset;
        CameraDistance = Player.GetCameraDistanceValue();
        
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
        float CurrentAngle = CameraAngle;
        
        float timer = 0f;
        while (timer < duration)
        {
            CameraAngle = Mathf.Lerp(CurrentAngle, TargetAngle, timer / duration);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        CameraAngle = TargetAngle;
        CoroutineRotationStamp = null;
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

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

    [SerializeField] private ParticleSystem SpeedVFX;
    public ParticleSystem DashVFX;


    void Start()
    {
        
        BaseOffset = transform.localPosition - Player.transform.localPosition;
        offset = BaseOffset;
    }

    public void SetVFXLayer(LayerMask layer)
    {
        SpeedVFX.gameObject.layer = layer;
    }
 
    /// <summary>
    /// Called after all other functions each frame, this function adapts the Camera position and angle
    /// according to the player position, direction and velocity speed. It calls 2 coroutine that smooth the camera movement.
    /// </summary>
    void LateUpdate()
    {
        if (Player.started)
        {
            // Check if the Camera distance value changed
            if (Player.GetCameraDistanceValue() != CameraDistance)
            {
                float NextDistance = Player.GetCameraDistanceValue();
                // Retrieve the offset, based on player velocity
                Vector3 TargetOffset = new Vector3(0f,
                    BaseOffset.y * (NextDistance / 2),
                    BaseOffset.z * NextDistance);

                // Stops the last coroutine, if it didn't have the time to end
                if (CoroutineDistanceStamp != null)
                {
                    StopCoroutine(CoroutineDistanceStamp);
                    CoroutineDistanceStamp = null;
                }

                CoroutineDistanceStamp = StartCoroutine(DistanceEffect(TargetOffset, 1f));
            }

            // Set the camera position, based on the previously calculated offset
            transform.position = Player.transform.position + offset +
                                 ((Vector3.up + Vector3.back) * (Player.transform.localScale.x - 1));

            // Set the camera rotation, that'll look a bit above the player
            transform.LookAt(
                Player.transform.position + Vector3.up + (Vector3.up * (Player.transform.localScale.y) / 2));

            // Get the angle of the difference between player velocity and world forward vector
            Vector3 vel = Player.GetComponent<Rigidbody>().velocity;
            Vector3 direction = new Vector3(vel.x, 0, vel.z).normalized;
            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

            // Angle correction, in case one of the value (current angle and targeted angle) aren't the same sign
            if (Mathf.Abs(CameraAngle - angle) > 100f)
            {
                if (angle > CameraAngle) CameraAngle += 360;
                else angle += 360;
            }

            // If there is no rotation coroutine yet, and the angle difference is more than 5
            // (to avoid rotation for a small amount), we start the CameraAngle smooth update
            if (CoroutineRotationStamp == null && Mathf.Abs(CameraAngle - angle) > 5)
                CoroutineRotationStamp = StartCoroutine(SmoothRotation(angle, .2f));

            // Set the orbital camera angle
            transform.RotateAround(Player.transform.position, Vector3.up, CameraAngle);

            var emission = SpeedVFX.emission;
            emission.rateOverTime = Player.GetVelocityInterpolation() * 500;
        }
    }

    /// <summary>
    /// Smooth the camera distance offset through a Coroutine
    /// </summary>
    /// <param name="TargetOffset">Targeted camera offset</param>
    /// <param name="duration">Coroutine duration</param>
    /// <returns>CameraDistance parameter value is changed</returns>
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
    
    /// <summary>
    /// Smooth the orbital camera angle around the player through a Coroutine
    /// </summary>
    /// <param name="TargetAngle">Targeted camera angle</param>
    /// <param name="duration">Coroutine duration</param>
    /// <returns>CameraAngle parameter value is changed</returns>
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

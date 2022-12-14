using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPile : MonoBehaviour
{
    [SerializeField] private GameObject PickUpParticle;
    [SerializeField] private int power;
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<SnowballSizer>().Grow(power);
        other.gameObject.GetComponent<Player>()._camera.GetComponent<CameraController>().DashVFX.Stop();
        other.gameObject.GetComponent<Player>()._camera.GetComponent<CameraController>().DashVFX.Play();
        Instantiate(PickUpParticle, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
    }
}

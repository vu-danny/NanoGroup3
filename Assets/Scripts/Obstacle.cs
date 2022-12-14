using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speedMultiplier;
    [SerializeField] int shrinkageLevel;

    [SerializeField] List<GameObject> registered;
    private void OnTriggerEnter(Collider other) {
        if(!registered.Contains(other.gameObject)){
        other.gameObject.GetComponent<SnowballSizer>().Shrink(shrinkageLevel);
        other.GetComponent<Rigidbody>().velocity*=speedMultiplier;
        registered.Add(other.gameObject);
        }
    }

        /* private void OnTriggerStay(Collider other) {
        other.gameObject.GetComponent<SnowballSizer>().Shrink(shrinkageLevel);
        other.GetComponent<Rigidbody>().velocity*=0.98f;
    } */
}

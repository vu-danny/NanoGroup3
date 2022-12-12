using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speedMultiplier;
    [SerializeField] int shrinkageLevel;
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<SnowballSizer>().Shrink(shrinkageLevel);
        other.GetComponent<Rigidbody>().velocity*=speedMultiplier;
    }

        private void OnTriggerStay(Collider other) {
        other.gameObject.GetComponent<SnowballSizer>().Shrink(shrinkageLevel);
        other.GetComponent<Rigidbody>().velocity*=0.98f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPile : MonoBehaviour
{
    [SerializeField] private int power;
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<SnowballSizer>().Grow(power);
        Destroy(gameObject);
    }
}

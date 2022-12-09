using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPile : MonoBehaviour
{
    [SerializeField] private float growthMultiplier;
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<AutoGrowth>().Grow(20);
        Destroy(gameObject);
    }
}

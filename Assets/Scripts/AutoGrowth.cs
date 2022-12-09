using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGrowth : MonoBehaviour
{
    [SerializeField] private float growthFactor;

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.layer == 3)
            transform.localScale += new Vector3(growthFactor, growthFactor, growthFactor);
    }
}

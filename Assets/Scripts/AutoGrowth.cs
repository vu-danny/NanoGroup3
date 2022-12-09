using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGrowth : MonoBehaviour
{
    [SerializeField] private float maxSize;

    [SerializeField] private float minSize;
    
    private float size;

    [SerializeField] private AnimationCurve growthFactor;

    private void Awake() {
        size = minSize;
        transform.localScale *= size;
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.layer == 3){
            Grow();
        }
    }

    public void Grow(int repetitions = 1){
        for(int i = 0; i < repetitions; i++){
            float delta = Mathf.InverseLerp(minSize, maxSize, size);
            float growth = growthFactor.Evaluate(delta);
            if(size + growth > maxSize)
                growth = maxSize - size;
            transform.localScale += new Vector3(growth, growth, growth);
            size += growth;
        }
    }
}

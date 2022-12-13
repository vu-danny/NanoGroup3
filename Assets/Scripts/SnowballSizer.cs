using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballSizer : MonoBehaviour
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
        //TODO tweak snowball physics according to size
    }

    public void Shrink(int repetitions = 1){
        for(int i = 0; i < repetitions*5; i++){
            float delta = Mathf.InverseLerp(minSize, maxSize, size);
            float shrinkage = growthFactor.Evaluate(delta);
            if(size - shrinkage < minSize)
                shrinkage = size - minSize;
            transform.localScale -= new Vector3(shrinkage, shrinkage, shrinkage);
            transform.localPosition -= new Vector3(0, shrinkage/2, 0);
            size -= shrinkage;
        }
        //TODO tweak snowball physics according to size
    }

    public float Size 
    {
        get { return size; }
    }
}

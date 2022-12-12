using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TreeSize : MonoBehaviour
{
    [Range(0.7f,2.3f)]
    public float treeSize = 1;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y != treeSize) 
        {
            transform.localScale = new Vector3(treeSize, treeSize, treeSize);
            
        }

        if (transform.localPosition.y != gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2) 
        {
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2, transform.position.z);
        }
    }
}

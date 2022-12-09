using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TreeRandomizer : MonoBehaviour
{
    public List<Sprite> treeList;
    public float minHeight;
    public float maxHeight;
    public bool updateChanges;
    public float height;
    public bool firstIteration;
    //Render un arbre random en taille et en apparence quand il est drag dans la scène depuis les assets
    void Awake()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == null)
        {
            int index = Random.Range(0, treeList.Count - 1);
            height = Random.Range(minHeight, maxHeight);
            gameObject.GetComponent<SpriteRenderer>().sprite = treeList[index];
            
            firstIteration = true;
        }
    }

    
    void Update()
    {
        if (firstIteration) 
        {
            transform.localScale = new Vector3(height, height, height);
            transform.position = new Vector3(transform.position.x, transform.parent.position.y + gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2, transform.position.z);
            firstIteration = false;
        }
    }
}

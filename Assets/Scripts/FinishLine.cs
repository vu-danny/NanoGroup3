using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log(other.gameObject);
            StartCoroutine(other.GetComponent<Player>().EndRun());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] EndUI endUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null){
            other.GetComponent<Player>().EndRun();

            endUI.AddResult(other.gameObject);
        }
    }
}

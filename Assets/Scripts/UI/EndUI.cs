using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour
{
    [SerializeField] TMP_Text winnerText;
    [SerializeField] GameObject resultLines;
    [SerializeField] GameObject resultLinePrefab;
    private bool first;
    private List<PlayerResultDisplayer> resDisplayers;
    private void Awake(){
        first = true;
        resDisplayers = new List<PlayerResultDisplayer>();
    }

    public void AddResult(GameObject playerObject){
        GameObject resultLine = GameObject.Instantiate(resultLinePrefab, resultLines.transform);
        PlayerResultDisplayer resDisplayer = resultLine.GetComponent<PlayerResultDisplayer>();
        int pNumber = 1; // TODO
        float time = 60; // TODO
        float size = 1; // TODO
        resDisplayer.InitializeValues(first, pNumber, time, size);
        if(first)
            winnerText.text = "Joueur "+pNumber+" l'emporte !";
        first = false;
        resDisplayers.Add(resDisplayer);

        UpdateBiggest();
    }

    private void UpdateBiggest(){
        int biggestIndex = 0;
        float biggestSize = 0;
        for(int i = 0; i<resDisplayers.Count; i++){
            if(resDisplayers[i].ballSize > biggestSize){
                biggestIndex = i;
                biggestSize = resDisplayers[i].ballSize;
            }
        }

        for(int i = 0; i<resDisplayers.Count; i++){
            resDisplayers[i].UpdateBall(i == biggestIndex);
        }
    }
}

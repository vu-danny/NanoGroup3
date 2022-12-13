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

    private List<GameObject> registered;

    [SerializeField] private GameObject progressTracker;

    private void Awake(){
        first = true;
        resDisplayers = new List<PlayerResultDisplayer>();
        registered = new List<GameObject>();
        gameObject.SetActive(false);
    }

    public void AddResult(GameObject playerObject){
            if(!registered.Contains(playerObject)){
            registered.Add(playerObject);
            Player player = playerObject.GetComponent<Player>();
            GameObject resultLine = GameObject.Instantiate(resultLinePrefab, resultLines.transform);
            PlayerResultDisplayer resDisplayer = resultLine.GetComponent<PlayerResultDisplayer>();
            int pNumber = player.number;
            float time = player.Timer;
            float size = playerObject.GetComponent<SnowballSizer>().Size;
            resDisplayer.InitializeValues(first, pNumber, time, size);
            if(first)
                winnerText.text = "Joueur "+pNumber+" l'emporte !";
            else
                progressTracker.SetActive(false);
            first = false;
            resDisplayers.Add(resDisplayer);

            UpdateBiggest();
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    [SerializeField] private List<Transform> playerTransforms;

    [SerializeField] private Transform goalTransform;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    private List<float> playerProgress;
    private List<int> playerRanking;
    [SerializeField] private GameObject UITrack;

    private float trackHeight;

    [SerializeField] private List<RectTransform> playerTrackIcons;

    [SerializeField] private List<TMP_Text> playerRankText;
    
    void Awake(){
        playerProgress = new List<float>();
        playerRanking = new List<int>();
        for(int i = 0; i < playerTransforms.Count; i++){
            playerProgress.Add(0.0f);
            playerRanking.Add(i+1);
        }
        trackHeight = UITrack.GetComponent<RectTransform>().sizeDelta.y;
    }

    void Update()
    {
        UpdatePlayerProgress();
        UpdateRanking();
    }

    void UpdatePlayerProgress(){
        for(int i = 0; i < playerTransforms.Count; i++){
            Transform player = playerTransforms[i];
            float distance = Vector3.Distance(player.position, goalTransform.position);
            playerProgress[i] = Mathf.InverseLerp(maxDistance, minDistance, distance);
            playerTrackIcons[i].anchoredPosition = new Vector3(playerTrackIcons[i].anchoredPosition.x, playerProgress[i]*trackHeight, 0);
        }

    }

    void UpdateRanking(){
        bool changed = false;
        if(playerTransforms.Count == 2){
            if(playerProgress[0] >= playerProgress [1]){
                if (playerRanking[0] != 1)
                    changed = true;
                playerRanking[0] = 1;
                playerRanking[1] = 2;
            }
            else{
                if (playerRanking[0] != 2)
                    changed = true;
                playerRanking[0] = 2;
                playerRanking[1] = 1;
            }
        }
        if(changed){
            // TODO add bouncy animation on text change
            playerRankText[0].text = ""+playerRanking[0];
            playerRankText[1].text = ""+playerRanking[1];
        }
    }
    
}

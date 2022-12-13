using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class UIBlur : MonoBehaviour
{
    public Volume myVolume;
    public DepthOfField depthOfField;
    public bool activeUI;
    public float maxBlur;
    public float minBlur;
    public float blurSpeed;
    public float target;
    public List<GameObject> menus;
    // Start is called before the first frame update
    void Start()
    {
        int allActiveUI = 0;
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].activeInHierarchy)
            {
                allActiveUI++;
            }
        }
        if (allActiveUI > 0)
        {
            activeUI = true;
        }
        else
        {
            activeUI = false;
        }
        if (gameObject.GetComponent<Volume>()) 
        {
            myVolume = gameObject.GetComponent<Volume>();
            if (myVolume.sharedProfile.TryGet<DepthOfField>(out depthOfField))
            {
                Debug.Log("Focus distance : " + depthOfField.focusDistance.value);
                if (activeUI) 
                {
                    depthOfField.focalLength.value = maxBlur;
                }
                else 
                {
                    depthOfField.focalLength.value = minBlur;
                }
            }
            else
            {
                Debug.Log("There is no depth of field");
            }
            //depthOfField = myVolume.GetComponent<DepthOfField>();
        }
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        for (int i =0; i<cameras.Length; i++) 
        {
            cameras[i].GetComponent<Camera>().GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeUI) 
        {
            target = maxBlur;
        }
        if (!activeUI) 
        {
            target = minBlur;
        }

        if (myVolume.sharedProfile.TryGet<DepthOfField>(out depthOfField))
        {
            //Debug.Log("Focus distance : " + depthOfField.focusDistance.value);
            depthOfField.focalLength.value = Mathf.Lerp(depthOfField.focalLength.value,target,blurSpeed*Time.deltaTime);
        }
        else
        {
            Debug.Log("There is no depth of field");
        }

        int allActiveUI = 0;
        for (int i = 0; i < menus.Count; i++) 
        { 
          if (menus[i].activeInHierarchy) 
            {
                allActiveUI++;
            }
        }
        if (allActiveUI > 0) 
        {
            activeUI = true;
        }
        else 
        {
            activeUI = false;
        }
        
    }
}

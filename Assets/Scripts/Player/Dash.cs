using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public KeyCode dashKey;
    public Vector3 initVelocity;
    public float maxSpeedDash;
    public float accelerationDuration;
    public float decelerationDuration;
    public SnowballSizer snowballSizer;
    public Player player;
    public GameObject speedObject;
    public float timer = 0;
    public bool justDashed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DashByKey();
        Debug.Log("SnowBall size : " + snowballSizer.Size);
    }

    public void DashByKey() 
    { 
        if (Input.GetKeyDown(dashKey)) 
        {
            initVelocity = speedObject.GetComponent<Rigidbody>().velocity;
            timer = 0;
        }
        if (Input.GetKey(dashKey) && snowballSizer.Size>1) 
        {
            if (snowballSizer.Size <= 1)
            {
                justDashed = true;
            }
            if (!justDashed) 
            {
                if (timer < accelerationDuration)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    justDashed = true;
                    timer = 0;
                }
            }
            else if (justDashed) 
            { 
            
            }
            snowballSizer.Shrink();
            transform.localScale = new Vector3 (snowballSizer.Size, snowballSizer.Size, snowballSizer.Size);


        }
        else if (justDashed) 
        {
            if (timer < decelerationDuration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                justDashed = false;
                timer = 0;
            }
        }
    }
    

    public void DashByCollecting() 
    { 
        if (!justDashed) 
        { 
            if (timer < accelerationDuration) 
            {
                timer += Time.deltaTime;
            }
            else 
            {
                justDashed = true;
                timer = 0;
            }
            DashByCollecting();
        }
        else if (justDashed) 
        {
            if (timer < decelerationDuration)
            {
                timer += Time.deltaTime;
                DashByCollecting();
            }
            else
            {
                justDashed = false;
                timer = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance Roll;
    private FMOD.Studio.EventInstance HitObstacle;
    private FMOD.Studio.EventInstance HitPlayer;
    private FMOD.Studio.EventInstance PowerUp;

    // Start is called before the first frame update
    void Start()
    {
        Roll = FMODUnity.RuntimeManager.CreateInstance("event:/Roll");
        HitObstacle = FMODUnity.RuntimeManager.CreateInstance("event:/ImpactObstacle");
        HitPlayer = FMODUnity.RuntimeManager.CreateInstance("event:/ImpactBalls");
        PowerUp = FMODUnity.RuntimeManager.CreateInstance("event:/Bonus");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
      if(other.gameObject.layer == 3)
      {
        Roll.start();
      }

      if(other.gameObject.tag == "Player")
      {
        HitPlayer.start();
      }
    }
    private void OnCollisionExit(Collision other)
    {
      if(other.gameObject.layer == 3)
      {
        Roll.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.layer == 8)
      {
        HitObstacle.start();
      }

      if(other.gameObject.layer == 6)
      {
        PowerUp.start();
      }
    }
}

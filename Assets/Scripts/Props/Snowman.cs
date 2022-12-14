using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    [SerializeField] private Transform PlayerPosition;
    [SerializeField] private Transform EndCameraPoint;
    [SerializeField] private AnimationCurve JumpCurve;
    [System.NonSerialized] public Player _player;
    
    public void Build(Player player, Animator FaceReveal)
    {
        _player = player;
        StartCoroutine(PlayerLandOnSnowman(3f, FaceReveal));
    }

    private void Update()
    {
        if(_player != null){
        transform.localScale = _player.transform.localScale;
        }
    }

    private IEnumerator PlayerLandOnSnowman(float duration, Animator FaceReveal)
    {
        Vector3 InitPos = _player.transform.position;
        Vector3 InitCameraPos = _player._camera.transform.position;
        float timer = 0f;
        
        // Jump on snowman
        while (timer < duration)
        {
            float y = JumpCurve.Evaluate(timer / duration) + Mathf.Lerp(InitPos.y, PlayerPosition.position.y, timer/duration);
            Vector3 translation = Vector3.Lerp(InitPos, PlayerPosition.position, timer / duration);
            _player.transform.position = new Vector3(translation.x, y, translation.z);
            _player._camera.transform.position = Vector3.Lerp(InitCameraPos, EndCameraPoint.position, timer / duration);
            _player._camera.transform.LookAt(_player.transform.position);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _player.transform.position = PlayerPosition.position;
        
        // Face Reveal
        FaceReveal.gameObject.transform.rotation = Quaternion.LookRotation(-transform.right);
        FaceReveal.gameObject.transform.Rotate(Vector3.left * 90);
        FaceReveal.SetTrigger("Reveal");

        _player.Arrived = true;
        yield return new WaitForSeconds(1f);
        GameManager.instance.CheckForEnd();
    }
}

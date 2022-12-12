using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    [SerializeField] private Transform PlayerPosition;
    [SerializeField] private AnimationCurve JumpCurve;
    private Player _player;
    
    public void Build(Player player)
    {
        _player = player;
        transform.localScale = _player.transform.localScale;
        StartCoroutine(PlayerLandOnSnowman(3f));
    }

    private IEnumerator PlayerLandOnSnowman(float duration)
    {
        Vector3 InitPos = _player.transform.position;
        float timer = 0f;
        
        while (timer < duration)
        {
            float y = JumpCurve.Evaluate(timer / duration) + Mathf.Lerp(InitPos.y, PlayerPosition.position.y, timer/duration);
            Vector3 translation = Vector3.Lerp(InitPos, PlayerPosition.position, timer / duration);
            _player.transform.position = new Vector3(translation.x, y, translation.z);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _player.transform.position = PlayerPosition.position;

        _player.Arrived = true;
        yield return new WaitForSeconds(1f);
        GameManager.instance.CheckForEnd();
    }
}

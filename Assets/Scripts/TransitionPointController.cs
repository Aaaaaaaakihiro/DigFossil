using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPointController : MonoBehaviour
{
    //インスペクターで遷移する先のシーンを選ぶ
    [SerializeField] private SceneData.GameState _scene = SceneData.GameState.DIG;

    //プレイヤーがコリジョンに入ってるかどうか
    private bool isPlayerEnteringCollision = false;

    void Update()
    {
        if (isPlayerEnteringCollision)
        {
            GameLoopManager.instance.dispatch(_scene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerEnteringCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerEnteringCollision = false;
        }
    }
}

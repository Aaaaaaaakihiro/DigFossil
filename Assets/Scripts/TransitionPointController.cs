using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPointController : MonoBehaviour
{
    //インスペクターで遷移する先のシーンを選ぶ
    [SerializeField] private SceneData.GameState _scene = SceneData.GameState.DIG;
    //Playerがコリジョンに入ったかどうか
    private bool isPlayerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerEntered)
        {
            isPlayerEntered = true;
            Debug.Log("Playerがマップ" + _scene + "へ移動");
            var _gameLoopManager = GameObject.FindGameObjectWithTag("GameLoopManager").GetComponent<GameLoopManager>();
            _gameLoopManager.dispatch(_scene);
        }
    }
}

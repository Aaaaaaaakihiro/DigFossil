using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterExplore : MonoBehaviour
{
    private bool isPlayerEnteringCollision = false;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerEnteringCollision)
        {
            //プレイヤーがコライダー内に入ると探索シーンにシーン遷移
            GameLoopManager.instance.dispatch(GameLoopManager.GameState.EXPLORE);

            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    GameLoopManager.instance.dispatch(GameLoopManager.GameState.EXPLORE);
            //}
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

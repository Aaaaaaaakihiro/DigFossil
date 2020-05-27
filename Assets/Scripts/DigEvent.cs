using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigEvent : MonoBehaviour
{
    private bool isPlayerEnteringCollision = false;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerEnteringCollision)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GameLoopManager.instance.dispatch(GameLoopManager.GameState.DIG);
            }
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

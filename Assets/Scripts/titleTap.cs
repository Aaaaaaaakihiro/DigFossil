using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleTap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startGame()
    {
        GameLoopManager.instance.dispatch(GameLoopManager.GameState.TOWN);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

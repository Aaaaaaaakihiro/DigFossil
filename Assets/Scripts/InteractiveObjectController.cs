using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectController : MonoBehaviour
{
    //イベントの種類
    private enum _eventList
    {
        _digEvent,
    }

    [SerializeField] private _eventList _event;

    public void OnUserAction()
    {
        if (_event == _eventList._digEvent)
        {
            GameLoopManager.instance.dispatch(GameLoopManager.GameState.DIG);
        }
    }
}

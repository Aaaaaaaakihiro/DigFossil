using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigResultMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform panelRect;

    // 獲得したアイテムが4つ以上のときはtrue
    [HideInInspector]
    public bool isOver3;

    private Vector2 beforeTouchPos;


    void Start()
    {
        beforeTouchPos = new Vector2(-1, -1);
    }

    void Update()
    {
        if (isOver3)
        {
            if(Input.touchCount == 1)
            {
                Vector2 touchPos = Input.GetTouch(0).position;
                if(beforeTouchPos == -Vector2.one)
                {
                    beforeTouchPos = touchPos;
                }
                else
                {
                    Vector2 diff = touchPos - beforeTouchPos;
                    panelRect.localPosition += (Vector3)diff;
                    beforeTouchPos = touchPos;
                }
            }
        }
    }
}

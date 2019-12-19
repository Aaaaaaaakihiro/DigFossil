using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigResult : MonoBehaviour
{
    private RectTransform stopPointRect;
    private RectTransform resultPanelRect;
    [SerializeField]
    private float moveSpeed;


    void Start()
    {
        stopPointRect = GameObject.Find("StopPoint").GetComponent<RectTransform>();
        resultPanelRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        
    }

    public void Open(){
        StartCoroutine(OpenCor());
    }

    private IEnumerator OpenCor(){
        bool reached = false;
        while(reached == false)
        {
            Vector2 pos = resultPanelRect.localPosition;
            pos.y += moveSpeed * Time.deltaTime;
            resultPanelRect.localPosition = pos;
            if(pos.y >= stopPointRect.localPosition.y)
            {
                reached = true;
                resultPanelRect.localPosition = stopPointRect.localPosition;
            }
            yield return null;
        }
    }
}

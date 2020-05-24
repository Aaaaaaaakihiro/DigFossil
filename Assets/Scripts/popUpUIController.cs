using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpUIController : MonoBehaviour
{
    private GameObject canvas;
    private RectTransform canvasRectTfm;
    private Transform targetTfm;
    private RectTransform myRectTfm;
    private Vector3 offset = new Vector3(0, 1.5f, 0);

    [SerializeField]
    private float offsetNum = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvasRectTfm = canvas.GetComponent<RectTransform>();
        targetTfm = GameObject.Find("Player").transform;
        myRectTfm = this.transform.GetComponent<RectTransform>();
        offset = new Vector3(0, offsetNum, 0);

        this.transform.SetParent(canvas.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(0, offsetNum, 0);
        myRectTfm.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);
    }
}

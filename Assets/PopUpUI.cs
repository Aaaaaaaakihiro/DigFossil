using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    [SerializeField]
    private GameObject popUpPrefab;

    private GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void makeUI()
    {
        if(!GameObject.Find("popUp(Clone)"))
        Instantiate(popUpPrefab);
    }

    private void destroyUI()
    {
        if (GameObject.Find("popUp(Clone)"))
        {
            GameObject instanceOfPU;
            instanceOfPU = GameObject.Find("popUp(Clone)");
            Object.Destroy(instanceOfPU);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("タグ名:" + other.gameObject.tag + " のオブジェクト  を検知");
        if (other.gameObject.tag != "Player")
        {
            makeUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            destroyUI();
        }
    }
}

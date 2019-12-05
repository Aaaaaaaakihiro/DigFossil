using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigToolBase : MonoBehaviour
{
    //道具のパワー
    [SerializeField] protected int power;
    //レイヤーマスク
    protected int layerMask;

    

    protected void Start()
    {
        layerMask = LayerMask.NameToLayer("DigBlock");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Dig();
        }
    }

    protected virtual void Dig()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, layerMask))
        {
            DigBlock block = hit.collider.gameObject.GetComponent<DigBlock>();
            block.Dig(power);
        }
    }
}

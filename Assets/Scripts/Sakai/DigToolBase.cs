using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DigToolBase : MonoBehaviour
{
    //道具のパワー
    //[SerializeField] protected int power;
    public int power;
    //レイヤーマスク
    [SerializeField] protected LayerMask targetLayer;

    

    protected void Start()
    {

    }

    void Update()
    {

    }

    //掘る作業のヴァーチャルメソッド
    //現状はボタンで実装しているので、これは不要
    protected virtual void Dig()
    {

    }
}

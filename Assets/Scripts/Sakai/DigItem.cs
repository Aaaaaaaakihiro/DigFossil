﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPart
{
    HEAD = 0,
    BODY,
    LEG
}


public class DigItem : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] private List<Vector2> itemIndexes;
    public int point{get; private set;}
    public string itemName { get; private set; }
    [SerializeField]
    private AudioClip foundSound;
    private AudioSource sePlayer;
    private DigResult digResult;
    // アイテムの部位（頭，胴体，足のいずれかになる）
    [HideInInspector]
    public ItemPart itemPart;

    void Start()
    {
        sePlayer = Camera.main.GetComponent<AudioSource>();
        digResult = GameObject.Find("ResultPanel").GetComponent<DigResult>();
        string[] names = new string[] { "何かの頭", "何かの胴体", "何かの足" };
        itemPart = (ItemPart)Random.Range(0, 3);
        itemName = names[(int)itemPart];
        Debug.Log(itemName);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 発見したかどうかを判定する
    /// </summary>
    /// <param name="x">壊したブロックのXインデックス</param>
    /// <param name="y">壊したブロックのYインデックス</param>
    public void CheckIsFound(int x, int y)
    {
        bool isBroken = false;
        for(int i = 0; i < itemIndexes.Count; i++)
        {
            if(itemIndexes[i].x == x && itemIndexes[i].y == y)
            {
                itemIndexes.RemoveAt(i);
                isBroken = true;
                break;
            }
        }
        if(isBroken && itemIndexes.Count == 0)
        {
            OnFound();
        }
        
    }
    
    /// <summary>
    /// 発見時の処理を行う
    /// </summary>
    private void OnFound()
    {
        sePlayer.PlayOneShot(foundSound);
        digResult.getItems.Add(this);
    }

    /// <summary>
    /// アイテムのフィールド内インデックス情報を保存する
    /// </summary>
    /// <param name="x">フィールド内インデックスのX</param>
    /// <param name="y">フィールド内インデックスのY</param>
    public void AddItemIndex(int x, int y)
    {
        if(itemIndexes == null)
        {
            itemIndexes = new List<Vector2>();
        }
        itemIndexes.Add(new Vector2(x, y));
    }
}
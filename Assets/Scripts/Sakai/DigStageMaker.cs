using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigStageMaker : MonoBehaviour
{
    //ブロックのプレハブ
    [SerializeField] private GameObject digBlock;
    //ブロックを生成するパネル
    [SerializeField] private GameObject basePanel;
    //幅、横の数
    [SerializeField] private int width;
    //高さ、縦の数
    [SerializeField] private int height;
    //ブロック生成の親
    private GameObject blockParent;

    void Start()
    {
        MakeStage(digBlock, width, height, blockParent);
    }

    /// <summary>
    /// 縦と横の数に応じてステージを作成する
    /// </summary>
    /// <param name="block">生成するブロック</param>
    /// <param name="w">幅、横の数</param>
    /// <param name="h">高さ、縦の数</param>
    /// <param name="parent">生成するときの起点となる親</param>
    private void MakeStage(GameObject block, int w, int h, GameObject parent)
    {
        RectTransform panelRect = basePanel.GetComponent<RectTransform>();
        float blockwidth = panelRect.rect.width / w;
        float blockHeight = panelRect.rect.height / h;

        RectTransform parentRect = parent.GetComponent<RectTransform>();
        parentRect.position = new Vector2(blockwidth / 2, parentRect.rect.height - (blockHeight / 2));

        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < w; j++)
            {
                GameObject b = GameObject.Instantiate(block, parentRect, false);
                RectTransform rect = b.GetComponent<RectTransform>();
                rect.SetParent(parentRect);
                rect.sizeDelta = Vector2.one;
                b.SetActive(true);
                rect.localPosition = new Vector2( blockwidth * w, blockHeight * h);
            }
        }


    }



    void Update()
    {
        
    }
}

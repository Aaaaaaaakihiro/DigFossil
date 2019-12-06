using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigStageMaker : MonoBehaviour
{
    //ブロックのプレハブ
    [SerializeField] private GameObject digBlock;
    //ブロックを生成するパネル
    private GameObject basePanel;
    //幅、横の数
    [SerializeField] private int width;
    //高さ、縦の数
    [SerializeField] private int height;
    //ブロック生成の親
    private GameObject blockParent;
    //ブロックの情報を入れる配列
    private DigBlock[,] digBlocks;
    //ステージの耐久値
    [SerializeField] private int stageHealth;
    //ステージの耐久値を表示するスライダー
    [SerializeField] private Slider healthGage;

    void Start()
    {
        basePanel = GameObject.Find("BasePanel");
        blockParent = GameObject.Find("BlockParent");
        healthGage = GameObject.Find("HealthGage").GetComponent<Slider>();
        healthGage.maxValue = stageHealth;
        healthGage.value = stageHealth;
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
        parentRect.position = new Vector2(blockwidth / 2, panelRect.rect.height - (blockHeight / 2));

        digBlocks = new DigBlock[h, w];

        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < w; j++)
            {
                GameObject b = Instantiate(block, parentRect, false);
                RectTransform rect = b.GetComponent<RectTransform>();
                rect.SetParent(parentRect);
                rect.localScale = Vector2.one;
                rect.sizeDelta = new Vector2(blockwidth, blockHeight);
                b.SetActive(true);
                rect.localPosition = new Vector2( blockwidth * j, -blockHeight * i);
                b.GetComponent<DigBlock>().StartSetting();
                digBlocks[i, j] = b.GetComponent<DigBlock>();
            }
        }
    }



    void Update()
    {
        
    }

    public void DamageStage(int damage)
    {
        stageHealth -= damage;
        healthGage.value = stageHealth;
        if(stageHealth <= 0)
        {
            Debug.Log("Stage Broken");
        }
    }
}

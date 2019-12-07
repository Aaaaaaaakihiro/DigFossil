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
    //private DigBlock[,] digBlocks;
    private bool[,] digBlockExist;
    //アイテムが存在するかどうかを管理する配列
    private bool[,] itemExist;
    //ステージの耐久値
    [SerializeField] private int stageHealth;
    //ステージの耐久値を表示するスライダー
    [SerializeField] private Slider healthGage;
    //ブロックの横幅
    private float blockWidth;
    //ブロックの縦幅
    private float blockHeight;

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
        blockWidth = panelRect.rect.width / w;
        blockHeight = panelRect.rect.height / h;

        RectTransform parentRect = parent.GetComponent<RectTransform>();
        parentRect.position = new Vector2(blockWidth / 2, panelRect.rect.height - (blockHeight / 2));

        digBlockExist = new bool[h, w];
        itemExist = new bool[h, w];

        for(int i = 0; i < h; i++)
        {
            for(int j = 0; j < w; j++)
            {
                GameObject b = Instantiate(block, parentRect, false);
                RectTransform rect = b.GetComponent<RectTransform>();
                rect.SetParent(parentRect);
                rect.localScale = Vector2.one;
                rect.sizeDelta = new Vector2(blockWidth, blockHeight);
                b.SetActive(true);
                rect.localPosition = new Vector2( blockWidth * j, -blockHeight * i);
                b.GetComponent<DigBlock>().StartSetting();
                //digBlocks[i, j] = b.GetComponent<DigBlock>();
                digBlockExist[i, j] = true;
            }
        }
    }



    void Update()
    {
        
    }

    private void SetItemOnRandomPoint(DigItem item, int stageWidth, int stageHeight, int blockWidth, int blockHeight)
    {
        //はみ出したりすることがないように限界を算出する
        int bottomRightX = stageWidth - item.width;
        int bottomRightY = stageHeight - item.height;

        //限定した範囲内でランダムな地点を決める
        int spawnX = Random.Range(0, bottomRightX + 1);
        int spawnY = Random.Range(0, bottomRightY + 1);

        //ここから実際の生成処理

        //まずはアイテムのインデックスに情報を登録
        for(int i = spawnY; i < spawnY + blockHeight; i++)
        {
            for(int j = spawnX; j < spawnX + blockWidth; j++)
            {
                itemExist[j, i] = true;
            }
        }




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

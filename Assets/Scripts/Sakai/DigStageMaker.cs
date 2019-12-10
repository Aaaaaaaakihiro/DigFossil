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
    private bool[,] digBlockExist;
    //アイテムが存在するかどうかを管理する配列
    private bool[,] itemExist;
    //アイテム生成の親　位置的にはブロック生成の親と同じ
    private GameObject itemParent;
    //ステージの耐久値
    [SerializeField] private int stageHealth;
    //ステージの耐久値を表示するスライダー
    [SerializeField] private Slider healthGage;
    //ブロックの横幅
    private float blockWidth;
    //ブロックの縦幅
    private float blockHeight;
    [SerializeField] private GameObject greenRockItem;

    void Start()
    {
        basePanel = GameObject.Find("BasePanel");
        blockParent = GameObject.Find("BlockParent");
        itemParent = GameObject.Find("ItemParent");
        healthGage = GameObject.Find("HealthGage").GetComponent<Slider>();
        healthGage.maxValue = stageHealth;
        healthGage.value = stageHealth;
        MakeStage(digBlock, width, height, blockParent, itemParent);
    }

    /// <summary>
    /// 縦と横の数に応じてステージを作成する
    /// </summary>
    /// <param name="block">生成するブロック</param>
    /// <param name="w">幅、横の数</param>
    /// <param name="h">高さ、縦の数</param>
    /// <param name="parent">生成するときの起点となる親</param>
    private void MakeStage(GameObject block, int w, int h, GameObject parent, GameObject itemParent)
    {
        RectTransform panelRect = basePanel.GetComponent<RectTransform>();
        blockWidth = panelRect.rect.width / w;
        blockHeight = panelRect.rect.height / h;

        //ブロック生成の親の位置を調整
        RectTransform parentRect = parent.GetComponent<RectTransform>();
        parentRect.position = new Vector2(blockWidth / 2, panelRect.rect.height - (blockHeight / 2));

        //アイテム生成の親の位置を調整
        RectTransform itemParentRect = itemParent.GetComponent<RectTransform>();
        itemParentRect.position = new Vector2(blockWidth / 2, panelRect.rect.height - (blockHeight / 2));


        digBlockExist = new bool[h, w];
        itemExist = new bool[h, w];

        for (int i = 0; i < h; i++)
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
                b.GetComponent<DigBlock>().StartSetting(j,i);
                digBlockExist[i, j] = true;
            }
        }

        for(int i = 0; i < 3; i++)
        {
            SetItemOnRandomPoint(greenRockItem.GetComponent<DigItem>(),
                width,
                height,
                blockWidth,
                blockHeight,
                itemParent.GetComponent<RectTransform>());
        }

        
    }

    void Update()
    {
        
    }
    /// <summary>
    /// ランダムな位置にアイテムを生成する
    /// </summary>
    /// <param name="item">生成するアイテムクラス</param>
    /// <param name="stageWidth">ステージの横の数</param>
    /// <param name="stageHeight">ステージの縦の数</param>
    /// <param name="blockWidth">ブロックの横幅</param>
    /// <param name="blockHeight">ブロックの縦幅</param>
    /// <param name="itemParentRect">アイテム生成の親のRectTransform</param>
    private void SetItemOnRandomPoint(DigItem item, int stageWidth, int stageHeight, float blockWidth, float blockHeight, RectTransform itemParentRect)
    {
        //はみ出したりすることがないように限界を算出する
        int bottomRightX = stageWidth - item.width;
        int bottomRightY = stageHeight - item.height;

        //範囲内でランダムな位置に生成地点を定める
        int spawnX = Random.Range(0, bottomRightX);
        int spawnY = Random.Range(0, bottomRightY);

        //ほかのアイテムと被っていないかチェック
        //仮にかぶっていたら強制的に終了（何も生成しない）
        for (int i = 0; i < item.height ; i++)
        {
            for (int j = 0; j < item.width ; j++)
            {
                if (itemExist[spawnY + i, spawnX + j] == true)
                    return;
            }
        }


        //次に生成する座標を算出
        float centerIndexX = (spawnX + spawnX + item.width) / 2;
        float centerIndexY = (spawnY + spawnY + item.height) / 2;

        //生成処理
        GameObject it = Instantiate(item.gameObject, itemParentRect, false);

        //生成したオブジェクトに情報を付与
        DigItem dig = it.GetComponent<DigItem>();
        for (int i = 0; i < item.height; i++)
        {
            for (int j = 0; j < item.width; j++)
            {
                dig.AddItemIndex(spawnX + j, spawnY + i);
                itemExist[spawnY + i, spawnX + j] = true;
            }
        }

        //大きさと位置を整える
        RectTransform itemRect = it.GetComponent<RectTransform>();
        itemRect.SetParent(itemParentRect);
        itemRect.localScale = Vector3.one;
        itemRect.sizeDelta = new Vector2(blockWidth * item.width, blockHeight * item.height);
        it.SetActive(true);
        itemRect.localPosition = new Vector2(blockWidth * centerIndexX - (blockWidth / 2), blockHeight * -centerIndexY + (blockHeight / 2));
    }

    /// <summary>
    /// 指定した位置にブロックを破壊する
    /// </summary>
    /// <param name="x">指定座標のX成分</param>
    /// <param name="y">指定座標のY成分</param>
    public void DestroyBlock(int x, int y)
    {
        digBlockExist[y, x] = false;
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < items.Length; i++)
        {
            DigItem item = items[i].GetComponent<DigItem>();
            item.CheckIsFound(x, y);
        }
    }

    /// <summary>
    /// 道具使用時にステージにダメージを与える
    /// </summary>
    /// <param name="damage">与えるダメージ</param>
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

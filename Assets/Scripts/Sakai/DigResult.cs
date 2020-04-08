using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigResult : MonoBehaviour
{
    // 停止位置
    private RectTransform stopPointRect;
    
    // リザルト表示パネル
    private RectTransform resultPanelRect;

    // 移動速度
    [SerializeField]
    private float moveSpeed;

    // 結果テキスト
    [SerializeField]
    private Text resultText;

    // 取得したアイテムのリスト
    public List<DigItem> getItems;

    // アイテム表示用のScrolView
    [SerializeField]
    private ScrollRect scrollRect;

    // 結果画面のアイテムのプレハブ
    [SerializeField]
    private RectTransform resultItemPrefab;

    // 結果画面のアイテムの親オブジェクト
    [SerializeField]
    private RectTransform itemParentRect;

    // アイテムの名前の配列
    [SerializeField]
    private List<string> itemNames;

    // アイテムの鑑定前の画像の配列
    [SerializeField]
    private List<Sprite> itemSprites;

    // 結果表示部分の位置の上限
    private int maxYofParent;

    // 結果表示部分の位置の下限
    private int minYofParent;

    // 結果表示画面のモード切り替えの境界となるアイテム数
    [SerializeField]
    private int itemRectBoarder;



    void Start()
    {
        stopPointRect = GameObject.Find("StopPoint").GetComponent<RectTransform>();
        resultPanelRect = GetComponent<RectTransform>();
        resultText = GameObject.Find("ResultMessage").GetComponent<Text>();
        getItems = new List<DigItem>();
        scrollRect.horizontal = false;
        maxYofParent = (int)itemParentRect.localPosition.y;
    }

    void Update()
    {
        if(itemParentRect.localPosition.y > maxYofParent)
        {
            Vector2 pos = itemParentRect.localPosition;
            pos.y--;
            itemParentRect.localPosition = pos;
        }else if(itemParentRect.localPosition.y < minYofParent)
        {
            Vector2 pos = itemParentRect.localPosition;
            pos.y++;
            itemParentRect.localPosition = pos;
        }
    }

    public void Open(){
        StartCoroutine(OpenCor());
        ShowResult();
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


    // 結果を表示する
    private void ShowResult()
    {
        if(getItems.Count == 0)
        {
            resultText.text = "採掘失敗...(-<>-)";
        }
        else
        {
            resultText.text = "採掘成功!!!(^<>^)";

            

            // 取得したアイテムを集計
            Dictionary<string, int> items = new Dictionary<string, int>();
            foreach(DigItem item in getItems)
            {
                if (items.ContainsKey(item.itemName))
                    items[item.itemName]++;
                else
                    items.Add(item.itemName, 1);
            }

            if (items.Count <= itemRectBoarder)
            {
                scrollRect.vertical = false;
            }
            else
            {
                scrollRect.vertical = true;
            }

            // 初期位置決定
            Vector2 resultItemOrigin = new Vector2(490, -150);

            // Y座標の変化量
            int diff = 150;

            int count = 0;

            // 各アイテムを表示
            foreach(string key in items.Keys)
            {
                count++;
                RectTransform itemRect = Instantiate(resultItemPrefab, transform.position, Quaternion.identity);
                itemRect.SetParent(itemParentRect);
                itemRect.localScale = Vector3.one;
                itemRect.localPosition = resultItemOrigin;
                resultItemOrigin.y -= diff;
                itemRect.transform.GetChild(0).GetComponent<Image>().sprite = itemSprites[itemNames.IndexOf(key)];
                itemRect.transform.GetChild(1).GetComponent<Text>().text = key + "(x" + items[key] + ")";
            }

            minYofParent = maxYofParent - diff * count;
        }
    }
}

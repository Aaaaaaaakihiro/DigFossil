using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DigBlock : MonoBehaviour
{

    //ブロックの耐久値
    //[HideInInspector] 
    public int blockHealth;
    //ブロックの状態を表すスプライト
    [SerializeField] private Sprite[] blockStatusSprites;
    //ブロックの画像を入れるRawImage
    private Image blockImage;
    private DigTool digTool;
    private DigStageMaker digStageMaker;
    private int pos_x, pos_y;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void StartSetting(int x, int y)
    {
        digTool = GameObject.Find("DigTool").GetComponent<DigTool>();
        blockHealth = 3;
        SetSprite();
        //最終的にブロックをImageにしてもButtonにしても対応できるように設定
        if(GetComponent<Button>() != null)
            GetComponent<Button>().onClick.AddListener(() => { Dig(digTool.GetPower()); });
        digStageMaker = GameObject.Find("BasePanel").GetComponent<DigStageMaker>();
        pos_x = x;
        pos_y = y;
    }


    public void OnClicked(BaseEventData data)
    {
        PointerEventData pointerEventData = (PointerEventData)data;
        GameObject result = pointerEventData.pointerCurrentRaycast.gameObject;
        if(result.tag == "DigBlock")
        {
            result.GetComponent<DigBlock>().Dig(digTool.GetPower());
        }
    }

    /// <summary>
    /// 耐久値が０であるかどうかを調べる
    /// </summary>
    /// <returns>耐久値が０ならtrue、０でなければfalse</returns>
    private bool CheckState()
    {
        return blockHealth == 0;
    }

    /// <summary>
    /// 現在の耐久値に応じて画像を変える
    /// </summary>
    private void SetSprite()
    {
        if (blockImage == null)
            blockImage = GetComponent<Image>();

        if(blockStatusSprites != null && blockStatusSprites.Length != 0)
            blockImage.sprite = blockStatusSprites[blockHealth];
        else
        {
            Color[] colors = { Color.red, Color.yellow, Color.green };
            blockImage.color = colors[blockHealth - 1];
        }
    }

    /// <summary>
    /// パワーの分だけ耐久値を減らす
    /// 耐久値が０になったら非表示にする
    /// </summary>
    /// <param name="power">ハンマーなどの掘る道具のパワー</param>
    public virtual void Dig(int power)
    {
        blockHealth -= power;
        if (CheckState())
        {
            gameObject.SetActive(false);
            digStageMaker.DestroyBlock(pos_x, pos_y);
        }
        else
            SetSprite();
    }
}

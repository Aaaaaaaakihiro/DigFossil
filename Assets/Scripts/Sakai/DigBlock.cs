using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigBlock : MonoBehaviour
{

    //ブロックの耐久値
    [HideInInspector] public int blockHealth;
    //ブロックの状態を表すスプライト
    [SerializeField] private Sprite[] blockStatusSprites;
    //ブロックの画像を入れるRawImage
    private Image blockImage;

    void Start()
    {
        SetSprite();
    }

    void Update()
    {
        
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
        blockImage.sprite = blockStatusSprites[blockHealth];
    }

    /// <summary>
    /// パワーの分だけ耐久値を減らす
    /// 耐久値が０になったら非表示にする
    /// </summary>
    /// <param name="power">ハンマーなどの掘る道具のパワー</param>
    public void Dig(int power)
    {
        blockHealth -= power;
        SetSprite();
        if (CheckState())
            gameObject.SetActive(false);
    }
}

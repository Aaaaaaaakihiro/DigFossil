using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DigToolChoice
{
    Hammer = 0,
    Pikkeru
}

public class DigTool : MonoBehaviour
{
    //道具のパワー
    [SerializeField] private int[] power;
    //レイヤーマスク
    //[SerializeField] 
    //private LayerMask targetLayer;
    private DigStageMaker stageMaker;
    private DigToolChoice toolChoice;
    [SerializeField] 
    private Button[] digToolButtons;
    

    protected void Start()
    {
        toolChoice = DigToolChoice.Hammer;
        ChangeTool((int)toolChoice);
        stageMaker = GameObject.Find("BasePanel").GetComponent<DigStageMaker>();
    }

    void Update()
    {

    }

    /// <summary>
    /// ツールの選択を変える
    /// </summary>
    /// <param name="choice">選択</param>
    public void ChangeTool(int choice)
    {
        toolChoice = (DigToolChoice)choice;
        for(int i = 0; i < digToolButtons.Length; i++)
        {
            digToolButtons[i].interactable = (i == choice) ? false : true;
        }
    }

    /// <summary>
    /// 威力を返すメソッド
    /// </summary>
    /// <returns></returns>
    public virtual int GetPower()
    {
        stageMaker.DamageStage(power[(int)toolChoice]);
        return power[(int)toolChoice];
    }
}

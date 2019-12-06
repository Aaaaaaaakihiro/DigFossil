using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DigToolChoice
{
    Hammer = 0,
    Pikkeru
}

public class DigToolBase : MonoBehaviour
{
    //道具のパワー
    //[SerializeField] protected int power;
    [SerializeField] private int[] power;
    //レイヤーマスク
    [SerializeField] protected LayerMask targetLayer;
    [HideInInspector] public DigToolChoice toolChoice;
    private DigStageMaker stageMaker;
    [SerializeField] private Button[] toolButtons;
    

    protected void Start()
    {
        toolChoice = DigToolChoice.Hammer;
        ChangeTool((int)toolChoice);
        stageMaker = GameObject.Find("BasePanel").GetComponent<DigStageMaker>();
    }

    void Update()
    {

    }

    public void ChangeTool(int choice)
    {
        toolChoice = (DigToolChoice)choice;
        toolButtons[choice].enabled = false;
        toolButtons[1 - choice].enabled = true;
    }

    //掘る威力を返すメソッド
    public virtual int GetPower()
    {
        stageMaker.DamageStage(power[(int)toolChoice]);
        return power[(int)toolChoice];
    }
}

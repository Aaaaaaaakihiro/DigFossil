using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    [SerializeField]
    private Flowchart _flowchart;

    private GameObject _player;

    private bool _isTalking = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public void StartConversation()
    {
        _flowchart.ExecuteBlock("DefaultConversation");
    }

    IEnumerator Talk()
    {
        //会話中にコルーチンに入らないように防止
        if(_isTalking)
        {
            yield break;
        }

        //会話中フラグをオン
        _isTalking = true;
        //会話中にNPCとプレイヤの動きを停止
        this.gameObject.GetComponent<NPCCotroller>().setNpcUnavairable();
        _player.GetComponent<TapController>().setControlUnavairable();

        //会話ブロックがすべて消化されるまで処理を継続
        while (_flowchart.GetExecutingBlocks().Count == 0)
        {
            yield return null;
        }

        //会話ブロックがすべて消化されれば、プレイヤ・ＮＰＣの動きを再開させ会話中フラッグをオフにする
        this.gameObject.GetComponent<NPCCotroller>().setNpcAvairable();
        _player.GetComponent<TapController>().setControlAvairable();
        _isTalking = false;
    }
}

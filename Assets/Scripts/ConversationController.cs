using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    [SerializeField]
    private Flowchart _flowchart;

    public void StartConversation()
    {
        _flowchart.ExecuteBlock("DefaultConversation");
    }
}

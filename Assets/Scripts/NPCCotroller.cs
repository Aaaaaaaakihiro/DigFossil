using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCotroller : MonoBehaviour
{
    private GameObject lookAtTarget;

    private bool _isNpcAvairable = true;
    // Start is called before the first frame update
    void Start()
    {
        lookAtTarget = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    public void OnUserAction()
    {
        this.gameObject.GetComponent<ConversationController>().StartConversation();
    }

    public void setNpcAvairable()
    {
        _isNpcAvairable = true;
    }

    public void setNpcUnavairable()
    {
        _isNpcAvairable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isNpcAvairable)
        {
            if (lookAtTarget)
            {
                Quaternion lookRotation = Quaternion.LookRotation(lookAtTarget.transform.position - transform.position, Vector3.up);

                lookRotation.z = 0;
                lookRotation.x = 0;

                this.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
            }
        }
    }
}

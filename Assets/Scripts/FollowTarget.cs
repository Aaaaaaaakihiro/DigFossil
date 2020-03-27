using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private float offset = 20;

    private GameObject player;
    private Vector3 fixedPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        fixedPosition = player.transform.position;
        fixedPosition.y += offset;
        this.gameObject.transform.position = fixedPosition;
    }
}

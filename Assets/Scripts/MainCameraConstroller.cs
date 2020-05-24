using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraConstroller : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = this.gameObject.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraConstroller : MonoBehaviour
{
    private GameObject player;
    private Vector3 camPos;

    [SerializeField]
    private float offset = 30;
    [SerializeField]
    private float rotateAngle = 45;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camPos = Vector3.zero;
        camPos.z = -offset;
        camPos.y = offset;

        this.transform.Rotate(new Vector3(1, 0, 0),rotateAngle);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + camPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;

    private static bool playerExists;

    private Dictionary<string, Vector3> oldPositionData;
    private void Awake()
    {
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(gameObject);
            oldPositionData = new Dictionary<string, Vector3>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey("down"))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }
}

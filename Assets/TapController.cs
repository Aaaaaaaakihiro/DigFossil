using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    //移動スピード
    [SerializeField] 
    private float moveSpeed = 20;
    //目的ポイントの半径。半径内に到達すると移動を終了させる。
    [SerializeField]
    private float destinationPointRadius = 2.0f;
    //初期位置の地面からのオフセット値
    [SerializeField]
    private float offset = 10;

    //タップするたびに更新される目的地
    private static Vector3 destinationPoint = Vector3.zero;
    private bool isReachedDestination = false;

    // Start is called before the first frame update
    void Start()
    {
        destinationPoint.y = offset;
    }

    // Update is called once per frame
    void Update()
    {
        //目的地まで移動する
        headingToDestination();
        //タッチorクリックでポイント更新
        setDestinationPoint();
    }

    /// <summary>
    /// 移動ポイントをタッチorクリックで更新する
    /// </summary>
    private void setDestinationPoint()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButton(0))
            {
                isReachedDestination = false;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 30.0f))
                {
                    destinationPoint = hit.point;
                    //destinationPoint.y = this.gameObject.transform.position.y;
                    destinationPoint.y = offset;
                    Debug.Log($"Editor/移動先の座標:{destinationPoint}");
                }
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                isReachedDestination = false;

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 30.0f))
                {
                    destinationPoint = hit.point;
                    //destinationPoint.y = this.gameObject.transform.position.y;
                    destinationPoint.y = offset;
                    Debug.Log($"Mobile/移動先の座標:{destinationPoint}");
                }

            }
        }
    }

    /// <summary>
    /// touchPointまでこのスクリプトがアタッチされたオブジェクトを移動させる
    /// </summary>
    private void headingToDestination()
    {
        //destinationPointに着いていない間はずっとdestinationPointまで移動する
        if (!isReachedDestination)
        {
            float step = moveSpeed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.transform.position, destinationPoint, step);

            //目的地との距離がdestinationPointRadius以内にになると移動が終了する。
            if (Vector3.Distance(this.gameObject.transform.position, destinationPoint) < destinationPointRadius)
            {
                isReachedDestination = true;
            }
        }
    }
}

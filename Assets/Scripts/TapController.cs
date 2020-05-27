using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    //移動スピード
    [SerializeField] 
    private float _moveSpeed = 20;
    //目的ポイントの半径。半径内に到達すると移動を終了させる。
    [SerializeField]
    private float _destinationPointRadius = 2.0f;
    //初期位置の地面からのオフセット値
    [SerializeField]
    private float _offset = 10;

    //タップするたびに更新される目的地
    private static Vector3 destinationPoint = Vector3.zero;
    private bool _isReachedDestination = false;

    //インタラクト可能な物体を検知したときにTrueになるBool
    private bool _isEnteringOtherCollision = false;

    //アニメーション用スクリプト
    private PlayerAniCon _aniCon;

    // Start is called before the first frame update
    void Start()
    {
        destinationPoint = this.transform.position;
        //destinationPoint.y = offset;

        _aniCon = this.gameObject.GetComponent<PlayerAniCon>();
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
        //エディター上での処理
        if (Application.isEditor)
        {
            if (Input.GetMouseButton(0))
            {
                _isReachedDestination = false;

                

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //複数Rayに対してタグに応じた処理を行う
                RaycastHit[] hits;
                hits = Physics.RaycastAll(ray, 50.0f);

                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    if (hit.collider.tag == "Terrain")
                    {
                        destinationPoint = hit.point;
                        //destinationPoint.y = this.gameObject.transform.position.y;
                        destinationPoint.y = _offset;
                        Debug.Log($"Editor/移動先の座標:{destinationPoint}");
                    }
                    else if (hit.collider.tag == "NPC")
                    {
                        if (_isEnteringOtherCollision)
                            hit.collider.gameObject.GetComponent<NPCCotroller>().OnUserAction();
                    }
                    else if (hit.collider.tag == "InteractiveObject")
                    {
                        if (_isEnteringOtherCollision)
                            hit.collider.gameObject.GetComponent<InteractiveObjectController>().OnUserAction();
                    }
                }

                //RaycastHit hit;
                //if (Physics.Raycast(ray, out hit, 30.0f))
                //{
                //    if (hit.collider.tag == "Terrain")
                //    {
                //        destinationPoint = hit.point;
                //        //destinationPoint.y = this.gameObject.transform.position.y;
                //        destinationPoint.y = _offset;
                //        Debug.Log($"Editor/移動先の座標:{destinationPoint}");
                //    }
                //    else if(hit.collider.tag == "NPC")
                //    {
                //        if(_isEnteringOtherCollision)
                //        hit.collider.gameObject.GetComponent<NPCCotroller>().OnUserAction();
                //    }
                //    else if (hit.collider.tag == "InteractiveObject")
                //    {
                //        if(_isEnteringOtherCollision)
                //        hit.collider.gameObject.GetComponent<InteractiveObjectController>().OnUserAction();
                //    }
                //}
            }
        }
        else
        {
            //モバイル上での処理(タップ処理)
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                _isReachedDestination = false;

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 30.0f))
                {
                    if(hit.collider.tag == "Terrain")
                    {
                        destinationPoint = hit.point;
                        //destinationPoint.y = this.gameObject.transform.position.y;
                        destinationPoint.y = _offset;
                        Debug.Log($"Mobile/移動先の座標:{destinationPoint}");
                    }
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
        if (!_isReachedDestination)
        {
            //向かう方向へ視線を向ける
            this.gameObject.transform.LookAt(destinationPoint);

            //歩くモーションをオンにする
            _aniCon.setWalkBoolTrue();

            float step = _moveSpeed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.transform.position, destinationPoint, step);

            //目的地との距離がdestinationPointRadius以内にになると移動が終了する。
            if (Vector3.Distance(this.gameObject.transform.position, destinationPoint) < _destinationPointRadius)
            {
                _isReachedDestination = true;
            }
        }
        else
        {
            //待機モーションをオン
            _aniCon.setWalkBoolFalse();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            _isEnteringOtherCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            _isEnteringOtherCollision = false;
        }
    }

}

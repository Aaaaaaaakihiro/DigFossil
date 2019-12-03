using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickSaveManager : MonoBehaviour
{
    public static QuickSaveManager instance = null;

    private static  string[] inTownOldPos = new string[3] {"0","0","0" };
    private static  string[] inExploreOldPos = new string[3] { "0","0","0"};

    //playerprefs
    //text
    //

    // Start is called before the first frame update
    private void Awake()
    {
        //シングルトン処理
        //インスタンス作成時、既にインスタンスが作成されている場合はインスタンスを破壊する。
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quickLoad()
    {
        if (SceneManager.GetActiveScene().name == "Town")
        {
            quickLoadInTown();
        }else if (SceneManager.GetActiveScene().name == "Explore")
        {
            quickLoadInExplore();
        }
    }

    public void quickSaveInTown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        inTownOldPos[0] = player.transform.position.x.ToString();
        inTownOldPos[1] = player.transform.position.y.ToString();
        inTownOldPos[2] = player.transform.position.z.ToString();
    }

    public void quickLoadInTown()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Playerの位置:"+player.transform.position.x+","+player.transform.position.y+","+player.transform.position.z);
        Debug.Log("取得した座標"+inTownOldPos[0]+","+inTownOldPos[1]+","+inTownOldPos[2]);
        Vector3 newVec = new Vector3(
             float.Parse(inTownOldPos[0]),
             float.Parse(inTownOldPos[1]),
             float.Parse(inTownOldPos[2]));
        Debug.Log("生成した座標"+newVec.x + "," + newVec.y + "," + newVec.z);
        player.transform.position = newVec;
    }

    public void quickSaveInExplore()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        inExploreOldPos[0] = player.transform.position.x.ToString();
        inExploreOldPos[1] = player.transform.position.y.ToString();
        inExploreOldPos[2] = player.transform.position.z.ToString();
    }

    public void quickLoadInExplore()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Playerの位置:" + player.transform.position.x + "," + player.transform.position.y + "," + player.transform.position.z);
        Debug.Log("取得した座標"+inExploreOldPos[0] + "," + inExploreOldPos[1] + "," + inExploreOldPos[2]);
        Vector3 newVec = new Vector3(
             float.Parse(inExploreOldPos[0]),
             float.Parse(inExploreOldPos[1]),
             float.Parse(inExploreOldPos[2]));
        Debug.Log("生成した座標" + newVec.x + "," + newVec.y + "," + newVec.z);
        player.transform.position = newVec;
    }
}

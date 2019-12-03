using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    //staticなインスタンスを作成していつでもアクセスできるように
    public static GameLoopManager instance = null;
    
    /// <summary>
    /// ゲームの状態一覧
    /// </summary>
    public enum GameState
    {
        TITLE,
        TOWN,
        EXPLORE,
        DIG
    }

    public GameState currentState = GameState.TITLE;

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
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.sceneUnloaded += SceneUnloaded;
        SceneManager.activeSceneChanged += ActiveSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded :"+nextScene.name);
        //Debug.Log("Scene Loaded mode :"+mode);

        //シーンが遷移する前に各状況に応じてクイックセーブをする
        //if (SceneManager.GetActiveScene().name == "Explore" && nextScene.name == "Town")
        //{
        //    QuickSaveManager.instance.quickSaveInExplore();
        //}
        //else if (SceneManager.GetActiveScene().name == "Town" && nextScene.name == "Explore")
        //{
        //    QuickSaveManager.instance.quickSaveInTown();
        //}
        //else if(SceneManager.GetActiveScene().name == "Explore" && nextScene.name == "Dig")
        //{
        //    QuickSaveManager.instance.quickSaveInExplore();
        //}
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<MoveController>().oldPositionData.ContainsKey(nextScene.name))
        {
            player.transform.position = player.GetComponent<MoveController>().oldPositionData[nextScene.name];
        }
        else
        {
            player.transform.position = new Vector3(0,1.1f,0);
        }

    }
    void SceneUnloaded(Scene thisScene)
    {
        Debug.Log("Scene Unloaded :"+thisScene.name);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MoveController>().oldPositionData[SceneManager.GetActiveScene().name] = player.transform.position;
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log("Scene Changed :"+thisScene.name);
        //Debug.Log("Scene Changed mode :"+nextScene.name);
        //シーンが遷移した後はクイックロードをする

        
    }

    //タイトルのシーンに遷移させる関数
    void switchToTitleScene()
    {
        currentState = GameState.TITLE;
        SceneManager.LoadScene("Title");
    }

    //街のシーンに遷移させる関数
    void switchToTownScene()
    {
        currentState = GameState.TOWN;
        //探検→街と遷移するときは、探検シーンの洞窟の入口でクイックセーブ
        //if (SceneManager.GetActiveScene().name == "Explore")
        //{
        //    QuickSaveManager.instance.quickSaveInExplore();
        //}
        SceneManager.LoadScene("Town");
        //違うシーンから遷移した時はクイックセーブされたポジションを参照する
        //QuickSaveManager.instance.quickLoadInTown();
    }

    //探検のシーンに遷移させる関数
    void switchToExploreScene()
    {
        currentState = GameState.EXPLORE;
        //街→探検と遷移するときは、街シーンの洞窟の入口でクイックセーブ
        //if (SceneManager.GetActiveScene().name == "Town")
        //{
        //    QuickSaveManager.instance.quickSaveInTown();
        //}
        SceneManager.LoadScene("Explore");
        //違うシーンから遷移した時はクイックセーブされたポジションを参照する
        //QuickSaveManager.instance.quickLoadInExplore();
    }

    //掘りのシーンに遷移させる関数
    void switchToDigScene()
    {
        currentState = GameState.DIG;
        //掘りシーンに移行する時に探検シーンでの位置を保存する
        //QuickSaveManager.instance.quickSaveInExplore();
        SceneManager.LoadScene("Dig");
        
    }

    public void dispatch(GameState state)
    {
        GameState oldState = currentState;
        currentState = state;

        switch (state)
        {
            case GameState.TITLE:
                switchToTitleScene();
                break;

            case GameState.TOWN:
                switchToTownScene();
                break;

            case GameState.EXPLORE:
                switchToExploreScene();
                break;

            case GameState.DIG:
                switchToDigScene();
                break;

        }
    }
}

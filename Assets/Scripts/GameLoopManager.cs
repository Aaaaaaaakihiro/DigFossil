using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    //staticなインスタンスを作成していつでもアクセスできるように
    public static GameLoopManager instance = null;

    //非同期ローディング用
    private AsyncOperation _async;
    
    /// <summary>
    /// ゲームの状態一覧
    /// </summary>
    //public enum GameState
    //{
    //    TITLE,
    //    TOWN,
    //    EXPLORE,
    //    DIG
    //}

    public SceneData.GameState currentState = SceneData.GameState.TITLE;

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
        currentState = SceneData.GameState.TITLE;
        StartCoroutine(Start("Title"));
        //SceneManager.LoadScene("Title");
    }

    //街のシーンに遷移させる関数
    void switchToTownScene()
    {
        currentState = SceneData.GameState.TOWN;
        //探検→街と遷移するときは、探検シーンの洞窟の入口でクイックセーブ
        //if (SceneManager.GetActiveScene().name == "Explore")
        //{
        //    QuickSaveManager.instance.quickSaveInExplore();
        //}
        StartCoroutine(Start("Town_Hirono"));
        //SceneManager.LoadScene("Town");
        //違うシーンから遷移した時はクイックセーブされたポジションを参照する
        //QuickSaveManager.instance.quickLoadInTown();
    }

    //探検のシーンに遷移させる関数
    void switchToExploreScene()
    {
        currentState = SceneData.GameState.EXPLORE;
        //街→探検と遷移するときは、街シーンの洞窟の入口でクイックセーブ
        //if (SceneManager.GetActiveScene().name == "Town")
        //{
        //    QuickSaveManager.instance.quickSaveInTown();
        //}
        StartCoroutine(Start("Explore_Hirono"));
        //SceneManager.LoadScene("Explore_Hirono");
        //違うシーンから遷移した時はクイックセーブされたポジションを参照する
        //QuickSaveManager.instance.quickLoadInExplore();
    }

    //掘りのシーンに遷移させる関数
    void switchToDigScene()
    {
        currentState = SceneData.GameState.DIG;
        //掘りシーンに移行する時に探検シーンでの位置を保存する
        //QuickSaveManager.instance.quickSaveInExplore();
        StartCoroutine(Start("Dig_Sakai"));
       // SceneManager.LoadScene("Dig_Sakai");
        
    }

    void switchToLabScene()
    {
        currentState = SceneData.GameState.LAB;
        StartCoroutine(Start("Lab_Hirono"));
    }

    IEnumerator Start(string sceneName)
    {
        yield return new WaitForSeconds(1);

        //非同期でロード開始
        _async = SceneManager.LoadSceneAsync(sceneName);

        //シーン移動を許可するかどうか
        //_async.allowSceneActivation = false;

        while (!_async.isDone)
        {
            Debug.Log(_async.progress / 0.9f);
            yield return null;
        }
        Debug.Log("SceneName = " + sceneName + " ロード完了");
        //_async.allowSceneActivation = true;

        yield return _async;
    }

    public void dispatch(SceneData.GameState state)
    {
        SceneData.GameState oldState = currentState;
        currentState = state;

        switch (state)
        {
            case SceneData.GameState.TITLE:
                switchToTitleScene();
                break;

            case SceneData.GameState.TOWN:
                switchToTownScene();
                break;

            case SceneData.GameState.EXPLORE:
                switchToExploreScene();
                break;

            case SceneData.GameState.DIG:
                switchToDigScene();
                break;

            case SceneData.GameState.LAB:
                switchToLabScene();
                break;
        }
    }
}

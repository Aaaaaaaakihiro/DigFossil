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
    //現在のゲームシーン
    public SceneData.GameState currentState = SceneData.GameState.TITLE;

    public Dictionary<string, Vector3> _oldPositionData;
    private void Awake()
    {
        _oldPositionData = new Dictionary<string, Vector3>();
    }

    private void Start()
    {
        //SceneLoaded,Unloaded,SceneChangedの３つのイベントをそれぞれ専用のイベントに追加
        SceneManager.sceneUnloaded += SceneUnloaded;
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void SceneUnloaded(Scene thisScene)
    {
        Debug.Log("Scene Unloaded :" + thisScene.name);

        //イベの削除
        SceneManager.sceneUnloaded -= SceneUnloaded;
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log("Scene Changed :" + thisScene.name);
        //イベの削除
        SceneManager.activeSceneChanged -= ActiveSceneChanged;
    }
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded :"+nextScene.name);

        //シーンが切り替えられた時の処理
        //シーン切り替え後のGameLoopManagerスクリプトを取得
        var _gameLoopManager = GameObject.FindWithTag("GameLoopManager").GetComponent<GameLoopManager>();
        //前のシーンのGameLoopManagerから今のシーンのGameLoopManagerにoldPositionDataのディクショナリーを引き継ぐ
        _gameLoopManager._oldPositionData = _oldPositionData;
        //プレイヤーを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        

        //前のシーンでのポジションデーターがある場合はそのポジションにPlayerを持っていく
        if (_gameLoopManager._oldPositionData.ContainsKey(nextScene.name))
        {
            player.transform.position = _gameLoopManager._oldPositionData[nextScene.name];
        }
        else
        {
            //player.transform.position = new Vector3(0, 1.1f, 0);
        }

        //イベントの削除
        SceneManager.sceneLoaded -= SceneLoaded;
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

        //プレイヤーを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //現在のシーンの座標を記録
        if (_oldPositionData.ContainsKey(SceneManager.GetActiveScene().name))
        {
            _oldPositionData[SceneManager.GetActiveScene().name] = player.transform.position;
        }
        else
        {
            _oldPositionData.Add(SceneManager.GetActiveScene().name, player.transform.position);
        }

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

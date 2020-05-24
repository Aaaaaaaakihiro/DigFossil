using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MapNameHolderAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Text mapNameText;
    private string currentSceneName;
    [SerializeField]
    private int indicateTime = 2000;
    [SerializeField]
    private int interbalTime = 10;

    enum scenes : byte
    {
        Dig_Hirono,
        Explore_Hirono,
        Main_Hirono,
        Title_Hirono,
        Town_Hirono
    }

    void Start()
    {
        //初期化
        animator = this.GetComponent<Animator>();
        mapNameText = this.transform.GetChild(0).gameObject.GetComponent<Text>();
        currentSceneName = SceneManager.GetActiveScene().name;

        //マップネームUIを非表示
        animator.SetBool("isFadeIn", false);

        Debug.Log("シーンネーム:" + currentSceneName);

        //マップネームを設定、表示
        setMapName(currentSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    if (animator.GetBool("isFadeIn") == true)
        //    {
        //        animator.SetBool("isFadeIn", false);
        //    }
        //    else
        //    {
        //        animator.SetBool("isFadeIn", true);
        //    }
            
        //}
    }

    private async void activateMapNameUI()
    {
        await Task.Delay(interbalTime);

        //UIをindicateTime分表示した後に非表示にする
        Debug.Log("マップネームを表示");

        animator.SetBool("isFadeIn", true);
        deactivateMapNameUI();
    }

     private async void deactivateMapNameUI()
    {
        Debug.Log("マップネームを非表示");

        await Task.Delay(indicateTime);
        animator.SetBool("isFadeIn", false);
    }

    private void setMapName(string currentSceneName)
    {

        switch (currentSceneName)
        {
            case "Explore_Hirono":
                mapNameText.text = "ホリホリ洞窟";
                activateMapNameUI();
                break;
            case "Town_Hirono":
                mapNameText.text = "ホリホリタウン";
                activateMapNameUI();
                break;
            default:
                mapNameText.text = "未定義";
                break;
        }
    }

    private IEnumerator Delay(int delayFrameCount)
    {
        for (int i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        //yield return new WaitForSeconds(delayFrameCount);
    }

}

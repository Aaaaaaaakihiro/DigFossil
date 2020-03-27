using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexScript : MonoBehaviour
{
    public RectTransform contentRectTranceform;
    public Button button;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateGridLayoutCell();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Create cells
    public void InstantiateGridLayoutCell()
    {
        for(int i = 0;i < 30; i++)
        {
            var obj = Instantiate(button, contentRectTranceform);
            obj.GetComponent<Image>().sprite = sprite;
        }
    }
}

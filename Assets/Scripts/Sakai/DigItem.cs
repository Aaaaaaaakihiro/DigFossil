using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigItem : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] private List<Vector2> itemIndexes;
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CheckIsFound(int x, int y)
    {
        //Debug.Log(itemIndexes);
        bool isBroken = false;
        for(int i = 0; i < itemIndexes.Count; i++)
        {
            if(itemIndexes[i].x == x && itemIndexes[i].y == y)
            {
                itemIndexes.RemoveAt(i);
                isBroken = true;
                break;
            }
        }
        if(isBroken && itemIndexes.Count == 0)
        {
            Debug.Log("アイテムを発見");
        }
        
    }

    public void AddItemIndex(int x, int y)
    {
        if(itemIndexes == null)
        {
            Debug.Log("座標情報なし");
            itemIndexes = new List<Vector2>();

        }
        Debug.Log("座標を追加");
        itemIndexes.Add(new Vector2(x, y));
        Debug.Log(itemIndexes.Count);
    }
}

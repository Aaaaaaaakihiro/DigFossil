using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigEventManager : MonoBehaviour
{
    private List<DigEvent> digEvents;
    [SerializeField] private int eventPointCount;
    void Start()
    {
        digEvents = new List<DigEvent>(GetDigEventPoints("DigEvent"));
        SetRandomEventPoint(eventPointCount);
    }

    void Update()
    {

    }

    private void SetRandomEventPoint(int count)
    {
        for(int i = 0; i < count; i++)
        {
            int activeIndex = Random.Range(0, digEvents.Count);
            digEvents[activeIndex].gameObject.SetActive(true);
        }
    }


    private DigEvent[] GetDigEventPoints(string tag)
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
        DigEvent[] events = new DigEvent[points.Length];
        for(int i = 0; i < points.Length; i++)
        {
            events[i] = points[i].GetComponent<DigEvent>();
            events[i].gameObject.SetActive(false);
        }
        //Debug.Log("Even Point Count : " + events.Length);
        return events;
    }
}

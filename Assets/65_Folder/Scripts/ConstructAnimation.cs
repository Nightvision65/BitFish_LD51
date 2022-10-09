using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructAnimation : MonoBehaviour
{
    public RectTransform panel, bound;
    public GameObject sequence;
    public SpriteRenderer grid;
    public static ConstructAnimation instance;
    public int direction = 0;
    private float panelMax, boundMax;
    private float panelMin = 50, boundMin = 145;
    private float StandTime, StandTimer = 0;
    private Vector2 panelDx, boundDx, panelOrigin, boundOrigin;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        panelMax = panel.sizeDelta.x;
        boundMax = bound.sizeDelta.x;
        panelOrigin = panel.sizeDelta;
        boundOrigin = bound.sizeDelta;
    }
    private void Update()
    {
        if (direction > 0)
        {
            if(direction == 1)
            {
                panel.sizeDelta -= panelDx * Time.deltaTime;
                bound.sizeDelta -= boundDx * Time.deltaTime;
                if (panel.sizeDelta.x <= panelMin)
                {
                    direction = 0;
                }
            }
            else
            {
                panel.sizeDelta += panelDx * Time.deltaTime;
                bound.sizeDelta += boundDx * Time.deltaTime;
                if (panel.sizeDelta.x >= panelMax)
                {
                    panel.sizeDelta = panelOrigin;
                    bound.sizeDelta = boundOrigin;
                    grid.enabled = true;
                    sequence.SetActive(true);
                    direction = 0;
                }

            }
        }
    }
    public void Close(float stand)
    {
        StandTime = stand;
        grid.enabled = false;
        sequence.SetActive(false);
        direction = 1;
        panelDx = new Vector2((panelMax - panelMin) / StandTime, 0);
        boundDx = new Vector2((boundMax - boundMin) / StandTime, 0);
    }

    public void Open(float stand)
    {
        StandTime = stand;
        grid.enabled = false;
        direction = 2;
        panelDx = new Vector2((panelMax - panelMin) / StandTime, 0);
        boundDx = new Vector2((boundMax - boundMin) / StandTime, 0);
    }

}

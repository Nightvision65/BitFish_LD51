using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructAnimation : MonoBehaviour
{
    public RectTransform panel, bound;
    public SpriteRenderer grid;
    public static ConstructAnimation instance;
    private float panelMax, boundMax;
    private float panelMin = 50, boundMin = 145;
    private float StandTime, StandTimer = 0;
    private Vector2 panelDx, boundDx, panelOrigin, boundOrigin;
    private int direction = 0;
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
        if (StandTimer > 0)
        {
            StandTimer -= Time.deltaTime;
            if (StandTimer == 0) StandTimer = -1;
        }
        if (StandTimer < 0)
        {
            StandTimer = 0;
            direction = 2;
        }
        if (direction > 0)
        {
            if(direction == 1)
            {
                panel.sizeDelta -= panelDx * Time.deltaTime;
                bound.sizeDelta -= boundDx * Time.deltaTime;
                if (panel.sizeDelta.x <= panelMin)
                {
                    StandTimer = StandTime / 3;
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
                    direction = 0;
                }

            }
        }
    }
    public void Play(float stand)
    {
        StandTime = stand - 0.3f;
        grid.enabled = false;
        direction = 1;
        panelDx = new Vector2((panelMax - panelMin) / StandTime * 3, 0);
        boundDx = new Vector2((boundMax - boundMin) / StandTime * 3, 0);
    }

}

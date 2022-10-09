using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitNextShow : MonoBehaviour
{
    public List<int> unitIndex;
    public static UnitNextShow instance;
    public List<RectTransform> unitTransform;
    public List<Image> unitImage;
    public List<Sprite> unitSprite;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            //Debug.Log(transform.GetChild(i));
            unitTransform[i] = transform.GetChild(i).GetComponent<RectTransform>();
            unitImage[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    
    public void UnitUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            unitTransform[i].rotation = Quaternion.Euler(0, 0, 0);
            unitImage[i].sprite = unitSprite[unitIndex[i]];
        }
    }
}

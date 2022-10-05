using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Text mText;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        mText.text = Mathf.FloorToInt(LevelManager.instance.ConstructTimer).ToString();
    }
}

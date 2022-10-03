using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM切换 : MonoBehaviour
{
    public AudioSource 音乐1, 音乐2;
    public float 音量调节系数 = 0.2f;
    float 音量1, 音量2, time;
    bool key;
    // Start is called before the first frame update
    void Start()
    {
        音量1 = 1;
        音量2 = 0;
        key = false;
        time = 0;
        音乐1.volume = 音量1 * 音量调节系数;
        音乐2.volume = 音量2 * 音量调节系数;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        var 转换耗时 = 1f;
        if (time > 9f)
        {
            if (key == false)
            {
                音量1 -= Time.deltaTime / 转换耗时;
                音量2 += Time.deltaTime / 转换耗时;
                if (音量1 <= 0)
                {
                    音量1 = 0;
                    音量2 = 1;
                    time -= 10;
                    key = !key;
                }
            }
            else
            {
                音量1 += Time.deltaTime / 转换耗时;
                音量2 -= Time.deltaTime / 转换耗时;
                if (音量2 <= 0)
                {
                    音量1 = 1;
                    音量2 = 0;
                    time -= 10;
                    key = !key;
                }
            }

            音乐1.volume = 音量1 * 音量调节系数;
            音乐2.volume = 音量2 * 音量调节系数;

        }
    }
}

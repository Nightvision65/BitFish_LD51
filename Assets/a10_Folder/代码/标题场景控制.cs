using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 标题场景控制 : MonoBehaviour
{
    public Image 背景图片;

    bool key;
    float alpha;
    // Start is called before the first frame update
    void Start()
    {
        key = false;
        alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (key)
        {
            alpha += Time.deltaTime * 5f;
            var c = 背景图片.color;
            c.a = alpha;
            背景图片.color = c;

            if (alpha>=1) SceneManager.LoadScene("65's Scene 1");
        }



    }


    public void 按钮()
    {
        key = true;
    }
}

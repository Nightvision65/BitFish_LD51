using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 特效引用 : MonoBehaviour
{
    public GameObject 火花特效,尘土特效,冒烟特效, 摧毁爆破烟雾特效,喷烟特效;
    public static 特效引用 instance;

    private void Awake()
    {
        instance = this;
    }
    float time = 0;
    private void Update()
    {
        /*
        time += Time.deltaTime;
        //特效测试案例
        if (time > 1f)
        {
            time -= 1f;
            生成火花特效(transform.position, 180, 10);
            生成摧毁爆破烟雾(transform.position + Vector3.right * 3, 0.1f, 0.1f, 20);

        }

        生成尘土(transform.position + Vector3.right, 0.3f, 0.1f, 1);
        生成冒烟(transform.position + Vector3.right*2, 0.2f, 0.2f, 1);
        */
    }

    //角度：0-正右方 90-正上方 -90-正下方
    public void 生成火花特效(Vector2 位置, float 角度, int 数量)
    {
        float 散射角 = 30;

        for (int i = 0; i < 数量; i++)
        {
            var angle = Random.Range(-散射角, 散射角) + 角度;
            Tool_ParticleTemplate.Emit(火花特效, 位置, Random.Range(2f, 8f) * ToVector2(angle), 0.5f, angle);

        }

    }
    public void 生成尘土(Vector2 位置, float X随机偏移范围, float y随机偏移范围, int 数量)
    {

        for (int i = 0; i < 数量; i++)
        {
            位置.x += Random.Range(-X随机偏移范围, X随机偏移范围);
            位置.y += Random.Range(-y随机偏移范围, y随机偏移范围);
            var angle = Random.Range(0, 360);
            Tool_ParticleTemplate.Emit(尘土特效, 位置, Vector3.zero, Random.Range(0.6f, 1.2f), angle);

        }
    }
    public void 生成冒烟(Vector2 位置, float X随机偏移范围, float y随机偏移范围, int 数量)
    {

        for (int i = 0; i < 数量; i++)
        {
            位置.x += Random.Range(-X随机偏移范围, X随机偏移范围);
            位置.y += Random.Range(-y随机偏移范围, y随机偏移范围);
            var angle = Random.Range(0, 360);
            Tool_ParticleTemplate.Emit(冒烟特效, 位置, Vector3.zero, Random.Range(0.7f, 1.4f), angle);

        }
    }
    public void 生成喷烟(Vector2 位置, float 角度, int 数量)
    {
        float 散射角 = 0;

        for (int i = 0; i < 数量; i++)
        {
            var angle = Random.Range(-散射角, 散射角) + 角度;
            Tool_ParticleTemplate.Emit(喷烟特效, 位置, Random.Range(2f, 8f) * ToVector2(angle), 0.5f, angle);

        }

    }


    public void 生成摧毁爆破烟雾(Vector2 位置, float X随机偏移范围, float y随机偏移范围, int 数量)
    {

        for (int i = 0; i < 数量; i++)
        {
            位置.x += Random.Range(-X随机偏移范围, X随机偏移范围);
            位置.y += Random.Range(-y随机偏移范围, y随机偏移范围);
            var angle = Random.Range(0, 360);
            Tool_ParticleTemplate.Emit(摧毁爆破烟雾特效, 位置, Random.Range(1f, 5f) * ToVector2(angle), Random.Range(1f, 2f), angle);

        }

    }




    /// <summary>
    /// 将角度转换为二维向量
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Vector2 ToVector2(float angle)
    {
        Vector2 v;
        v.x = Mathf.Cos(angle / Mathf.Rad2Deg);
        v.y = Mathf.Sin(angle / Mathf.Rad2Deg);
        return v;
    }


}

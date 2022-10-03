using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//粒子模板
public class Tool_ParticleTemplate : MonoBehaviour
{
    //获得一个粒子发射器
    public static Tool_ParticleTemplate Warm(GameObject prefab)
    {
        if (PrefabToParticleTempDir == null)
        {
            PrefabToParticleTempDir = new Dictionary<GameObject, Tool_ParticleTemplate>();
        }
        if (ParticleTemplateRoot == null)
        {
            ParticleTemplateRoot = new GameObject("ParticleTemplateRoot");
        }
        if (PrefabToParticleTempDir.TryGetValue(prefab, out var parTemp))
        {
            if (parTemp == null)
            {
                Debug.LogError("null particle template.");
                PrefabToParticleTempDir.Remove(prefab);
            }
        }
        if (parTemp == null)
        {
            var obj = GameObject.Instantiate<GameObject>(prefab, ParticleTemplateRoot.transform);
            if (!obj.TryGetComponent<Tool_ParticleTemplate>(out parTemp))
            {
                parTemp = obj.AddComponent<Tool_ParticleTemplate>();
            }

            PrefabToParticleTempDir.Add(prefab, parTemp);
        }

        return parTemp;
    }



    //发射粒子
    public static void Emit(GameObject prefab, Vector3 pos, Vector3 velocity, float scale = 1f, float rot = float.NaN)
    {
        var temp = Warm(prefab);
        if (temp != null)
        {
            temp.Emit(pos, velocity, rot, scale);
        }
    }
    public static void Emit(GameObject prefab, Vector3 pos, float scale = 1f, float rot = float.NaN)
    {
        Emit(prefab, pos, Vector3.positiveInfinity, scale, rot);
    }
    public static void Emit(GameObject prefab, float scale = 1f, float rot = float.NaN)
    {
        Emit(prefab, Vector3.positiveInfinity, scale, rot);
    }
    //发射粒子
    public static void EmitWithScale(GameObject prefab, Vector3 pos, float scale = 1f, float rot = 0, int num = -1)
    {
        var temp = Warm(prefab);
        if (temp != null)
        {
            temp.EmitWithScale(pos, num, rot, scale);
        }
    }


    public static void Clear()
    {
        PrefabToParticleTempDir.Clear();
    }
    private static GameObject ParticleTemplateRoot;
    private static Dictionary<GameObject, Tool_ParticleTemplate> PrefabToParticleTempDir = null;
    private ParticleSystem[] tempPars = null;
    private int[] emitCount;

    private void OnDestroy()
    {
        foreach (var kv in PrefabToParticleTempDir)
        {
            if (kv.Value == this)
            {
                PrefabToParticleTempDir.Remove(kv.Key);
                return;
            }
        }
    }

    //初始化
    void Init()
    {
        tempPars = transform.GetComponentsInChildren<ParticleSystem>();
        emitCount = new int[tempPars.Length];
        for (int i = 0; i < tempPars.Length; i++)
        {
            var par = tempPars[i];
            int count = 1;
            var emission = par.emission;

            //test code.
            var parMain = par.main;
            parMain.simulationSpace = ParticleSystemSimulationSpace.World;
            parMain.maxParticles = 1000;


            if (emission.burstCount >= 1)
            {
                count = (int)emission.GetBurst(0).count.Evaluate(0);
            }
            emission.enabled = false;
            emitCount[i] = count;
        }
    }

    //发射粒子
    void Emit(Vector3 pos, Vector3 velocity, float rot, float scale)
    {
        if (tempPars == null)
        {
            this.Init();
        }

        //velocity = Vector3.positiveInfinity;

        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        if (!Equals(pos, Vector3.positiveInfinity)) emitParams.position = pos;
        if (!Equals(velocity, Vector3.positiveInfinity)) emitParams.velocity = velocity;
        if (!float.IsNaN(rot)) emitParams.rotation = -rot;
        if (scale != 1f)
        {
            var startSize = tempPars[0].main.startSize;
            if (startSize.mode == ParticleSystemCurveMode.Constant) emitParams.startSize = startSize.constant;
            else emitParams.startSize = Random.Range(startSize.constantMin, startSize.constantMax);
            emitParams.startSize *= scale;
        }


        tempPars[0].Emit(emitParams, 1);

    }
    //发射粒子
    void EmitWithScale(Vector3 pos, int num, float rot, float scale)
    {
        if (tempPars == null)
        {
            this.Init();
        }
        transform.position = pos;
        transform.rotation = Quaternion.Euler(-rot, 90, -90);
        transform.localScale = Vector3.one * scale;
        for (int i = 0; i < tempPars.Length; i++)
        {
            if (num == -1)
            {
                tempPars[i].Emit(emitCount[i]);
            }
            else
            {
                tempPars[i].Emit(num);
            }
        }

        transform.localScale = Vector3.one;
    }
}
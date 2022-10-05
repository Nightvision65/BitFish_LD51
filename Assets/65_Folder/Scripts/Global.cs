using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    public List<GameObject> chariotUnit, chariotUnitEnemy;
    public int team;
    private int unitPage = 0;
    private AudioSource src;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        src = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
    //    debugFunc();
    }

    //∆’Õ®≤•∑≈“Ù–ß
    public void AudioPlay(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/" + name);
        src.PlayOneShot(clip);
    }

    void debugFunc()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LevelManager.instance.StartConstruct();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LevelManager.instance.EndConstruct();
            LevelManager.instance.StartConstruct();
        }
        if (Input.GetKeyDown(KeyCode.Q)) { unitPage = (unitPage + 1) % 2; }
        if (Input.GetKeyDown(KeyCode.E)) { team = (team + 1) % 2; }
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && i + unitPage * 9 < chariotUnit.Count)
            {
                Destroy(GameObject.FindGameObjectWithTag("Placing"));
                GameObject up = Instantiate(LevelManager.instance.placingObject, transform);
                up.GetComponent<UnitPlacing>().unitIndex = i + unitPage * 9;
            }
        }
        for (int i = 2; i < 12; i++)
        {
            if(Input.GetKeyDown(KeyCode.F1 + i))
            {
                LevelManager.instance.EnemySpawn(i - 2);
            }
        }
    }
}

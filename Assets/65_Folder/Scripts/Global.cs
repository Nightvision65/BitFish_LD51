using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    public List<GameObject> chariotUnit;
    private int unitPage = 0;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LevelManager.instance.StartConstruct();
    }
    // Update is called once per frame
    void Update()
    {
        debugFunc();
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
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && i + unitPage * 9 < chariotUnit.Count)
            {
                Destroy(GameObject.FindGameObjectWithTag("Placing"));
                GameObject up = Instantiate(LevelManager.instance.placingObject, transform);
                up.GetComponent<UnitPlacing>().unitIndex = i + unitPage * 9;
            }
        }
    }
}

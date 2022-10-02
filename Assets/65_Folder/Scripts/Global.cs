using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    public List<GameObject> chariotUnit;
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
        for (int i = 0; i < chariotUnit.Capacity; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Destroy(GameObject.FindGameObjectWithTag("Placing"));
                GameObject up = Instantiate(LevelManager.instance.placingObject, transform);
                up.GetComponent<UnitPlacing>().unitIndex = i;
            }
        }
    }
}

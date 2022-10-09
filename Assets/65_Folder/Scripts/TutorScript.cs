using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorScript : MonoBehaviour
{
    bool build = false, placed=false;
    GameObject place;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!place)
        {
            if (!placed)
            {
                place = GameObject.FindGameObjectWithTag("Placing");
                if (place) placed = true;
            }
            else
            {
                build = true;
            }
        }
        if (!build)
        {
            LevelManager.instance.ConstructTimer = LevelManager.instance.ConstructTime;
        }
        else
        {
            if (LevelManager.instance.ConstructTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

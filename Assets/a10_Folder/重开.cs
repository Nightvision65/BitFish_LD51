using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 重开 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Restart()
    {
        SceneManager.LoadScene("65's Scene 1");
    }

    public void Destroy()
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit")){
            var script = unit.GetComponentInParent<UnitPlaced>();
            if (script.team == 0 && script.maxHP < 1000 && script.isActived) script.TakeDamage(114514);
        }
    }
}

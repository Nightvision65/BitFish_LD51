using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global instance;
    public GameObject vehicleUnitPlacing;
    public List<GameObject> vehicleUnit;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        debugFunc();
    }

    void debugFunc()
    {
        if (Input.GetMouseButtonUp(0) && !GameObject.FindGameObjectWithTag("Placing"))
        {
            GameObject up = Instantiate(vehicleUnitPlacing, transform.position, Quaternion.Euler(0, 0, 0));
            up.GetComponent<UnitPlacing>().unitIndex = Random.Range(0, vehicleUnit.Capacity);
        }
    }
}

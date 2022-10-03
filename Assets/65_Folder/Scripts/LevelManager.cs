using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform chariotStartPos;    //战车起始位置
    public GameObject placingObject, chariotObject;
    public Rigidbody2D theCarrier;
    public bool isConstruct;
    private GameObject mChariot;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isConstruct)
        {
            if (!GameObject.FindGameObjectWithTag("Placing"))
            {
                GameObject up = Instantiate(placingObject, mChariot.transform);
                up.GetComponent<UnitPlacing>().unitIndex = Random.Range(0, Global.instance.chariotUnit.Capacity);
            }
        }
    }

    //开始造车
    public void StartConstruct()
    {
        isConstruct = true;
        mChariot = Instantiate(chariotObject, UnitGrid.instance.transform.position, Quaternion.Euler(0, 0, 0));
    }

    //结束造车
    public void EndConstruct()
    {
        isConstruct = false;
        Destroy(GameObject.FindGameObjectWithTag("Placing"));
        mChariot.transform.position = chariotStartPos.position;
        UnitGrid.instance.ChariotCombine();
        foreach (SpriteRenderer sprite in mChariot.GetComponentsInChildren<SpriteRenderer>())
        {
            if (sprite.gameObject.tag == "Outline") sprite.enabled = true;
        }
        foreach (Collider2D collider in mChariot.GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = true;
        }
        foreach (Joint2D joint in mChariot.GetComponentsInChildren<Joint2D> ())
        {
            joint.enabled = true;
        }
        foreach (Rigidbody2D rbody in mChariot.GetComponentsInChildren<Rigidbody2D>())
        {
            rbody.WakeUp();
            rbody.velocity = theCarrier.velocity;
        }
        foreach (ConstantForce2D force in mChariot.GetComponentsInChildren<ConstantForce2D>())
        {
            force.enabled = true;
        }
        mChariot.tag = "Untagged";
        UnitGrid.instance.ClearGrid();
    }
}

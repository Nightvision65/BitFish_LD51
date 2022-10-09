using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weight
{
    public List<int> unit;
    public float baseWeight, addWeight, nowWeight;
    public Weight(float baseW, float addW, List<int> u)
    {
        unit = u;
        baseWeight = baseW;
        addWeight = addW;
        nowWeight = baseWeight;
    }
    public int Pick()
    {
        nowWeight = baseWeight;
        return unit[Random.Range(0, unit.Count)];
    }
    public void Drop()
    {
        nowWeight += addWeight;
    }
    public void Reset()
    {
        nowWeight = baseWeight;
    }
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform Cat;
    public Transform chariotStartPos, chariotEndPos;    //战车起始位置
    public Collider2D chariotStartCollider, chariotEndCollider;
    public List<GameObject> Enemy;
    public GameObject placingObject, chariotObject;
    public Rigidbody2D theCarrier;
    public float ConstructTime, ConstructTimer, StandTime;
    public bool isConstruct;
    public float levelTimer;
    private Queue<int> unitQueue = new Queue<int>();
    private int enemyCount = 5;
    private GameObject mChariot;
    private bool keySkip, isDestroyed = false, isOpen = false;
    private List<Weight> listWeight = new List<Weight>();
    private int loop = 0;
    private bool isSkip = true;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        WeightInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;
        if (isConstruct)
        {
            keySkip = Input.GetButtonDown("Skip");
            if (keySkip)
            {
                Destroy(GameObject.FindGameObjectWithTag("Placing"));
                InstantiatePlacement();
            }
            else
            {
                if (!GameObject.FindGameObjectWithTag("Placing"))
                {
                    InstantiatePlacement();
                }
            }
        }
        if (ConstructTimer < 0)
        {
            ConstructTimer = ConstructTime;
            isDestroyed = false;
            isOpen = false;
            EndConstruct();
            BGM切换.instance.key = true;
            ConstructAnimation.instance.Close(StandTime);
            Global.instance.AudioPlay("chariot_constructed");
            loop++;
            if (loop == 3 || loop == 6) LevelUp();
            if (loop < 3 || isSkip)
            {
                int index = Random.Range(0, enemyCount);
                //if (index > 8) { isSkip = false; }
                EnemySpawn(index);
            }
            else
            {
                isSkip = true;
            }
        }
        else
        {
            if (相机固定位置.instance.目标物体 == Cat)
            {
                if (!isOpen)
                {
                    isOpen = true;
                    ConstructAnimation.instance.Open(StandTime);
                }
                if (ConstructAnimation.instance.direction == 0)
                {
                    if (!isConstruct)
                    {
                        BGM切换.instance.key = false;
                        StartConstruct();
                    }
                    ConstructTimer -= Time.deltaTime * 0.9f;
                    if (ConstructTimer < 0.15f && !isDestroyed)
                    {
                        isDestroyed = true;
                        DestroyArea();
                    }
                }
            }
        }
    }
    
    //生成放置组件
    public void InstantiatePlacement()
    {
        GameObject up = Instantiate(placingObject, mChariot.transform);
        up.GetComponent<UnitPlacing>().unitIndex = unitQueue.Dequeue();
        RandomUnit();
        List<int> t = new List<int>(unitQueue.ToArray());
        UnitNextShow.instance.unitIndex = t;
        UnitNextShow.instance.UnitUpdate();
    }

    //摧毁造车位置的车辆
    public void DestroyArea()
    {
        List<Collider2D> collider = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        chariotStartCollider.OverlapCollider(filter, collider);
        foreach(Collider2D col in collider)
        {
            if(col && col.gameObject.tag == "Unit" && col.GetComponentInParent<UnitPlaced>().team == 1)
            {
                col.GetComponentInParent<UnitPlaced>().TakeDamage(114514);
            }
        }

    }

    //开始造车
    public void StartConstruct()
    {
        Debug.Log("Start");
        isConstruct = true;
        mChariot = Instantiate(chariotObject, UnitGrid.instance.transform.position, Quaternion.Euler(0, 0, 0));
        unitQueue.Clear();
        unitQueue.Enqueue(listWeight[0].Pick());
        for (int i = 0; i < 4; i++)
        {
            unitQueue.Enqueue(listWeight[i].Pick());
        }
        foreach (Weight weight in listWeight) weight.Reset();
    }

    //结束造车
    public void EndConstruct()
    {
        Debug.Log("End");
        isConstruct = false;
        Destroy(GameObject.FindGameObjectWithTag("Placing"));
        mChariot.transform.position = chariotStartPos.position + new Vector3(10 * Global.instance.team, 0, 0);
        UnitGrid.instance.ChariotCombine();
        foreach (SpriteRenderer sprite in mChariot.GetComponentsInChildren<SpriteRenderer>())
        {
            if (sprite.gameObject.tag == "Outline") sprite.enabled = true;
        }
        foreach (Collider2D collider in mChariot.GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = true;
        }
        foreach (Joint2D joint in mChariot.GetComponentsInChildren<Joint2D>())
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
        foreach (UnitScript script in mChariot.GetComponentsInChildren<UnitScript>())
        {
            script.enabled = true;
        }
        foreach (UnitPlaced script in mChariot.GetComponentsInChildren<UnitPlaced>())
        {
            script.UnitSoloShut(null);
        }
        mChariot.tag = "Untagged";
        UnitGrid.instance.ClearGrid();
    }

    //刷新敌人
    public void EnemySpawn(int index)
    {
        GameObject enemy = Instantiate(Enemy[index], chariotEndPos.position, Quaternion.Euler(0, 0, 0));
        foreach (Rigidbody2D rbody in enemy.GetComponentsInChildren<Rigidbody2D>())
        {
            rbody.WakeUp();
            rbody.velocity = theCarrier.velocity;
        }
    }

    //权重初始化
    public void WeightInitialize()
    {
        List<int> lBlock = new List<int> { 0, 0, 1, 1, 2, 3 };
        List<int> lWheel = new List<int> { 4 };
        List<int> lMotor = new List<int> { 5, 5, 5, 5 };
        List<int> lWeapon = new List<int> { 11, 13 };
        List<int> lSpecial = new List<int> { 0 };
        Weight wBlock = new Weight(7.5f, 1, lBlock);
        Weight wWheel = new Weight(2, 0.75f, lWheel);
        Weight wMotor = new Weight(1, 0.5f, lMotor);
        Weight wWeapon = new Weight(2.5f, 2.5f, lWeapon);
        Weight wSpecial = new Weight(1, 0.5f, lSpecial);
        listWeight.Add(wBlock);
        listWeight.Add(wWheel);
        listWeight.Add(wMotor);
        listWeight.Add(wWeapon);
        listWeight.Add(wSpecial);
    }

    //分类加权随机
    public void RandomUnit()
    {
        float maxWeight = 0;
        foreach (Weight weight in listWeight) maxWeight += weight.nowWeight;
        float nowWeight = Random.Range(0, maxWeight);
        maxWeight = 0;
        bool isPick = false;
        int index = 0;
        foreach (Weight weight in listWeight)
        {
            maxWeight += weight.nowWeight;
            if(!isPick && maxWeight >= nowWeight)
            {
                isPick = true;
                index = weight.Pick();
            }
            else
            {
                weight.Drop();
            }
        }
        unitQueue.Enqueue(index);
    }

    //升本
    public void LevelUp()
    {
        if (loop == 3)
        {
            listWeight[2].unit.Add(6);
            listWeight[3].unit.Add(12);
            listWeight[3].unit.Add(14);
            listWeight[4].unit.Add(8);
            enemyCount += 4;
        }
        else
        {
            listWeight[2].unit.Add(7);
            listWeight[3].unit.Add(15);
            listWeight[3].unit.Add(16);
            listWeight[4].unit.Add(9);
            listWeight[4].unit.Add(10);
            enemyCount += 3;
        }
    }
}

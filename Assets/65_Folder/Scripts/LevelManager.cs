using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weight
{
    public List<int> unit;
    public float startWeight, baseWeight, addWeight, nowWeight;
    public Weight(float startW, float baseW, float addW, List<int> u)
    {
        unit = u;
        startWeight = startW;
        baseWeight = baseW;
        addWeight = addW;
        nowWeight = startWeight;
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
        nowWeight = startWeight;
    }
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform chariotStartPos, chariotEndPos;    //战车起始位置
    public Collider2D chariotStartCollider, chariotEndCollider;
    public List<GameObject> Enemy;
    public GameObject placingObject, chariotObject;
    public Rigidbody2D theCarrier;
    public float ConstructTime, ConstructTimer, StandbyTime, StandbyTimer;
    public bool isConstruct;
    private int enemyCount = 5;
    private GameObject mChariot;
    private bool keySkip, isDestroyed = false;
    private List<Weight> listWeight = new List<Weight>();
    private int loop = 0;
    private bool isSkip = false;
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
        if (StandbyTimer == 0)
        {
            if (ConstructTimer < 0 && StandbyTimer == 0)
            {
                ConstructTimer = 0;
                EndConstruct();
                loop++;
                if (loop == 4 || loop == 8) LevelUp();
                if (loop < 4 || isSkip)
                {
                    isSkip = false;
                    EnemySpawn(Random.Range(0, enemyCount));
                }
                else
                {
                    isSkip = true;
                }
                StandbyTimer = StandbyTime;
            }
            else
            {
                ConstructTimer -= Time.deltaTime;
                if (ConstructTimer == 0) ConstructTimer = -1;
                if (ConstructTimer < 0.15f && !isDestroyed)
                {
                    isDestroyed = true;
                    DestroyArea();
                }
            }
        }
        if (ConstructTimer == 0) {
            if (StandbyTimer < 0)
            {
                isDestroyed = false;
                StandbyTimer = 0;
                StartConstruct();
                ConstructTimer = ConstructTime;
            }
            else
            {
                StandbyTimer -= Time.deltaTime;
                if (StandbyTimer == 0) StandbyTimer = -1;
            }
        }
    }
    
    //生成放置组件
    public void InstantiatePlacement()
    {
        GameObject up = Instantiate(placingObject, mChariot.transform);
        up.GetComponent<UnitPlacing>().unitIndex = RandomUnit();
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
        isConstruct = true;
        mChariot = Instantiate(chariotObject, UnitGrid.instance.transform.position, Quaternion.Euler(0, 0, 0));
        foreach (Weight weight in listWeight) weight.Reset();
    }

    //结束造车
    public void EndConstruct()
    {
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
        ConstructAnimation.instance.Play(StandbyTime);
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
        List<int> lBlock = new List<int> { 0, 1, 1, 2, 3 };
        List<int> lWheel = new List<int> { 4 };
        List<int> lMotor = new List<int> { 5, 5, 5, 5 };
        List<int> lSpecial = new List<int> { 0 };
        List<int> lWeapon = new List<int> { 11, 13, 13 };
        Weight wBlock = new Weight(8, 8, 1, lBlock);
        Weight wWheel = new Weight(6, 2, 0.75f, lWheel);
        Weight wMotor = new Weight(15, 1, 0.5f, lMotor);
        Weight wSpecial = new Weight(0, 0.5f, 0.5f, lSpecial);
        Weight wWeapon = new Weight(0, 2.5f, 2, lWeapon);
        listWeight.Add(wBlock);
        listWeight.Add(wWheel);
        listWeight.Add(wMotor);
        listWeight.Add(wSpecial);
        listWeight.Add(wWeapon);
    }

    //分类加权随机
    public int RandomUnit()
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
        return index;
    }

    //升本
    public void LevelUp()
    {
        if (loop == 4)
        {
            listWeight[2].unit.Add(6);
            listWeight[3].unit.Add(8);
            listWeight[4].unit.Add(12);
            listWeight[4].unit.Add(14);
            enemyCount += 4;
        }
        else
        {
            listWeight[2].unit.Add(7);
            listWeight[3].unit.Add(9);
            listWeight[3].unit.Add(10);
            listWeight[4].unit.Add(15);
            listWeight[4].unit.Add(16);
            enemyCount += 3;
        }
    }
}

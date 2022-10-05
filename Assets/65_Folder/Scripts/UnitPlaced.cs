using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlaced : MonoBehaviour
{
    public float maxHP, nowHP;
    public int unitType, unitWidth, unitHeight, unitAngle, unitFlip;
    public bool canFlip;
    public int team;
    public List<Rigidbody2D> unitRbody;
    public List<UnitPlaced> jointUnit;
    public List<FixedJoint2D> mJoint;
    public bool isShut = false;
    public GameObject explosion, sceneShift;
    private float smokeTimer = 99999f, smokeTime = 99999f;
    private bool isDestroyed = false;
    public bool isActived = false;
    // Start is called before the first frame update
    void Start()
    {
        jointUnit.Add(this);
        nowHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (smokeTimer < 0)
        {
            smokeTimer = smokeTime;
            特效引用.instance.生成冒烟(transform.position, 0.1f, 0.1f, 3);
        }
        else
        {
            smokeTimer -= Time.deltaTime;
        }
    }

    public bool hasJoint(UnitPlaced unit)
    {
        bool has = false;
        foreach (UnitPlaced junit in jointUnit)
        {
            if(junit == unit)
            {
                has = true;
                break;
            }
        }
        return has;
    }

    //受伤
    public void TakeDamage(float damage)
    {
        Debug.Log("Damage:" + damage);
        nowHP -= damage;
        float ratio = nowHP / maxHP;
        if (ratio <= 0.75)
        {
            if (smokeTime > 1)
            {
                smokeTimer = 1;
            }
            smokeTime = 1;
        }
        if (ratio <= 0.5) smokeTime = 0.5f;
        if (ratio <= 0.25) smokeTime = 0.25f;
        if (nowHP < 0 && !isDestroyed)
        {
            isDestroyed = true;
            if (maxHP < 1000)
            {
                特效引用.instance.生成摧毁爆破烟雾(transform.position, 0.15f, 0.15f, 20);
                Global.instance.AudioPlay("unit_lost");
                Destroy(gameObject, 0.1f);
            }
            else
            {
                GameObject explode = Instantiate(explosion, transform.position, transform.rotation);
                explode.transform.localScale = new Vector3(10, 10, 1);
                GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(0, 500000), transform.position + new Vector3(-1, 0, 0));
                Global.instance.AudioPlay("car_explode");
                var script = sceneShift.GetComponent<标题场景控制>();
                if (team == 0)
                {
                    script.sceneName = "65's Scene 1";
                }
                else
                {
                    script.sceneName = "结束场景";
                }
                StartCoroutine(ChangeScene(3));
            }
        }
    }

    public IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);
        sceneShift.GetComponent<标题场景控制>().按钮();
    }

    //每有一个组件被摧毁，全体检测是否单独
    void OnDestroy()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Unit"))
        {
            UnitPlaced script = obj.GetComponent<UnitPlaced>();
            if (script) script.UnitSoloShut(this);
        }
    }
    
    //检测自己单独时，关闭功能
    public void UnitSoloShut(UnitPlaced des)
    {
        if ((isActived || team == 1) && !isShut && UnitSolo(des))
        {
            isShut = true;
            UnitShutDown();
        }
    }

    //检测是否自己没有相连的组件
    bool UnitSolo(UnitPlaced des)
    {
        bool isSolo = true;
        foreach(UnitPlaced script in jointUnit)
        {
            if (script && script!=this && script!=des)
            {
                isSolo = false;
                break;
            }
        }
        return isSolo;
    }

    //关闭组件功能
    void UnitShutDown()
    {
        if(GetComponentInChildren<ConstantForce2D>())
        {
            GetComponentInChildren<ConstantForce2D>().enabled = false;
        }
        if (GetComponentInChildren<UnitScript>())
        {
            GetComponentInChildren<UnitScript>().enabled = false;
        }
        if (GetComponentInChildren<WheelJoint2D>())
        {
            GetComponentInChildren<WheelJoint2D>().useMotor = false;
        }
    }

    //焊接零件（继承方法）
    public virtual void UnitCombine(int x, int y)
    {
        isActived = true;
        for (int dir = 1; dir <= 4; dir++)
        {
            UnitPlaced unitScript = UnitGrid.instance.GetUnitScript(x, y, dir);
            if (unitScript && !hasJoint(unitScript) && unitScript.unitType == 0)
            {
                jointUnit.Add(unitScript);
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = unitScript.unitRbody[0];
                mJoint.Add(joint);
            }
        }
    }
}

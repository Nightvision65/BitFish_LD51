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
    private float smokeTimer = 99999f, smokeTime = 99999f;
    private bool isDestroyed = false;
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
            ��Ч����.instance.����ð��(transform.position, 0.1f, 0.1f, 3);
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

    //����
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
            ��Ч����.instance.���ɴݻٱ�������(transform.position, 0.15f, 0.15f, 20);
            Destroy(gameObject, 0.1f);
        }
    }

    //ÿ��һ��������ݻ٣�ȫ�����Ƿ񵥶�
    void OnDestroy()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Unit"))
        {
            UnitPlaced script = obj.GetComponent<UnitPlaced>();
            if (script) script.UnitSoloShut(this);
        }
    }
    
    //����Լ�����ʱ���رչ���
    public void UnitSoloShut(UnitPlaced des)
    {
        if (!isShut && UnitSolo(des))
        {
            isShut = true;
            UnitShutDown();
        }
    }

    //����Ƿ��Լ�û�����������
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

    //�ر��������
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

    //����������̳з�����
    public virtual void UnitCombine(int x, int y)
    {
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

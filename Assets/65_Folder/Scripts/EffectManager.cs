using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public int effecttype;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("type", effecttype);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EffectEnd()
    {
        Destroy(gameObject);
    }
}

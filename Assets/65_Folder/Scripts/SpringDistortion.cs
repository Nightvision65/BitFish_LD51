using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDistortion : MonoBehaviour
{
    public Transform startPos, endPos;
    private SpriteRenderer mSprite;
    // Start is called before the first frame update
    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos.position;
        Vector3 offset = endPos.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, offset);
        transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        transform.localScale = new Vector3(Vector3.Distance(transform.position, endPos.position) / 0.32f, 1, 1);
    }
}

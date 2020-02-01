using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Core : MonoBehaviour
{

    private Vector2 originPos, currentPos, driftDifference;
    public float speed = 10.0f;
    public float driftThreshhold = 10;
    public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = this.transform.position;
        float step = speed * Time.deltaTime;
        if ((originPos - currentPos).magnitude > driftThreshhold)
        {
            driftDifference = originPos - currentPos;
            rigidbody.AddForce(driftDifference * step);
            //transform.position = Vector2.MoveTowards(currentPos, Vector2.Lerp(currentPos, originPos,step), step);
        }
    }

    public void SetAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<DragGameSprite>().AttachNewBody(rigidbody);
        }
    }

    public GameObject ReturnTopParent()
    {
        Transform temp = transform;
        while (temp.parent != null) temp = temp.parent;
        return temp.gameObject;
    }
}

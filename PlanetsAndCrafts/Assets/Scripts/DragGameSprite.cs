using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragGameSprite : Core
{
    private float startPosY, startPosX;
    private bool isBeingHeld = false;
    private Vector2 PreviousMousePos, CurrentMousePos, MousePosDifference;
    public float driftMultiplier = 25f;
    public float lerpMultiplierMin = 1f;
    public float lerpMultiplierMax = 10f;
    public float time;

    public Tools _tool;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            _tool = player.GetComponent<Tools>();
        }
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Debug.Log("currentmouse"+CurrentMousePos.ToString());
        //Debug.Log("previousmouse"+PreviousMousePos.ToString());
        //Debug.Log("object"+this.transform.position.ToString());
        
        

        if (isBeingHeld == true)
        {
            if (time < lerpMultiplierMin) time = lerpMultiplierMin;
            time = Mathf.Lerp(time, lerpMultiplierMax, 0.001f);
            PreviousMousePos = CurrentMousePos;
            CurrentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //this.gameObject.transform.position = Vector2.Lerp(transform.position, new Vector3(CurrentMousePos.x + startPosX, CurrentMousePos.y + startPosY), Time.deltaTime * time);
            this.gameObject.transform.position = Vector2.Lerp(transform.position, new Vector3(CurrentMousePos.x, CurrentMousePos.y), Time.deltaTime * time);
        }
        else
        {
            time = 0;
        }
    }

    
    void OnMouseDown()
    {
       // may want to set velocity to 0
        if(_tool._state == State.Hand && Input.GetMouseButtonDown(0))
        {
            rigidbody.velocity = Vector2.zero;
            CurrentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosX = CurrentMousePos.x - transform.position.x;
            startPosY = CurrentMousePos.y - transform.position.y;
            isBeingHeld = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_tool._state == State.Hand && collision.gameObject.tag == "Core")
        {
            EndBeingHeld();
        }
    }

    void OnMouseUp()
    {
        if (_tool._state == State.Hand) EndBeingHeld();
    }

    void EndBeingHeld()
    {
        CurrentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosDifference = CurrentMousePos - PreviousMousePos;
        MousePosDifference.Normalize();
        rigidbody.AddForce(MousePosDifference * driftMultiplier);
        isBeingHeld = false;
    }

    public bool CheckForCore()
    {
        Transform temp = transform;
        if (temp.tag == "Core") return true;
        while (temp.parent != null)
        {
            if (temp.parent.tag == "Core") return true;
            temp = temp.parent;
        }
        return false;
    }

    public void AttachNewBody(Rigidbody2D body)
    {
        if (transform.parent != null)
        {
            Rigidbody2D bodywody = GetComponent<Rigidbody2D>();
            if (bodywody) Destroy(bodywody);
            rigidbody = body;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Piece")
            {
                transform.GetChild(i).GetComponent<DragGameSprite>().AttachNewBody(body);
            }
        }
    }

    public DragGameSprite ReturnTopSprite()
    {
        Transform temp = transform;
        while (temp.parent != null) temp = temp.parent;
        DragGameSprite top = temp.GetComponent<DragGameSprite>();
        return top;
    }
}

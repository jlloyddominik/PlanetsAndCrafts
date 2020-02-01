using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragGameSprite : MonoBehaviour
{
    private float startPosY, startPosX;
    private bool isBeingHeld = false;
    private Vector2 PreviousMousePos, CurrentMousePos, MousePosDifference;
    public Rigidbody2D rigidbody;
    public float driftMultiplier = 25f;

    

    private void Update()
    {
        //Debug.Log("currentmouse"+CurrentMousePos.ToString());
        //Debug.Log("previousmouse"+PreviousMousePos.ToString());
        //Debug.Log("object"+this.transform.position.ToString());
        
       

        if (isBeingHeld == true)
        {
            PreviousMousePos = CurrentMousePos;
            CurrentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.gameObject.transform.position = new Vector2(CurrentMousePos.x + startPosX, CurrentMousePos.y + startPosY);
        }
    }

    
    void OnMouseDown()
    {
       // may want to set velocity to 0
        if(Input.GetMouseButtonDown(0))
        {
            rigidbody.velocity = Vector2.zero;
            CurrentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosX = CurrentMousePos.x - transform.position.x;
            startPosY = CurrentMousePos.y - transform.position.y;
            isBeingHeld = true;
        }
    }

    void OnMouseUp()
    {
        CurrentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosDifference = CurrentMousePos - PreviousMousePos;
        MousePosDifference.Normalize();
        rigidbody.AddForce(MousePosDifference * driftMultiplier);

        isBeingHeld = false;
   
    }

  
}

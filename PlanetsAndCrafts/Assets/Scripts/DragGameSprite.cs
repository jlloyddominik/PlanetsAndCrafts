using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragGameSprite : MonoBehaviour
{
    private float startPosY, startPosX;
    private bool isBeingHeld = false;

    private void Update()
    {
        if(isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    
    void OnMouseDown()
    {
       
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            startPosX = mousePos.x - transform.localPosition.x;
            startPosY = mousePos.y - transform.localPosition.y;

            isBeingHeld = true;
        }
    }

    
    void OnMouseUp()
    {
        isBeingHeld = false;
    }
}

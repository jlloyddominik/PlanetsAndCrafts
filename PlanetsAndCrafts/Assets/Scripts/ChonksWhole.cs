using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChonksWhole : MonoBehaviour
{

    public DragGameSprite[] chonks;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        bool chonksComplete = true;
        foreach (DragGameSprite chonk in chonks)
        {
            if (chonk.ReturnTopParent().tag != "Core")
            {
                chonksComplete = false;
            }  

        }
        if (chonksComplete == true)
        {
            Debug.Log("CHONKS COMPLETE!");
        }
    }
}

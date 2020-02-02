using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tools tool;
    public Button button;
    public Sprite[] buttonIcons;
    
    // Start is called before the first frame update

    private void Start()
    {
        button.onClick.AddListener(ChangeTool);
    }


    public void OnPointerEnter(PointerEventData data)
    {
        tool._hovering = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        tool._hovering = false;
    }

    void ChangeTool()
    {
        if (tool._state != State.GooglyEyes)
        {
            tool.ChangeTool();
            button.GetComponent<Image>().sprite = buttonIcons[(int)tool._state];
        }
    }
    
    
}

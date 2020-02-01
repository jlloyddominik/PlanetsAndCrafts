using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Tools tool;
    public Button button;
    // Start is called before the first frame update

    private void Start()
    {
        button.onClick.AddListener(tool.ChangeTool);
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
        Debug.Log("clicked");
        tool.ChangeTool();
    }
    
}

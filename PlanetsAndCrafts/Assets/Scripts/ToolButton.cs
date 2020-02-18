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
    private bool hasWonIcon;
    
    // Start is called before the first frame update

    private void Start()
    {
        hasWonIcon = false;
        button.GetComponent<Image>().sprite = buttonIcons[0];
        button.onClick.AddListener(ChangeTool);
    }

    private void Update()
    {
        if (!hasWonIcon && tool._state == State.GooglyEyes)
        {
            button.GetComponent<Image>().sprite = buttonIcons[(int)tool._state];
            hasWonIcon = true;
        }
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

    public void Reset()
    {
        button.GetComponent<Image>().sprite = buttonIcons[(int)tool._state];
    }
}

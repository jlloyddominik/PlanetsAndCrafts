﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Hand, Stapler, Tape, Glue, GooglyEyes}

public class Tools : MonoBehaviour
{
    public SpriteRenderer _renderer;
    public Sprite _mouse;
    public GameObject _stapler;
    public Sprite _tape;
    public Sprite _glue;
    public Sprite _pipeCleaner;
    public Sprite _sillyString;

    public State _state;

    public Vector3 currentPos;
    public Vector3 previousPos;

    public Vector2 prev2;
    public Vector2 cur2;
    public Vector2 dif;

    public float angle;
    public float tempAngle;
    public float rotSpeed;

    public bool _hovering;

    public bool _toolReady = true;

    public LayerMask _toolHits;
    private Sprite _googlyEyes;

    private void Update()
    {
        previousPos = currentPos;
        currentPos = Input.mousePosition;
        currentPos.z = 10;
        currentPos = Camera.main.ScreenToWorldPoint(currentPos);
        transform.position = currentPos;

        prev2 = new Vector2(previousPos.x, previousPos.y);
        cur2 = new Vector2(currentPos.x, currentPos.y);
        dif = cur2 - prev2;
        angle = Mathf.LerpAngle(angle, Vector2.SignedAngle(Vector2.right, dif), rotSpeed * Time.deltaTime);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        #region Switches what call functions for each state
        if (!_hovering)
        {
            if (Input.GetMouseButtonDown(0))
            {
                    switch (_state)
                    {
                        case State.Hand:
                            HandDown();
                            break;
                        case State.Stapler:
                            StaplerDown();
                            break;
                        case State.Tape:
                            TapeDown();
                            break;
                        case State.Glue:
                            GlueDown();
                            break;
                        case State.GooglyEyes:
                            GooglyEyesDown();
                            break;
                    }
            }

            if (Input.GetMouseButtonUp(0))
            {
                switch (_state)
                {
                    case State.Hand:
                        HandUp();
                        break;
                    case State.Stapler:
                        StaplerUp();
                        break;
                    case State.Tape:
                        TapeUp();
                        break;
                    case State.Glue:
                        GlueUp();
                        break;
                    case State.GooglyEyes:
                        GooglyEyesUp();
                        break;
                }
            }
        }

        #endregion

    }


    private void HandUp()
    {
    }

    private void HandDown()
    {
    }

    private void StaplerUp()
    {
        _toolReady = true;
    }

    private void StaplerDown()
    {
        Collider2D first = Physics2D.OverlapCircle(transform.TransformPoint(Vector3.up * 0.25f), 0.1f, _toolHits);
        Collider2D second = Physics2D.OverlapCircle(transform.TransformPoint(Vector3.down * 0.25f), 0.1f, _toolHits);
        if (first && second && first != second)
        {
            GameObject firstObject = first.gameObject;
            GameObject secondObject = second.gameObject;
            if (first.GetComponent<DragGameSprite>().CheckForCore())
            {
                if (first.tag == "Core") secondObject.GetComponent<DragGameSprite>().ReturnTopParent().transform.parent = first.transform;
                else
                {
                    secondObject.GetComponent<DragGameSprite>().ReturnTopParent().transform.parent = firstObject.GetComponent<DragGameSprite>().ReturnTopParent().transform;
                }
                firstObject.GetComponent<DragGameSprite>().ReturnTopParent().GetComponent<Core>().SetAllChildren();
            }
            else if (second.GetComponent<DragGameSprite>().CheckForCore())
            {
                if (second.tag == "Core") firstObject.GetComponent<DragGameSprite>().ReturnTopParent().transform.parent = second.transform;
                else
                {
                    firstObject.GetComponent<DragGameSprite>().ReturnTopParent().transform.parent = secondObject.GetComponent<DragGameSprite>().ReturnTopParent().transform;
                }
                secondObject.GetComponent<DragGameSprite>().ReturnTopParent().GetComponent<Core>().SetAllChildren();
            }
            else
            {
                firstObject.GetComponent<DragGameSprite>().ReturnTopParent().transform.parent = secondObject.GetComponent<DragGameSprite>().ReturnTopParent().transform;
                //second.
            }
        }
    }

    private void TapeUp()
    {
        _toolReady = true;
    }

    private void TapeDown()
    {
        throw new NotImplementedException();
    }

    private void GlueUp()
    {
        _toolReady = true;
    }

    private void GlueDown()
    {
        throw new NotImplementedException();
    }

    private void GooglyEyesUp()
    { }

    private void GooglyEyesDown()
    { }



    public void ChangeTool()
    {
        Deactivate(_state);
        int temp = (int)_state;
        temp++;
        temp %= 6;
        _state = (State)temp;
        Activate(_state);
    }

    private void Deactivate(State state)
    {
        switch (_state)
        {
            case State.Hand:
                _renderer.sprite = _mouse;
                break;
            case State.Stapler:
                _stapler.SetActive(false);
                break;
            case State.Tape:
                _renderer.sprite = _tape;
                break;
            case State.Glue:
                _renderer.sprite = _glue;
                break;
            case State.GooglyEyes:
                _renderer.sprite = _googlyEyes;
                break;
        }
    }

    private void Activate(State state)
    {
        switch (_state)
        {
            case State.Hand:
                _renderer.sprite = _mouse;
                break;
            case State.Stapler:
                _stapler.SetActive(true);
                break;
            case State.Tape:
                _renderer.sprite = _tape;
                break;
            case State.Glue:
                _renderer.sprite = _glue;
                break;
            case State.GooglyEyes:
                _renderer.sprite = _googlyEyes;
                break;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Hand, Stapler, Tape, Glue, GooglyEyes}

public class Tools : MonoBehaviour
{
    public float toolz;
    public float materialz;

    public SpriteRenderer _renderer;
    public Sprite _mouse;
    public GameObject _stapler;
    public GameObject _tapeTool;
    public GameObject _tapeProto;
    public GameObject _tape;
    public Vector3 _tapeToolOrigin;
    public Vector3 _tapeToolRot;
    public SpriteRenderer _tapeRenderer;
    public GameObject _glueBottle;
    public GameObject _glueProto;
    public GameObject _glue;
    public SpriteRenderer _glueRenderer;

    public GameObject _googlyEye;


    public State _state;

    public Vector3 currentPos;
    public Vector3 previousPos;

    public Vector2 prev2;
    public Vector2 cur2;
    public Vector2 dif;

    float angle;
    float tempAngle;
    public float rotSpeed;

    public bool _hovering;

    public bool _toolReady = true;

    public LayerMask _toolHits;
    private Sprite _googlyEyes;

    public Vector2 _tapeBegin;
    public Vector2 _tapeEnd;
    float anglebangle;
    public bool _taping;
    public float _maxTapeLength;
    public float _tapeFixMulti;

    public List<Color> _colours;
    public List<Sprite> _glueShapes;
    public List<Sprite> _tapeTypes;

    public AudioClip[] dronkAudio;
    public AudioClip[] chonkAudio;
    public AudioClip[] bigchonkAudio;
    public AudioClip[] tapeAudio;
    public AudioClip[] tapereactAudio;
    public AudioClip[] staplerAudio;
    public AudioClip[] staplerreactAudio;
    public AudioClip[] lavareactAudio;
    public AudioClip[] icereactAudio;
    public AudioClip[] hornAudio;
    public AudioClip[] hornreactAudio;
    public AudioClip[] handAudio;
    public AudioClip[] handreactAudio;
    public AudioClip[] glueAudio;
    public AudioClip[] gluereactAudio;
    public AudioClip[] bgreactAudio;

    /*    
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(1);
        Debug.Log("started");
    }
    */

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

        if (_taping)
        {
            _tapeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(_tapeBegin,_tapeEnd) > _maxTapeLength)
            {
                _tapeEnd = _tapeEnd - _tapeBegin;
                _tapeEnd.Normalize();
                _tapeEnd *= _maxTapeLength;
                _tapeEnd += _tapeBegin;
                //toolPos = 2f * _tapeEnd;
                //toolPos.z = -2f;
            }
            Vector3 toolPos = 2f * _tapeEnd;
            float tapeLength = Mathf.Min(Vector2.Distance(_tapeBegin, _tapeEnd), _maxTapeLength);
            anglebangle = -Vector2.SignedAngle(_tapeBegin, _tapeEnd);
            _tape.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, _tapeEnd - _tapeBegin) - 90);
            transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, _tapeEnd - _tapeBegin), Vector3.forward);
            _tapeTool.transform.position = _tapeBegin + (_tapeEnd - _tapeBegin).normalized * ((tapeLength * 1.3f) + .9f);
            _tapeRenderer.size = new Vector2(.5f,tapeLength);
        }

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
        Core firstTest = first.gameObject.GetComponent<Core>();
        Core secondTest = second.gameObject.GetComponent<Core>();
        if (firstTest.ReturnTopParent().tag != "Core" || secondTest.ReturnTopParent().tag != "Core")
        {
            if (first && second && first != second)
            {
                DragGameSprite firstObject = first.gameObject.GetComponent<DragGameSprite>();
                DragGameSprite secondObject = second.gameObject.GetComponent<DragGameSprite>();
                if (first.tag == "Core" || firstObject.CheckForCore())
                {
                    if (first.tag == "Core")
                    {
                        secondObject.ReturnTopParent().transform.parent = first.transform;
                        first.GetComponent<Core>().SetAllChildren();
                    }
                    else
                    {
                        secondObject.ReturnTopParent().transform.parent = firstObject.ReturnTopParent().transform;
                        firstObject.ReturnTopParent().GetComponent<Core>().SetAllChildren();

                    }
                }
                else if (second.tag == "Core" || second.GetComponent<DragGameSprite>().CheckForCore())
                {
                    if (second.tag == "Core")
                    {
                        firstObject.ReturnTopParent().transform.parent = second.transform;
                        second.GetComponent<Core>().SetAllChildren();
                        Debug.Log("got to if");
                    }
                    else
                    {
                        firstObject.ReturnTopParent().transform.parent = secondObject.ReturnTopParent().transform;
                        Debug.Log("got to else");
                        secondObject.ReturnTopParent().GetComponent<Core>().SetAllChildren();
                    }
                }
                else
                {
                    firstObject.ReturnTopParent().transform.parent = secondObject.ReturnTopParent().transform;
                    DragGameSprite top = secondObject.ReturnTopParent().GetComponent<DragGameSprite>();
                    top.AttachNewBody(top.rigidbody);
                }
                // Spawn a staple;
            }
        }
    }

    private void TapeUp()
    {
        _tapeTool.transform.parent = transform;
        _tapeTool.transform.localPosition = _tapeToolOrigin;
        _tapeTool.transform.localEulerAngles = _tapeToolRot;
        _toolReady = true;
        _taping = false;
        float dist = Mathf.Min(Vector2.Distance(_tapeBegin, _tapeEnd),_maxTapeLength);
        int firstBigBoi = -1;
        Core core;
        RaycastHit2D[] collisions = Physics2D.BoxCastAll(_tapeBegin, Vector2.one, 0, _tapeEnd - _tapeBegin, dist, _toolHits);
        if (collisions.Length > 1)
        {
            for (int i = 0; i < collisions.Length; i++)
            {
                Core temp = collisions[i].collider.GetComponent<Core>();
                if (temp && temp.ReturnTopParent().tag == "Core")
                {
                    firstBigBoi = i;
                    Debug.Log("Plant is in in group: " + firstBigBoi);
                    break;
                }
            }
            if (firstBigBoi >= 0)
            {
                core = collisions[firstBigBoi].collider.GetComponent<Core>().ReturnTopParent().GetComponent<Core>();
                for (int i = 0; i < collisions.Length; i++)
                {
                    GameObject temp = collisions[i].collider.GetComponent<Core>().ReturnTopParent();
                    if (firstBigBoi >= 0 && i != firstBigBoi && temp.tag != "Core")
                    {
                        temp.transform.parent = core.transform;
                        temp.GetComponent<DragGameSprite>().AttachNewBody(core.rigidbody);
                    }
                    _tape.transform.parent = core.transform;
                }
            }
            else
            {
                DragGameSprite newParent = collisions[0].collider.GetComponent<DragGameSprite>();
                for (int i = 1; i < collisions.Length; i++)
                {
                    DragGameSprite temp = collisions[i].collider.GetComponent<DragGameSprite>().ReturnTopSprite();
                    temp.transform.parent = newParent.transform;
                    temp.AttachNewBody(newParent.rigidbody);
                }
                _tape.transform.parent = newParent.transform;
            }
        }

    }

    private void TapeDown()
    {
        if (_toolReady)
        {
            _taping = true;
            _tapeBegin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _tape = Instantiate(_tapeProto);
            _tapeRenderer = _tape.GetComponent<SpriteRenderer>();
            _tapeRenderer.sprite = _tapeTypes[UnityEngine.Random.Range(0, _tapeTypes.Count)];
            _tapeRenderer.color = _colours[UnityEngine.Random.Range(0, _colours.Count)];
            _tape.SetActive(true);
            _tape.transform.position = _tapeBegin;
        }
        _toolReady = false;
    }

    private void GlueUp()
    {
        _toolReady = true;
    }

    private void GlueDown()
    {
        if (_toolReady)
        {
            int firstBigBoi = -1;
            Core core;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f, _toolHits);
            if (colliders.Length > 1)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Core temp = colliders[i].GetComponent<Core>();
                    if (temp && temp.ReturnTopParent().tag == "Core")
                    {
                        firstBigBoi = i;
                        Debug.Log("Plant is in in group: " + firstBigBoi);
                        break;
                    }
                }
                if (firstBigBoi >= 0)
                {
                    core = colliders[firstBigBoi].GetComponent<Core>().ReturnTopParent().GetComponent<Core>();
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        GameObject temp = colliders[i].GetComponent<Core>().ReturnTopParent();
                        if (firstBigBoi >= 0 && i != firstBigBoi && temp.tag != "Core")
                        {
                            temp.transform.parent = core.transform;
                            temp.GetComponent<DragGameSprite>().AttachNewBody(core.rigidbody);
                        }
                    }
                    _glue = Instantiate(_glueProto);
                    _glue.SetActive(true);
                    Vector3 gluepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    gluepos.z = materialz;
                    _glue.transform.position = gluepos;
                    _glue.transform.parent = core.transform;
                    _glueRenderer = _glue.GetComponent<SpriteRenderer>();
                    _glueRenderer.sprite = _glueShapes[UnityEngine.Random.Range(0, _glueShapes.Count)];
                    _glueRenderer.color = _colours[UnityEngine.Random.Range(0, _colours.Count)];
                }
                else
                {
                    DragGameSprite newParent = colliders[0].GetComponent<DragGameSprite>();
                    for (int i = 1; i < colliders.Length; i++)
                    {
                        DragGameSprite temp = colliders[i].GetComponent<DragGameSprite>().ReturnTopSprite();
                        temp.transform.parent = newParent.transform;
                        temp.AttachNewBody(newParent.rigidbody);
                    }
                    _glue = Instantiate(_glueProto);
                    _glue.SetActive(true);
                    Vector3 gluepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    gluepos.z = materialz;
                    _glue.transform.position = gluepos;
                    _glue.transform.parent = newParent.transform;
                    _glueRenderer = _glue.GetComponent<SpriteRenderer>();
                    _glueRenderer.sprite = _glueShapes[UnityEngine.Random.Range(0, _glueShapes.Count)];
                    _glueRenderer.color = _colours[UnityEngine.Random.Range(0, _colours.Count)];
                }
            }
            else Debug.Log("one or fewer");
        }
        _toolReady = false;
    }

    private void GooglyEyesUp()
    {
        _toolReady = true;
    }

    private void GooglyEyesDown()
    {
        if (_toolReady)
        {
            _toolReady = false;
            Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), .1f);
            if (col.tag == "Piece" || col.tag == "Core" || col.tag == "Material")
            {
                GameObject googly = Instantiate(_googlyEye);
                googly.SetActive(true);
                Vector3 eyepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                eyepos.z = 0;
                googly.transform.position = eyepos;
                googly.transform.parent = col.transform;
            }
        }
    }



    public void ChangeTool()
    {
        Deactivate(_state);
        int temp = (int)_state;
        temp++;
        temp %= 4;
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
                _tapeTool.SetActive(false);
                break;
            case State.Glue:
                _glueBottle.SetActive(false);
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
                _tapeTool.SetActive(true);
                break;
            case State.Glue:
                _glueBottle.SetActive(true);
                break;
            case State.GooglyEyes:
                _renderer.sprite = _googlyEyes;
                break;
        }
    }

}

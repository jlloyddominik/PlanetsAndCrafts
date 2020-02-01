using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Hand, Stapler, Tape, Glue, PipeCleaner, SillyString}

public class Tools : MonoBehaviour
{
    public SpriteRenderer _renderer;
    public Sprite _mouse;
    public Sprite _stapler;
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


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _state = State.Hand;
            _renderer.sprite = _mouse;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _state = State.Stapler;
            _renderer.sprite = _stapler;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _state = State.Tape;
            _renderer.sprite = _tape;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _state = State.Glue;
            _renderer.sprite = _glue;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _state = State.PipeCleaner;
            _renderer.sprite = _pipeCleaner;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _state = State.SillyString;
            _renderer.sprite = _sillyString;
        }

        if(Input.GetMouseButtonDown(0))
        {
            switch(_state)
            {

            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            switch(_state)
            { }
        }

    }

}

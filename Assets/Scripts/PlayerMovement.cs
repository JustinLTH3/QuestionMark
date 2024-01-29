using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public enum InputMode { Touch, Keyboard, Swipe }
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public InputMode inputMode = InputMode.Touch;

    [SerializeField] List<GameObject> PlayerPositions = new List<GameObject>();

    Vector2 fingerDown;
    Vector2 fingerUp;
    int Pos = 1;
    UnityAction OnInputModeChange;
    public void AddPlayerPos(GameObject pos)
    {
        PlayerPositions.Add(pos);
    }
    private void Awake()
    {
        instance = this;
        inputMode = Settings.instance.InputMode_;
        OnInputModeChange = delegate { inputMode = Settings.instance.InputMode_; };
        Settings.instance.OnInputModeChange.AddListener(OnInputModeChange);
    }
    void Start()
    {
        //transform.position = PlayerPositions[1].transform.position;
    }
    public void ResetPos()
    {
        if (PlayerPositions[Pos] != null)
        {
            transform.position = PlayerPositions[Pos].transform.position;
        }
    }
    void Update()
    {
        if (inputMode == InputMode.Swipe)
        {
            SwipeMove();
        }
        else if (inputMode == InputMode.Keyboard)
        {
            Keyboard();
        }
        else if (inputMode == InputMode.Touch)
        {
            TouchMove();
        }
    }
    void Move(int right)
    {
        int tempPos = Pos + right;
        if (tempPos < PlayerPositions.Count && tempPos >= 0)
        {
            transform.position = PlayerPositions[tempPos].transform.position;
            Pos = tempPos;
        }
    }
    void TouchMove()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log(Input.touchCount);
            if (Input.mousePosition.x > (Screen.width * 0.75f))
            {
                Move(1);
            }
            else if (Input.mousePosition.x <= (Screen.width * 0.25f))
            {
                Move(-1);
            }
        }
    }
    void SwipeMove()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                fingerDown = Input.touches[0].position;
                Debug.Log("touch");
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                fingerUp = Input.touches[0].position;
                CheckSwipe();
            }
        }

    }

    void Keyboard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(1);
        }
    }

    void CheckSwipe()
    {
        if (fingerDown.x - fingerUp.x < 0)
        {
            Move(1);
        }

        else if (fingerDown.x - fingerUp.x > 0)
        {
            Move(-1);
        }
    }
    private void OnDestroy()
    {
        Settings.instance.OnInputModeChange.RemoveListener(OnInputModeChange);
    }
}

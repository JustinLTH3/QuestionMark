using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public enum InputMode { Touch, Keyboard, Swipe }
    public InputMode inputMode = InputMode.Touch;

    [SerializeField] List<GameObject> PlayerPositions = new List<GameObject>();

    Vector2 fingerDown;
    Vector2 fingerUp;
    float moveInput;
    public void AddPlayerPos(GameObject pos)
    {
        PlayerPositions.Add(pos);
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //transform.position = PlayerPositions[1].transform.position;
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

    void TouchMove()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log(Input.touchCount);
            if (Input.mousePosition.x > (Screen.width * 0.75f))
            {
                if (transform.position == PlayerPositions[1].transform.position)
                {
                    transform.position = PlayerPositions[2].transform.position;
                }
                else if (transform.position == PlayerPositions[0].transform.position)
                {
                    transform.position = PlayerPositions[1].transform.position;
                }
            }
            if (Input.mousePosition.x <= (Screen.width * 0.25f))
            {
                if (transform.position == PlayerPositions[1].transform.position)
                {
                    transform.position = PlayerPositions[0].transform.position;
                }
                else if (transform.position == PlayerPositions[2].transform.position)
                {
                    transform.position = PlayerPositions[1].transform.position;
                }
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
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                fingerUp = Input.touches[0].position;
                CheckSwipe();
            }
        }

    }

    void Keyboard()
    {
        if (Input.GetKeyDown(KeyCode.R)) { transform.position = PlayerPositions[1].transform.position; }
        if (transform.position == PlayerPositions[1].transform.position)
        {
            if (Input.GetKeyDown("a"))
            {
                transform.position = PlayerPositions[0].transform.position;
            }
            else if (Input.GetKeyDown("d"))
            {
                transform.position = PlayerPositions[2].transform.position;
            }
        }
        else if (transform.position == PlayerPositions[2].transform.position)
        {
            if (Input.GetKeyDown("a"))
            {
                transform.position = PlayerPositions[1].transform.position;
            }
            else if (Input.GetKeyDown("d"))
            {
                transform.position = PlayerPositions[2].transform.position;
            }
        }
        else if (transform.position == PlayerPositions[0].transform.position)
        {
            if (Input.GetKeyDown("a"))
            {
                transform.position = PlayerPositions[0].transform.position;
            }
            else if (Input.GetKeyDown("d"))
            {
                transform.position = PlayerPositions[1].transform.position;
            }
        }


    }

    void CheckSwipe()
    {
        if (fingerDown.x - fingerUp.x < 0)
        {
            OnSwipeRight();
        }

        else if (fingerDown.x - fingerUp.x > 0)
        {
            OnSwipeLeft();
        }
    }

    void OnSwipeLeft()
    {
        if (transform.position == PlayerPositions[1].transform.position)
        {
            transform.position = PlayerPositions[0].transform.position;
        }
        else if (transform.position == PlayerPositions[2].transform.position)
        {
            transform.position = PlayerPositions[1].transform.position;
        }
    }

    void OnSwipeRight()
    {
        if (transform.position == PlayerPositions[1].transform.position)
        {
            transform.position = PlayerPositions[2].transform.position;
        }
        else if (transform.position == PlayerPositions[0].transform.position)
        {
            transform.position = PlayerPositions[1].transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    public enum Direction { Left, Right, Up, Down};
    private bool[] swipe = new bool[4];

    private Vector2 startTouch;
    private bool touchMoved;
    private Vector2 swipeDelta;

    const float SWIPE_THRESHOLD = 50;

    public delegate void MoveDelegate(bool[] swipes);
    public MoveDelegate MoveEvent;

    public delegate void ClickDelegate(Vector2 pos);
    public ClickDelegate ClickEvent;
    private Vector2 TouchPos()
    {
        return (Vector2)Input.mousePosition;
    }
    private bool TouchBegan()
    {
        return Input.GetMouseButtonDown(0);
    }
    private bool TouchEnded()
    {
        return Input.GetMouseButtonUp(0);
    }
    private bool GetTouch()
    {
        return Input.GetMouseButton(0);
    }

    
    void Awake()
    {
        instance = this;
    }

    
    void Update()
    {
        if (TouchBegan()) // начало свайпа
        {
            startTouch = TouchPos();
            touchMoved = true;
        }
        else if(TouchEnded() && touchMoved == true) // конец свайпа
        {
            SendSwipe();
            touchMoved = false;
        }

        swipeDelta = Vector2.zero;
        if(touchMoved && GetTouch()) // измеряем растояние свайпа
        {
            swipeDelta = TouchPos() - startTouch;
        }

        if(swipeDelta.magnitude > SWIPE_THRESHOLD) // был ли свайп
        {
            if(Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // свайп в лево или в право
            {
                swipe[(int)Direction.Left] = swipeDelta.x < 0;
                swipe[(int)Direction.Right] = swipeDelta.x > 0;
            }
            if (Mathf.Abs(swipeDelta.x) < Mathf.Abs(swipeDelta.y)) // свайп up или down
            {
                swipe[(int)Direction.Down] = swipeDelta.y < 0;
                swipe[(int)Direction.Up] = swipeDelta.y > 0;
            }

            SendSwipe();
        }
        

    }
    private void SendSwipe()
    {
        if(swipe[0] || swipe[1] || swipe[2] || swipe[3])
        {
           
            if(MoveEvent != null)
            {
                MoveEvent(swipe);
            }
            if (swipe[1])
            {
                Player.MoveRight();
            }
            else if (swipe[0])
            {
                Player.MoveLeft();
            }
            else if (swipe[2])
            {
                RoadManager.curSpeed += 10;
            }
            else if (swipe[3])
            {
                RoadManager.curSpeed -= 10;
            }
        }
        else
        {
            
            if (ClickEvent != null)
            {
                ClickEvent(TouchPos());
            }
        }
        Reset();
    }
    private void Reset() // сбрасывыем данные о прощедшем свайпе
    {
        startTouch = swipeDelta = Vector2.zero;
        touchMoved = false;
        for(int i = 0; i< 2; i++)
        {
            swipe[i] = false;
        }
    }
}

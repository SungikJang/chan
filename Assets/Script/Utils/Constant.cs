using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public enum MouseEvent
    {
        Clicked,
        Pressed
    }
    public enum KeyBoardEvent
    {
        Clicked,
        Pressed
    }
    public enum State
    {
        Die,
        Move,
        Idle,
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
}

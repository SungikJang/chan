using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagers
{
    public Action<Constant.KeyBoardEvent> KeyBoardAction = null;
    public Action<Constant.MouseEvent> MouseAction = null;

    private bool MousePressed = false;
    private bool KeyBorad_Any_Pressed = false;

    public void OnUpdate()
    {
        if (KeyBoardAction != null)
        {
            if (Input.anyKey)
            {
                KeyBorad_Any_Pressed = true;
                KeyBoardAction.Invoke(Constant.KeyBoardEvent.Pressed);
            }
            else
            {
                KeyBorad_Any_Pressed = false;
            }
        }
        if (MouseAction != null)
        {
            if (Input.GetMouseButton(1))
            {
                MousePressed = true;
                MouseAction.Invoke(Constant.MouseEvent.Pressed);
            }
            else
            {
                MousePressed = false;
            }
            

        }
    }
}

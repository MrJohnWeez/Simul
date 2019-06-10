using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType
{
    None,
    Xbox,
    PS4
}

public static class InputHandler
{
    public static ControllerType connectedController;
    public static float GetAxisRaw(string axis)
    {
        return Input.GetAxisRaw(axis);
    }

    public static void CheckForConnectedControllers()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 19)
            {
                Debug.Log("PS4 CONTROLLER IS CONNECTED");
                connectedController = ControllerType.PS4;
                return;
            }
            if (names[x].Length == 33)
            {
                Debug.Log("XBOX ONE CONTROLLER IS CONNECTED");
                connectedController = ControllerType.Xbox;
                return;
            }
        }
        connectedController = ControllerType.None;
    }

    public static float GetAxis(string axis)
    {
        float returnThis = 0;
        if(axis == "LookX")
        {
            returnThis = Input.GetAxis("Mouse X");
            if(returnThis != 0)
                return returnThis;

            if(connectedController == ControllerType.Xbox)
            {
                returnThis = Input.GetAxis("Mouse X Xbox");
            }
            else if(connectedController == ControllerType.PS4)
            {
                returnThis = Input.GetAxis("Mouse X PS4");
            }
        }
        else
        {
            returnThis = Input.GetAxis(axis);
        }

        return returnThis;
    }

    public static bool GetButtonDown(string buttonValue)
    {
        bool returnThis = false;
        if(buttonValue == "DownButton")
        {
            returnThis = Input.GetButtonDown("Jump");
            if(returnThis)
                return returnThis;

            if(connectedController == ControllerType.Xbox)
            {
                returnThis = Input.GetButtonDown("Xbox Button Down");
            }
            else if(connectedController == ControllerType.PS4)
            {
                returnThis = Input.GetButtonDown("PS4 Button Down");
            }
        }
        else if(buttonValue == "LeftButton")
        {
            returnThis = Input.GetKeyDown(KeyCode.Tab); 
            if(returnThis)
                return returnThis;

            if(connectedController == ControllerType.Xbox)
            {
                returnThis = Input.GetButtonDown("Xbox Button Left");
            }
            else if(connectedController == ControllerType.PS4)
            {
                returnThis = Input.GetButtonDown("PS4 Button Left");
            }
        }
        else
        {
            Input.GetButtonDown(buttonValue);
        }

        return returnThis;
    }
}

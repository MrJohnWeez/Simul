using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType
{
    None,
    Xbox,
    PS4
}

public class InputHandler: MonoBehaviour
{
    public Joystick touchMovement = null;
    public static InputHandler instance = null;
    public ControllerType connectedController;

    private void Start() {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public float GetAxisRaw(string axis)
    {
        Vector2 touchMovementRaw = new Vector2(touchMovement.Horizontal, touchMovement.Vertical);

        if(touchMovement && axis == "Horizontal" && touchMovementRaw.x != 0)
        {
            return touchMovementRaw.x;
        }
        else if(touchMovement && axis == "Vertical" && touchMovementRaw.y != 0)
        {
            return touchMovementRaw.y;
        }
        
        return Input.GetAxisRaw(axis);
    }

    public void CheckForConnectedControllers()
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

    public float GetAxis(string axis)
    {
        float returnThis = 0;
        if(axis == "LookX")
        {
            returnThis = Input.GetAxis("Mouse X");
            if(returnThis != 0)
                return returnThis;

            returnThis = CameraTouchController.delta.x;
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

    public bool GetButtonDown(string buttonValue)
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

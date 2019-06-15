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
    public Joystick leftTouchMovement = null;
    public Joystick rightTouchMovement = null;
    
    public static InputHandler instance = null;
    public ControllerType connectedController;
    private Joystick touchMovement = null;

    private void Start() {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        touchMovement = rightTouchMovement;
    }

    public void UpdateTouchStick()
    {
        if(SettingsController.joystickIsRight)
        {
            touchMovement = rightTouchMovement;
        }
        else
        {
            touchMovement = leftTouchMovement;
        }
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
        else if(buttonValue == "UpButton")
        {
            returnThis = Input.GetKeyDown(KeyCode.R); 
            if(returnThis)
                return returnThis;

            if(connectedController == ControllerType.Xbox)
            {
                returnThis = Input.GetButtonDown("Xbox Button Up");
            }
            else if(connectedController == ControllerType.PS4)
            {
                returnThis = Input.GetButtonDown("PS4 Button Up");
            }
        }
        else if(buttonValue == "Menu")
        {
            returnThis = Input.GetKeyDown(KeyCode.Escape); 
            if(returnThis)
                return returnThis;

            if(connectedController == ControllerType.Xbox)
            {
                returnThis = Input.GetButtonDown("Xbox Button Menu");
            }
            else if(connectedController == ControllerType.PS4)
            {
                returnThis = Input.GetButtonDown("PS4 Button Options");
            }
        }
        else
        {
            Input.GetButtonDown(buttonValue);
        }

        return returnThis;
    }
}

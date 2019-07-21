using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ControllerType
{
    None,
    Xbox,
    PS4
}

public class InputHandler: MonoBehaviour
{
    public static bool ForceZero = false;
    public Joystick leftTouchMovement = null;
    public Joystick rightTouchMovement = null;
    
    public static InputHandler instance = null;
    public ControllerType connectedController;
    private Joystick touchMovement = null;
    public Text debugText = null;

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

    private void Update() {
        // Debug.Log("RightX: " + GetAxis("LookX") + 
        //             "   RightY: " + GetAxis("LookY") + 
        //             "   MoveX: " + GetAxis("MoveX") + 
        //             "   MoveY: " + GetAxis("MoveY"));
        // if(debugText)
        // {
        //     debugText.text = ("RX: " + GetAxis("LookX") + 
        //             "   RY: " + GetAxis("LookY") + 
        //             "   MX: " + GetAxis("MoveX") + 
        //             "   MY: " + GetAxis("MoveY"));
        // }
        
    }


    public float GetAxis(string axis)
    {
        if(ForceZero)
            return 0;

        if(axis == "LookX")
        {
            #if !UNITY_ANDROID
                if(Input.GetAxisRaw("Mouse X") != 0)
                {
                    return Input.GetAxisRaw("Mouse X");
                }
            #endif
            
            if(CameraTouchController.delta.x != 0)
            {
                return CameraTouchController.delta.x;
            }

            if(connectedController == ControllerType.Xbox)
            {
                return ConvertRange(-(2.0f/3.0f), (2.0f/3.0f), -1, 1, Input.GetAxisRaw("RightStick X Xbox"));
            }
            else if(connectedController == ControllerType.PS4)
            {
                return Input.GetAxisRaw("RightStick X PS4");
            }
        }
        else if(axis == "LookY")
        {
            #if !UNITY_ANDROID
                if(Input.GetAxisRaw("Mouse Y") != 0)
                {
                    return Input.GetAxisRaw("Mouse Y");
                }
            #endif
            
            if(CameraTouchController.delta.y != 0)
            {
                return CameraTouchController.delta.y;
            }

            if(connectedController == ControllerType.Xbox)
            {
                return ConvertRange(-(2.0f/3.0f), (2.0f/3.0f), -1, 1, Input.GetAxisRaw("RightStick Y Xbox"));
            }
            else if(connectedController == ControllerType.PS4)
            {
                return Input.GetAxisRaw("RightStick Y PS4");
            }
        }
        else if (axis == "MoveX")
        {
            if(touchMovement && touchMovement.Horizontal != 0)
            {
                return touchMovement.Horizontal;
            }

            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                return Input.GetAxisRaw("Horizontal");
            }

            if(connectedController == ControllerType.Xbox)
            {
                return Input.GetAxisRaw("LeftStick X Xbox");
            }
            else if(connectedController == ControllerType.PS4)
            {
                return Input.GetAxisRaw("LeftStick X PS4");
            }
            
        }
        else if (axis == "MoveY")
        {
            if(touchMovement && touchMovement.Vertical != 0)
            {
                return touchMovement.Vertical;
            }

            if(Input.GetAxisRaw("Vertical") != 0)
            {
                return Input.GetAxisRaw("Vertical");
            }

            if(connectedController == ControllerType.Xbox)
            {
                return Input.GetAxisRaw("LeftStick Y Xbox");
            }
            else if(connectedController == ControllerType.PS4)
            {
                return Input.GetAxisRaw("LeftStick Y PS4");
            }
        }
        else
        {
            return 0;
        }

        return 0;
    }

    public static float ConvertRange(
    float originalStart, float originalEnd, // original range
    float newStart, float newEnd, // desired range
    float value) // value to convert
    {
        double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
        return (float)(newStart + ((value - originalStart) * scale));
    }

    public bool GetButtonDown(string buttonValue)
    {
        if(ForceZero)
            return false;
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
            returnThis = Input.GetKeyDown(KeyCode.Q); 
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Camera : MonoBehaviour
{


    public GameObject target;
    public float rotateSpeed = 5;
    public Vector3 offset; 
    float rotation;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    private bool PS4_Controller = false;
    private bool Xbox_One_Controller = false;
    void LateUpdate()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                PS4_Controller = true;
                Xbox_One_Controller = false;
            }
            if (names[x].Length == 33)
            {
                print("XBOX ONE CONTROLLER IS CONNECTED");
                //set a controller bool to true
                PS4_Controller = false;
                Xbox_One_Controller = true;
            }
        }
        if(Xbox_One_Controller)
        {
            Debug.Log("Hello Xbox");
        }
        else if(PS4_Controller)
        {
            Debug.Log("Hello Ps4");
        }
        // Need to include an axis for the controller. 
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        rotation += horizontal * rotateSpeed;

        transform.position = target.transform.position - offset;
        transform.LookAt(target.transform);
        transform.RotateAround(target.transform.position, target.transform.up, rotation);
    }

}
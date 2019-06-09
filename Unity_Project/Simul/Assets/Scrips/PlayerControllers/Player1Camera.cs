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


    void LateUpdate()
    {
        // Need to include an axis for the controller. 
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        rotation += horizontal * rotateSpeed;

        transform.position = target.transform.position - offset;
        transform.LookAt(target.transform);
        transform.RotateAround(target.transform.position, target.transform.up, rotation);
    }

}
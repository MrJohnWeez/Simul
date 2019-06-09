using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask layersToJumpOn;
    public GameObject cameraPlaceholder = null;
    public float moveSpeed = 7000;
    public float jumpHeight = 1;
    public bool isActive = false;
    public float jumpGravity = 9.8f;

    private Rigidbody rb = null;
    private float moveHorizontal = 0;
    private float moveVertical = 0;
    Vector3 forwardMotion;
    Vector3 rightMotion;
    Vector3 motion;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Cursor.visible = false;
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

        // If using keyboard make sure values are clamped to a magnitude of 1
        if(direction.magnitude > 1) {
            direction = direction.normalized;
        }
        
        // Move the character realitive to the camera 
        forwardMotion = moveVertical * Vector3.ProjectOnPlane(cameraPlaceholder.transform.forward, transform.up).normalized;
        rightMotion = moveHorizontal * Vector3.ProjectOnPlane(cameraPlaceholder.transform.right, transform.up).normalized;
        motion = forwardMotion + rightMotion;

        // Aligh character in the direction of travel
        if(direction != Vector3.zero) {
            transform.LookAt(transform.position + motion, transform.up);
        }

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position + new Vector3(0,0.1f,0), -transform.up, out hit, 0.2f, layersToJumpOn);
    }

    private void FixedUpdate() {
        if(isActive)
        {
            rb.AddForce(motion * moveSpeed, ForceMode.Force);
            if(!isGrounded)
                rb.AddForce(new Vector3(0,-jumpGravity, 0), ForceMode.Acceleration);
    
            if(isGrounded && Input.GetButtonDown("Jump"))
                rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            
        }
    }
}

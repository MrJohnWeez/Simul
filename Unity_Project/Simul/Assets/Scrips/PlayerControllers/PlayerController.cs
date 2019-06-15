using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool isActive = true;

    // Player jump vars
    public LayerMask layersToJumpOn;
    public float jumpHeight = 1000;
    public float inAirGravity = 20f;

    public GameObject cameraPlaceholder = null;
    public float moveSpeed = 7000;

    private Rigidbody rb = null;
    private Vector3 motion;
    private bool isGrounded = false;

    // Camera control variables
    private GameObject mainCamera = null;
    public float cameraRotateSpeed = 2;
    public Vector3 cameraOffset; 
    float currentCameraRotation;
    private ControllerType controllerType = ControllerType.None;
    private bool checkedForControllers = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main.gameObject;
        cameraOffset = cameraPlaceholder.transform.position - transform.position;
    }

    private void Update()
    {
        if(!checkedForControllers && InputHandler.instance != null)
        {
            checkedForControllers = true;
            InputHandler.instance.CheckForConnectedControllers();
        }

        #if !UNITY_EDITOR
            if(SettingsController.IsPaused)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        #endif

        if(isActive)
        {
            float moveHorizontal = 0;
            float moveVertical = 0;

            if(!SettingsController.IsPaused)
            {
                moveHorizontal = InputHandler.instance.GetAxisRaw("Horizontal");
                moveVertical = InputHandler.instance.GetAxisRaw("Vertical");
            }

            Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

            // If using keyboard make sure values are clamped to a magnitude of 1
            if(direction.magnitude > 1) {
                direction = direction.normalized;
            }
            
            // Move the character realitive to the camera 
            Vector3 forwardMotion = moveVertical * Vector3.ProjectOnPlane(cameraPlaceholder.transform.forward, transform.up).normalized;
            Vector3 rightMotion = moveHorizontal * Vector3.ProjectOnPlane(cameraPlaceholder.transform.right, transform.up).normalized;
            motion = forwardMotion + rightMotion;

            // Aligh character in the direction of travel
            if(direction != Vector3.zero) {
                transform.LookAt(transform.position + motion, transform.up);
            }
        }
        

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position + new Vector3(0,0.1f,0), -transform.up, out hit, 0.2f, layersToJumpOn);


        if(isActive)
        {
            float horizontal = 0;

            if(!SettingsController.IsPaused)
            {
                horizontal = InputHandler.instance.GetAxis("LookX") * cameraRotateSpeed;
            }

            currentCameraRotation += horizontal * cameraRotateSpeed;

            cameraPlaceholder.transform.position = transform.position + cameraOffset;
            cameraPlaceholder.transform.LookAt(transform);
            cameraPlaceholder.transform.RotateAround(transform.position, transform.up, currentCameraRotation);
        }
    }

    private void FixedUpdate() {
        if(isActive)
        {
            rb.AddForce(motion * moveSpeed, ForceMode.Force);
            if(!isGrounded)
                rb.AddForce(new Vector3(0,-inAirGravity, 0), ForceMode.Acceleration);
    
            if(isGrounded && InputHandler.instance.GetButtonDown("DownButton") && !SettingsController.IsPaused)
                rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            
        }
    }

    void LateUpdate()
    {
        if(isActive)
        { 
            mainCamera.transform.position = cameraPlaceholder.transform.position;
            mainCamera.transform.rotation = cameraPlaceholder.transform.rotation;
        }
    }
}

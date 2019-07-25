using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool isActive = true;
    public Animator anim;

    [Header("Player jump vars")]
    public LayerMask layersToJumpOn;
    public float jumpHeight = 1000;
    public float inAirGravity = 20f;
    public GameObject cameraPlaceholder = null;
    public float moveSpeed = 5600;
    
    [Header("Camera Properties")]
    public Transform target;
    public float defualtSmoothing = 0.0f;
    public float verticalRotation = 0.0f;
    public float horizontalRotation = 180.0f;
    public float distanceAway = 2.81f;
    public Vector3 CamOffset = new Vector3(0,0.6f,0);

    [Header("Camera Limits")]
    public float verticleRotationLimitMin = 89;
    public float verticleRotationLimitMax = 22;
    public float maxDistanceAway = 3;
    public float minDistanceAway = 0.1f;
    public LayerMask layerMaskToExclude;

    private Rigidbody rb = null;
    private Vector3 motion;
    private bool isGrounded = false;

    //Sound Vars
    public AudioSource AudioClip = null;
    private GameObject camMask;
    private Vector3 _distanceOffset;
    private Vector3 _targetOffset;
    private Quaternion _newRotation;
    private bool _camHittingWall = false;
    private float smooth = 0f;
    private GameObject _mainCamera = null;
    private bool _checkedForControllers = false;
    private bool _isPlayingAudio = false;

    void Start()
    {
        camMask = new GameObject("camTransform");
        camMask.transform.position = cameraPlaceholder.transform.position;
        camMask.transform.rotation = cameraPlaceholder.transform.rotation;
        camMask.transform.localScale = cameraPlaceholder.transform.localScale;
        smooth = defualtSmoothing;

        rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        if(!_checkedForControllers && InputHandler.instance != null)
        {
            _checkedForControllers = true;
            InputHandler.instance.CheckForConnectedControllers();
        }

        if(isActive)
        {
            float moveHorizontal = 0;
            float moveVertical = 0;

            if (SettingsController.UserInput)
            {
                moveHorizontal = SettingsController.InvertMovementX ? -InputHandler.instance.GetAxis("MoveX") : InputHandler.instance.GetAxis("MoveX");

                moveVertical = SettingsController.InvertMovementY ? -InputHandler.instance.GetAxis("MoveY") : InputHandler.instance.GetAxis("MoveY");

                if((moveHorizontal != 0 || moveVertical != 0) && !_isPlayingAudio)
                {
                    _isPlayingAudio = true;
                    AudioClip.Play();
                }
                else if(moveHorizontal == 0 && moveVertical == 0 && _isPlayingAudio)
                {
                    _isPlayingAudio = false;
                    AudioClip.Stop(); // or Pause()
                }
            }

            Vector3 direction = new Vector3(moveHorizontal, 0, moveVertical);

            // If using keyboard make sure values are clamped to a magnitude of 1
            if(direction.magnitude > 1) {
                direction = direction.normalized;
            }

            anim.SetFloat ("MoveAmount", direction.magnitude);
            
            // Move the character realitive to the camera 
            Vector3 forwardMotion = direction.z * Vector3.ProjectOnPlane(cameraPlaceholder.transform.forward, transform.up).normalized;
            Vector3 rightMotion = direction.x * Vector3.ProjectOnPlane(cameraPlaceholder.transform.right, transform.up).normalized;
            motion = forwardMotion + rightMotion;

            // Aligh character in the direction of travel
            if(direction != Vector3.zero) {
                transform.LookAt(transform.position + motion, transform.up);
            }
        }
        else
        {
            if(!SettingsController.UserInput)
            {
                if(_isPlayingAudio)
                {
                    _isPlayingAudio = false;
                    AudioClip.Stop(); // or Pause()
                }
            }
            anim.SetFloat ("MoveAmount", 0);
        }
        

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position + new Vector3(0,0.1f,0), -transform.up, out hit, 0.2f, layersToJumpOn);


        if(isActive && SettingsController.UserInput)
        {
            int invertX = SettingsController.InvertCameraX ? -1 : 1;
            int invertY = SettingsController.InvertCameraY ? -1 : 1;

            // Get inputs
            verticalRotation += InputHandler.instance.GetAxis("LookY") * SettingsController.CameraSensitivityY * 0.1f * invertX;
            horizontalRotation += InputHandler.instance.GetAxis("LookX") * SettingsController.CameraSensitivityX * 0.1f * invertY;

            // Determine camera position and rotation
            _targetOffset = target.position + CamOffset;
            _distanceOffset = new Vector3(0, 0, Mathf.Clamp(distanceAway, minDistanceAway, maxDistanceAway));
            verticalRotation = Mathf.Clamp(verticalRotation, -verticleRotationLimitMin, verticleRotationLimitMax);
            _newRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

            camMask.transform.position = _targetOffset + _newRotation * _distanceOffset;
            camMask.transform.LookAt(_targetOffset);

            // Wall detection and smoothing
            _camHittingWall = WallDetection(ref _targetOffset);
            smoothCamMethod();
        }
    }

    public void Disable()
    {
        isActive = false;
        anim.SetFloat ("MoveAmount", 0);  
    }

    private void FixedUpdate() {
        if(isActive)
        {
            rb.AddForce(motion * moveSpeed, ForceMode.Force);
            
        }

        if(!isGrounded)
            rb.AddForce(new Vector3(0,-inAirGravity, 0), ForceMode.Acceleration);

        
        // Disable jump because it is not used in our game and is buggy
        // if(isGrounded && InputHandler.instance.GetButtonDown("DownButton") && SettingsController.UserInput)
        //     rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
    }

    /// <summary>
    /// Smooths the camera follow and prevents bouncy camera motion
    /// </summary>
    void smoothCamMethod()
	{
        float lagDistance = Vector3.Distance(cameraPlaceholder.transform.position, camMask.transform.position);
        if(!_camHittingWall && lagDistance > maxDistanceAway)
        {
            smooth = defualtSmoothing;
            cameraPlaceholder.transform.position = Vector3.Lerp(cameraPlaceholder.transform.position, camMask.transform.position, Time.deltaTime * smooth);
            cameraPlaceholder.transform.rotation = Quaternion.Lerp(cameraPlaceholder.transform.rotation, camMask.transform.rotation, Time.deltaTime * smooth);
        }
        else
        {
            smooth = defualtSmoothing + Mathf.Pow(defualtSmoothing, (lagDistance - maxDistanceAway * 10));
            cameraPlaceholder.transform.position = Vector3.Lerp(cameraPlaceholder.transform.position, camMask.transform.position, Time.deltaTime * smooth);
            cameraPlaceholder.transform.rotation = Quaternion.Lerp(cameraPlaceholder.transform.rotation, camMask.transform.rotation, Time.deltaTime * smooth);
        }
        
    }

	/// <summary>
    /// Overrides the camera position if a wall is hit
    /// </summary>
    bool WallDetection(ref Vector3 targetFollow)
	{
        RaycastHit wallHit = new RaycastHit();
        //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
		Debug.DrawRay(targetFollow, camMask.transform.position, Color.blue);

        int layerNumber = (int)(Mathf.Log((uint)layerMaskToExclude.value, 2));
        
        if(Physics.Linecast(targetFollow, camMask.transform.position, out wallHit, layerNumber)){
            //the smooth is increased so you detect geometry collisions faster.
            smooth = defualtSmoothing * 2;
            //the x and z coordinates are pushed away from the wall by hit.normal.
            //the y coordinate stays the same.
            camMask.transform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, 
                                                    camMask.transform.position.y, 
                                                    wallHit.point.z + wallHit.normal.z * 0.5f);

			Debug.DrawRay(targetFollow, new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, 
                                                    camMask.transform.position.y, 
                                                    wallHit.point.z + wallHit.normal.z * 0.5f), 
                                                    Color.green);
            return true;
        }
        return false;
    }

    void LateUpdate()
    {
        if(isActive && SettingsController.UserInput)
        { 
            _mainCamera.transform.position = cameraPlaceholder.transform.position;
            _mainCamera.transform.rotation = cameraPlaceholder.transform.rotation;
        }
    }
}

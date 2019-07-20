//Created by MrJohnWeez

using UnityEngine;
using System.Collections;

public class RealCamera : MonoBehaviour{
    [Header("Set Varibles")]
    public Transform target;
    
    [Header("Properties")]
    public float defualtSmoothing = 0.3f;
    public float verticalRotation = 0.0f;
    public float horizontalRotation = 180.0f;
    public float distanceAway = 2.81f;
    public Vector3 CamOffset = new Vector3(0,0.6f,0);

    [Header("Limits")]
    public float verticleRotationLimitMin = 89;
    public float verticleRotationLimitMax = 22;
    public float maxDistanceAway = 3;
    public float minDistanceAway = 0.1f;
    public LayerMask layerMaskToExclude;

    private GameObject camMask;
    private Vector3 _distanceOffset;
    private Vector3 _targetOffset;
    private Quaternion _newRotation;
    private bool _camHittingWall = false;
    private float smooth = 0.3f;                    //how smooth the camera moves into place


    void Start(){
        camMask = new GameObject("camTransform");
        camMask.transform.position = transform.position;
        camMask.transform.rotation = transform.rotation;
        camMask.transform.localScale = transform.localScale;
        smooth = defualtSmoothing;
    }
 
    private void FixedUpdate()
    {
        // Get inputs
        verticalRotation += InputHandler.instance.GetAxis("LookY");
        horizontalRotation += InputHandler.instance.GetAxis("LookX");

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

	/// <summary>
    /// Smooths the camera follow and prevents bouncy camera motion
    /// </summary>
    void smoothCamMethod()
	{
        float lagDistance = Vector3.Distance(transform.position, camMask.transform.position);
        if(!_camHittingWall && lagDistance > maxDistanceAway)
        {
            smooth = defualtSmoothing;
            transform.position = Vector3.Lerp(transform.position, camMask.transform.position, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Lerp(transform.rotation, camMask.transform.rotation, Time.deltaTime * smooth);
        }
        else
        {
            smooth = defualtSmoothing + Mathf.Pow(defualtSmoothing, (lagDistance - maxDistanceAway * 10));
            transform.position = Vector3.Lerp(transform.position, camMask.transform.position, Time.deltaTime * smooth);
            transform.rotation = Quaternion.Lerp(transform.rotation, camMask.transform.rotation, Time.deltaTime * smooth);
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
}
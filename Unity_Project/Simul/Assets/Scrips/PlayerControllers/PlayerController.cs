using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Make a player able to move around a 3d cube </summary>
public class PlayerController : MonoBehaviour
{
    public LayerMask toHitGround;
    public GameObject cameraPlaceholder = null;
    public float moveSpeed = 7000;
    public float rotationSpeed = 100;
    public float gravityRotationSmoothing = 0.5f;
    public bool isActive = false;

    private Rigidbody rb = null;
    private float gravity = -20;
    private float moveRot = 0;
    private float movePos = 0;
    private Quaternion oldRotation;
    private float lookAngle;
    Vector3 forwardMotion;
    Vector3 rightMotion;
    Vector3 motion;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldRotation = transform.rotation;
    }

    private void Update()
    {
        moveRot = Input.GetAxisRaw("Horizontal");
        movePos = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveRot, 0, movePos).normalized;
        // moveRot = direction.x;
        // movePos = direction.z;
        
        forwardMotion = movePos * Vector3.ProjectOnPlane(cameraPlaceholder.transform.forward, transform.up).normalized;
        rightMotion = moveRot * Vector3.ProjectOnPlane(cameraPlaceholder.transform.right, transform.up).normalized;
        motion = forwardMotion + rightMotion;

        // Quaternion newRotation = Quaternion.LookRotation(forwardMotion + rightMotion);

        // transform.rotation = newRotation;
        if(direction != Vector3.zero){
            transform.LookAt(transform.position + motion, transform.up);
        }

        oldRotation = transform.rotation;
        UpdatePlayerNormal();
    }

    private void FixedUpdate() {
        if(isActive)
        {
            // Update position and rotation of player
            rb.AddForce(motion * moveSpeed, ForceMode.Force);
            // rb.AddRelativeTorque();
        }
        rb.AddRelativeForce(Vector3.down * -gravity, ForceMode.Acceleration);
    }

    /// <summary> Determine if the user is about to fall off a edge and update thier normal to match
    /// surrounding faces </summary>
    private void UpdatePlayerNormal()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position + new Vector3(0,0.1f,0), -transform.up, out hit, 20f, toHitGround))
        {
            // Plane is under user
            CalculateNewRotation(hit.normal);
        }
        else if(Physics.Raycast(transform.position, -transform.forward, out hit, 20f, toHitGround))
        {
            // Plane behind user
            CalculateNewRotation(hit.normal);
        }
        else if(Physics.Raycast(transform.position, transform.forward, out hit, 20f, toHitGround))
        {
            // Plane infront of user
            CalculateNewRotation(hit.normal);
        }
    }

    /// <summary> Rotate the player so that the player's up vector matches with the normal given
    /// also mostly keeping the original forward direction </summary>
    private void CalculateNewRotation(Vector3 hitNormal)
    {
        Vector3 myForward = Vector3.Cross(transform.right,hitNormal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, hitNormal);
        transform.rotation = Quaternion.Lerp(targetRot, oldRotation, gravityRotationSmoothing);
    }
}

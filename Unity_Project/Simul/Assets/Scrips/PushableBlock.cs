using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Creates a block that can be pushed around a 3d object without falling off </summary>
public class PushableBlock : MonoBehaviour
{
    public LayerMask toHitGround;
    Rigidbody rb = null;
    private float gravity = -20;
    private Quaternion oldRotation;
    public float gravityRotationSmoothing = 0.5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        oldRotation = transform.rotation;
        UpdatePlayerNormal();
    }

    private void FixedUpdate() {
        rb.AddRelativeForce(Vector3.down * -gravity, ForceMode.Acceleration);
    }
    /// <summary> Determine if the block is about to fall off a edge and update its normal to match
    /// surrounding faces </summary>
    private void UpdatePlayerNormal()
    {
        Vector3[] directions = new Vector3[6] {transform.up, -transform.up, transform.right, -transform.right, transform.forward, -transform.forward};
        RaycastHit hit;
        foreach (Vector3 dir in directions)
        {
            if(Physics.Raycast(transform.position, dir, out hit, 1f, toHitGround))
            {
                // Plane is under user
                print("Hit! :  " + dir);
                CalculateNewRotation(hit.normal);
                return;
            }
        }
    }

    /// <summary> Rotate the player so that the player's up vector matches with the normal given
    /// also mostly keeping the original forward direction </summary>
    private void CalculateNewRotation(Vector3 hitNormal)
    {
        transform.up = hitNormal;
        // Vector3 myForward = Vector3.Cross(transform.right,hitNormal);
        // Quaternion targetRot = Quaternion.LookRotation(myForward, hitNormal);
        // transform.rotation = Quaternion.Lerp(targetRot, oldRotation, gravityRotationSmoothing);
    }
}

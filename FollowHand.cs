/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowHand : MonoBehaviour
{
    public Transform avatar;  // Reference to the avatar's Transform
    public float distance = 0.49f;  // Distance in front of the avatar
    public float fixedY = 3.97f;  // Desired fixed height (Y position)
    public Vector3 fixedRotation = new Vector3(0, 0, 0); // Fixed rotation (X, Y, Z)

    void LateUpdate()
    {
        if (avatar != null)
        {
            // Calculate the position in front of the avatar
            Vector3 frontPosition = avatar.position + avatar.forward * distance;

            // Set the Y position to the fixed value
            frontPosition.y = fixedY;

            // Apply the calculated position
            transform.position = frontPosition;

            // Set the fixed rotation
            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }
}*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    public Transform avatar;  // Reference to the avatar's Transform
    public float distance = 0.49f;  // Distance in front of the avatar
    public float fixedY = 3.97f;  // Desired fixed height (Y position)
    public Vector3 fixedRotation = new Vector3(0, 0, 0); // Fixed rotation (X, Y, Z)
    public string gripButton = "Grip"; // Name of the grip button input

    private bool isAttached = true; // Track attachment state

    void LateUpdate()
    {
        if (isAttached && avatar != null)
        {
            // Calculate the position in front of the avatar
            Vector3 frontPosition = avatar.position + avatar.forward * distance;

            // Set the Y position to the fixed value
            frontPosition.y = fixedY;

            // Apply the calculated position
            transform.position = frontPosition;

            // Set the fixed rotation
            transform.rotation = Quaternion.Euler(fixedRotation);
        }

        // Check for detachment input
        if (isAttached && Input.GetButtonDown(gripButton))
        {
            Detach();
        }
    }

    void Detach()
    {
        // Detach the object
        isAttached = false;
        transform.parent = null;

        // Enable physics if the object has a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Debug.Log("Object detached!");
    }

    public void Attach(Transform newParent)
    {
        // Reattach the object
        isAttached = true;
        transform.parent = newParent;

        // Reset local position and rotation
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Disable physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Debug.Log("Object attached!");
    }
}
*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class FollowHand : MonoBehaviour
{
    public Transform avatar; // Reference to the avatar's Transform
    public float distance = 0.49f; // Distance in front of the avatar
    public float fixedY = 3.97f; // Desired fixed height (Y position)
    public Vector3 fixedRotation = new Vector3(0, 0, 0); // Fixed rotation (X, Y, Z)

    public XRController rightController; // Reference to the XR Controller (Oculus Device-Based)
    private bool isAttached = true; // Track attachment state
    private Vector3 fixedPosition; // Store the fixed position when detaching

    void LateUpdate()
    {
        if (isAttached && avatar != null)
        {
            // Calculate the position in front of the avatar
            Vector3 frontPosition = avatar.position + avatar.forward * distance;

            // Set the Y position to the fixed value
            frontPosition.y = fixedY;

            // Apply the calculated position
            transform.position = frontPosition;

            // Set the fixed rotation
            transform.rotation = Quaternion.Euler(fixedRotation);
        }

        // Check if the grip button is pressed on the right controller
        if (isAttached && rightController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isGripPressed))
        {
            if (isGripPressed)
            {
                DetachAndFixPosition();
            }
        }
    }

    void DetachAndFixPosition()
    {
        // Save the current position and stop updating
        fixedPosition = transform.position;
        isAttached = false;

        // Stop the block from moving with the avatar
        transform.position = fixedPosition;

        // Optionally, enable physics if the block needs to interact with the environment
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Debug.Log("Object detached and fixed in place!");
    }

    public void Attach()
    {
        // Reattach the object to follow the avatar
        isAttached = true;

        // Disable physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Debug.Log("Object attached!");
    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FollowHand : MonoBehaviour
{
    public Transform avatar; // Reference to the avatar's Transform
    public float distance = 0.49f; // Distance in front of the avatar
    public float fixedY = 3.97f; // Desired fixed height (Y position)
    public Vector3 fixedRotation = new Vector3(0, 0, 0); // Fixed rotation (X, Y, Z)

    public XRController rightController; // Reference to the XR Controller (Oculus Device-Based)
    private bool hasBeenGrabbed = false; // Track if the object has been grabbed
    private bool isCurrentlyGrabbed = false; // Track if the grip is currently held

    void LateUpdate()
    {
        if (!hasBeenGrabbed && avatar != null)
        {
            // Calculate the position in front of the avatar
            Vector3 frontPosition = avatar.position + avatar.forward * distance;

            // Set the Y position to the fixed value
            frontPosition.y = fixedY;

            // Apply the calculated position
            transform.position = frontPosition;

            // Set the fixed rotation
            transform.rotation = Quaternion.Euler(fixedRotation);
        }

        // Check grip button press
        if (rightController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isGripPressed))
        {
            if (isGripPressed && !hasBeenGrabbed)
            {
                hasBeenGrabbed = true; // Mark that the object has been grabbed once
                isCurrentlyGrabbed = true; // Track that the grip is still being held
                Debug.Log("Object grabbed!");
            }
            else if (!isGripPressed && hasBeenGrabbed && isCurrentlyGrabbed)
            {
                Destroy(gameObject); // Destroy the object when the grip is released
                Debug.Log("Object released and destroyed!");
            }
        }
    }
}


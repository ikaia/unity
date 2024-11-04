using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoldCube : MonoBehaviour
{
    public Transform handTransform; // Avatar hand transform
    public XRGrabInteractable grabInteractable; // Grab component

    private bool isGrabbed = false;

    void Start()
    {
        // Make the object a child of the hand at the start
        transform.SetParent(handTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Listen to grab and release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        // Keep the object at the hand's position and rotation
        if (!isGrabbed) // If it's not grabbed, maintain position on the hand
        {
            transform.position = handTransform.position;
            transform.rotation = handTransform.rotation;
        }
    }

    // Called when the object is grabbed
    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    // Called when the object is released
    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;

        // Ensure the object stays attached to the hand when released
        transform.SetParent(handTransform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
